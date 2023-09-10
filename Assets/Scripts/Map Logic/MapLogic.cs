using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapLogic : Utils.Singleton<MapLogic> {

    private Nexus nexusNode;
    public uint difficulty = 10;
    public uint bufferDifficulty;
    public GameObject[] spawnerNodes;

    private bool WaveSpawnSignal = false;
    private float timer;

    public bool gameRunning = true;

    private float timeBetweenWaves = 30f;
    public float getTimeBetweenWaves() { return timeBetweenWaves; }

    private bool alertSoundPlayed = false;
    private bool incomingVeryHardPlayed = false;

    public event Action OnTimerStarted;

    public event Action OnWaveStarted;

    void Start()
    {
        nexusNode = this.gameObject.transform.GetChild(0).GetComponent<Nexus>();

        StartCoroutine(NewLogic());
    }

    private IEnumerator NewLogic() {
        yield return new WaitForSeconds(0.5f);
        while (nexusNode.alive) {
            if (!h_checkIfAnySpawnerHasChildren()) {
                OnTimerStarted?.Invoke();
                yield return new WaitForSeconds(timeBetweenWaves);
                if (!alertSoundPlayed)// && timer >= timeBetweenWaves - 5f)
                {
                    Debug.Log("Wave Alert Sound played");
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveAlert", transform.position);
                    alertSoundPlayed = true;
                }

                if (difficulty > 100 && !incomingVeryHardPlayed)// && timer >= timeBetweenWaves - 5f)
                {
                    Debug.Log("Wave Alert Sound played -- VERY HARD");
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveIncomingVeryHard", transform.position);
                    incomingVeryHardPlayed = true;
                }

                h_divideDifficultyToPaths();
                OnWaveStarted?.Invoke();
            } else {
                yield return new WaitForEndOfFrame();
            }
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveComplete", transform.position);
    }

   

    bool h_checkIfAnySpawnerHasChildren()
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

    #region Deprecated
    [Obsolete("Use NewLogic() instead", true)]
    private void Logic() {
        if (!h_checkIfAnySpawnerHasChildren()) {
            WaveSpawnSignal = h_timerCountdown();
        }

        if (WaveSpawnSignal) {
            if (!alertSoundPlayed)// && timer >= timeBetweenWaves - 5f)
            {
                Debug.Log("Wave Alert Sound played");
                FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveAlert", transform.position);
                alertSoundPlayed = true;
            }

            if (difficulty > 100 && !incomingVeryHardPlayed)// && timer >= timeBetweenWaves - 5f)
            {
                Debug.Log("Wave Alert Sound played -- VERY HARD");
                FMODUnity.RuntimeManager.PlayOneShot("event:/Wave/WaveIncomingVeryHard", transform.position);
                incomingVeryHardPlayed = true;
            }

            h_divideDifficultyToPaths();
        }
    }
    #endregion
}
