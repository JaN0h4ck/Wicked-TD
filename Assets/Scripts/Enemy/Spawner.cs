using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public BasicEnemy[] easyEnemies;
    public BasicEnemy[] mediumEnemies;
    public BasicEnemy[] hardEnemies;

    public uint currentDifficulty;
    private MapLogic _mapLogic;
    private float _timeDelayBuffer;

    private void Start()
    {
        _mapLogic = gameObject.transform.parent.gameObject.transform.parent.GetComponent<MapLogic>();
        StartCoroutine(SpawnWave());
        Debug.Log("Coroutine started!!");
    }

    private IEnumerator SpawnWave() {
        const uint easy = 1;
        const uint medium = 5;
        const uint hard = 9;

        while (_mapLogic.gameRunning)
        {
            if(currentDifficulty > 0) {
                if (currentDifficulty >= hard) {
                    currentDifficulty -= h_EnemyDecider(new uint[] { easy, medium, hard });
                } else if (currentDifficulty >= medium) {
                    currentDifficulty -= h_EnemyDecider(new uint[] { easy, medium });
                } else if (currentDifficulty > 0){
                    currentDifficulty -= h_EnemyDecider(new uint[] { easy });
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private uint h_EnemyDecider(uint[] usedDifficulties) {
        Debug.Log("executed enemydecider");
        switch (usedDifficulties[Random.Range(0, usedDifficulties.Length)]) {
            case 1:
                Debug.Log("EasyEnemy spawned!");
                GameObject EasyEnemy = Instantiate(easyEnemies[Random.Range(0, easyEnemies.Length)].gameObject, this.transform);
                return (uint)1;
            case 5:
                Debug.Log("MediumEnemy spawned!");
                GameObject MediumEnemy = Instantiate(mediumEnemies[Random.Range(0, mediumEnemies.Length)].gameObject, this.transform);
                return (uint)5;
            case 9:
                Debug.Log("HardEnemy spawned!");
                GameObject HardEnemy = Instantiate(hardEnemies[Random.Range(0, hardEnemies.Length)].gameObject, this.transform);
                return (uint)9;
            default:
                return 0u;
        }
    }
}
