using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject boyPrefab;
    public GameObject girlPrefab;
    private Vector2 temp;

    [SerializeField] float interval;
    public AnimationCurve intervalCurve; // Assign this curve in the inspector
    private float timer;
    public float maxTime = 60f; // Maximum time for the curve
    private float totalTime = 0f;

    private void OnEnable()
    {
        timer = 0;
    }

    private void OnDisable()
    {
        Pooler.ClearPools();
    }

    private void Awake()
    {
        Pooler.ClearPools();
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        GameObject enemyPrefab = rand > 0 ? boyPrefab : girlPrefab;
        Pooler.Spawn(enemyPrefab, GetPointOnMap(), Quaternion.identity);
    }

    public Vector3 GetPointOnMap()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        float randomX = rand > 0 ? -6.5f : 6.5f;
        float randomY = UnityEngine.Random.Range(-3.75f, 1);
        temp.x = randomX;
        temp.y = randomY;

        return temp;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        totalTime += Time.deltaTime;

        // Calculate the interval using the animation curve
        float interval = intervalCurve.Evaluate(totalTime / maxTime);

        if (timer > interval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }
}
