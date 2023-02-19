using System.Linq;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    private Nexus nexusNode;
    public uint difficulty = 10;
    public uint bufferDifficulty;
    public GameObject[] spawnerNodes;

    private bool WaveSpawnSignal = false;
    private float timer;

    public bool gameRunning = true;

    private float timeBetweenWaves = 30f;
    private bool alertSoundPlayed = false;
    private bool incomingVeryHardPlayed = false; 

    void Start()
    {
        nexusNode = this.gameObject.transform.GetChild(0).GetComponent<Nexus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nexusNode.alive)
        {
            Logic();
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveComplete", transform.position);
        }
    }

    private void Logic()
    {
        if (!h_checkIfAnySpawnerGotChildren())
        {
            WaveSpawnSignal = h_timerCountdown();
        }

        if (WaveSpawnSignal)
        {
            if (!alertSoundPlayed && timer >= timeBetweenWaves - 5f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveAlert", transform.position);
                alertSoundPlayed = true;
            }

            if (difficulty > 100 && !incomingVeryHardPlayed && timer >= timeBetweenWaves - 5f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveIncomingVeryHard", transform.position);
                incomingVeryHardPlayed = true;
            }

            h_divideDifficultyToPaths();
        }
    }

    bool h_checkIfAnySpawnerGotChildren()
    {
        ushort evaluationBuffer = 0;
        foreach (var node in spawnerNodes)
        {
            evaluationBuffer += h_boolToInt(node.transform.childCount > 0);
        }
        return evaluationBuffer > 0;
    }

    private ushort h_boolToInt(bool i)
    {
        return i ? (ushort)1 : (ushort)0;
    }

    private bool h_timerCountdown()
    {
        timer += Time.deltaTime;
        bool state = (timer >= timeBetweenWaves);
        if (state)
        {
            timer = 0f;
            alertSoundPlayed = false;
            incomingVeryHardPlayed = false;
        }
        return state;
    }

    float getTimeUntilNextWave()
    {
        return timer;
    }

    private void h_divideDifficultyToPaths()
    {
        difficulty += 10;
        bufferDifficulty = difficulty;

        for (int i = 0; i < Random.Range(2, Random.Range(0, 15) * spawnerNodes.Count()); i++)
        {
            int a = Random.Range(0, spawnerNodes.Length);
            int b = Random.Range(0, spawnerNodes.Length);
            (spawnerNodes[a], spawnerNodes[b]) = (spawnerNodes[b], spawnerNodes[a]);
        }

        for (int i = 0; i < spawnerNodes.Count(); i++)
        {
            uint generatedRange = (uint)Random.Range(0, bufferDifficulty);
            bufferDifficulty -= generatedRange;

            if (i == spawnerNodes.Length)
            {
                spawnerNodes[i].gameObject.transform.GetComponent<Spawner>().currentDifficulty = bufferDifficulty;
                bufferDifficulty = 0;
            }
            else
                spawnerNodes[i].gameObject.transform.GetComponent<Spawner>().currentDifficulty = generatedRange;
        }
    }
}
