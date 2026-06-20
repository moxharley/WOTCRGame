using System.Collections.Generic;
using UnityEngine;

namespace grcubes
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject enemyPrefab;

        [Header("Spawn Position")]
        [SerializeField] private float spawnDistance = 10f;

        [Header("Timing")]
        [SerializeField] private float spawnInterval = 2f;
        [SerializeField] private int maxAliveEnemies = 4;

        [Header("Source")]
        [SerializeField] private Transform player;

        private readonly List<GameObject> aliveEnemies = new List<GameObject>();
        private float spawnTimer;

        private void Start()
        {
            if (player == null)
            {
                player = Player.Instance.transform;
            }
        }

        private void Update()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                TrySpawn();
            }
        }

        private void TrySpawn()
        {
            if (aliveEnemies.Count >= maxAliveEnemies)
                return;

            Vector2 spawnPos = GetRingSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            aliveEnemies.Add(enemy);
        }

        private Vector2 GetRingSpawnPosition()
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnDistance;
            return (Vector2)player.position + offset;
        }
    }
}