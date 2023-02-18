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

    private float _timeDelayBuffer;

    private void Start() {
        StartCoroutine(SpawnWave());
    }

    public IEnumerator SpawnWave() {
        uint spawnDifficultyBuffer = currentDifficulty;
        const uint easy = 1;
        const uint medium = 5;
        const uint hard = 9;

        while(spawnDifficultyBuffer > 0) {
            if (spawnDifficultyBuffer >= hard) {
                spawnDifficultyBuffer -= h_EnemyDecider(new uint[] { easy, medium, hard });
            } else if (spawnDifficultyBuffer >= medium) {
                spawnDifficultyBuffer -= h_EnemyDecider(new uint[] { easy, medium });
            } else if (spawnDifficultyBuffer > 0){
                spawnDifficultyBuffer -= h_EnemyDecider(new uint[] { easy });
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private uint h_EnemyDecider(uint[] usedDifficulties) {
        switch (usedDifficulties[Random.Range(0, usedDifficulties.Length)]) {
            case 1u:
                GameObject EasyEnemy = Instantiate(easyEnemies[Random.Range(0, easyEnemies.Length)].gameObject, this.transform);
                return 1u;
            case 5u:
                GameObject MediumEnemy = Instantiate(mediumEnemies[Random.Range(0, mediumEnemies.Length)].gameObject, this.transform);
                return 5u;
            case 9u:
                GameObject HardEnemy = Instantiate(hardEnemies[Random.Range(0, hardEnemies.Length)].gameObject, this.transform);
                return 9u;
            default:
                return 0u;
        }
    }


    private bool h_SpawnDelay() {
        _timeDelayBuffer += Time.deltaTime;
        if (_timeDelayBuffer >= 1f) {
            _timeDelayBuffer = 0f;
            return true;
        }
        return false;
    }
}
