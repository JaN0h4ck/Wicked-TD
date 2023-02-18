using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public BasicEnemy[] easyEnemies;
    public BasicEnemy[] mediumEnemies;
    public BasicEnemy[] hardEnemies;

    public uint currentDifficulty;

    private float _timeDelayBuffer;
    public void SpawnWave() {
        uint spawnDifficultyBuffer = currentDifficulty;
        uint easy = 1;
        uint medium = 5;
        uint hard = 9;
        
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
