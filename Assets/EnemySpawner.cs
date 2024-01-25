using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector2Variable playerPos;
    public GameObject enemyPrefab;
    private Vector2 temp;

    [SerializeField] float interval;
    private float timer;

    private void Awake()
    {
        timer = 0;
    }
    public void SpawnEnemy()
    {
        Pooler.Spawn(enemyPrefab, GetPointOnMap(), Quaternion.identity);
    }

    public Vector3 GetPointOnMap()
    {
        int randomX = UnityEngine.Random.Range(-11,11);
        int randomY = UnityEngine.Random.Range(-6, 6);
        temp.x = randomX;
        temp.y = randomY;

        return playerPos.value + temp;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }
}
