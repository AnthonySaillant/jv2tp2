using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    private static List<CharacterSpawnPoint> unusedSpawnPoints; // Liste statique de spawnPoints
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        SpawnRandomPlace();
    }

    private void SpawnRandomPlace()
    {
        var spawnPoints = gameManager.CityObjects.CharacterSpawnPoints;

        if (unusedSpawnPoints == null || unusedSpawnPoints.Count == 0)
        {
            unusedSpawnPoints = new List<CharacterSpawnPoint>(spawnPoints);
        }

        int randomIndex = Random.Range(0, unusedSpawnPoints.Count);
        var spawnPoint = unusedSpawnPoints[randomIndex];

        transform.position = spawnPoint.Position;
        unusedSpawnPoints.RemoveAt(randomIndex);
    }
}
