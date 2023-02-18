using System.Linq;
using UnityEngine;
public class MapLogic : MonoBehaviour
{
    private Nexus nexusNode;
    private uint difficulty = 10;
    private uint bufferDifficulty;
    public Spawner[] spawnerNodes;

    private bool WaveSpawnSignal = false;

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
    }

    private void Logic()
    {
        if (WaveSpawnSignal) //TODO:wavespawnsignal muss geschrieben werden logic die überprüft ob enemies auf der karte sind muss noch implementiert werden
        {
            h_divideDifficultyToPaths();
        }
       
    }

    private void h_divideDifficultyToPaths()
    {
        difficulty += 10;
        bufferDifficulty = difficulty;

        for (int i = 0 ; i < Random.Range(2, Random.Range(0,15) * spawnerNodes.Count()); i++)
        {
            int a = Random.Range(0, spawnerNodes.Length);
            int b = Random.Range(0, spawnerNodes.Length);
            (spawnerNodes[a], spawnerNodes[b]) = (spawnerNodes[b], spawnerNodes[a]);
        }

        foreach (var node in spawnerNodes)
        {
            uint generatedRange = (uint)Random.Range(0, bufferDifficulty);
            bufferDifficulty -= generatedRange;
        }
    }
}
