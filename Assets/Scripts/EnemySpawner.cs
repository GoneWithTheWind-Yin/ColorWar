using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform START;
    public float waveRate = 1;
    public static int CountEnemyAlive = 0;
    public static int currentWave = 0;

    void Start() {
        StartCoroutine("SpawnEnemy");
    }

    public void Stop() {
        StopCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy() {
        currentWave = 0;
        foreach (Wave wave in waves) {
            currentWave++;
            for (int i = 0; i < wave.count; ++i) {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1) {
                    yield return new WaitForSeconds(wave.rate);
                }
            }
            while (CountEnemyAlive > 0) {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0) {
            yield return 0;
        }
        GameManage.Instance.Win();
    }

    public static int GetWave() {
        return currentWave;
    }

}
