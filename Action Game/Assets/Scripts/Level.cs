using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshCollider))]
public class Level : MonoBehaviour
{
    [System.Serializable]
	public class SpawnGroup
	{
		public Spawn[] spawns;
		public int minimumSpawns;
		public int maximumSpawns;
    }

	[System.Serializable]
	public class Spawn
	{
		public GameObject enemyPrefab;
		public float spawnChance = 1.0f;
	}

    public static Level Instance { get; private set; }

	[Header("Level Generation")]
	public float maximumSpawnRadius = 1f;

	public Transform[] spawnPoints;
	public SpawnGroup[] spawnGroups;

	public GameObject[] asteroidPrefabs;
    public int minimumAsteroidCount = 10;
    public int maximumAsteroidCount = 20;

	public GameObject minePrefab;
	public int minimumMineCount = 5;
	public int maximumMineCount = 10;

	[Header("UI")]
	public GameObject gameOverPanel;

	private int enemiesRemaining = 0;
	
	void Start()
	{
		if (Instance != null)
		{
			Debug.LogError("Multiple Instances of Level");
			Destroy(gameObject);
		}

		Instance = this;

		// Level Generation
		foreach (Transform spawnPoint in spawnPoints)
		{
			SpawnGroup spawnGroup = spawnGroups[Random.Range(0, spawnGroups.Length)];

			int spawnCount = 0;
			while (spawnCount < spawnGroup.maximumSpawns)
			{
				Spawn spawn = spawnGroup.spawns[Random.Range(0, spawnGroup.spawns.Length)];

				if (spawnCount < spawnGroup.minimumSpawns || Random.value <= spawn.spawnChance)
				{
					Vector3 positionOffset = Random.insideUnitSphere * Random.Range(0f, maximumSpawnRadius);
                    positionOffset.y = 2;
					Instantiate<GameObject>(spawn.enemyPrefab, spawnPoint.position + positionOffset, Quaternion.identity);
					spawnCount++;
					enemiesRemaining++;
				}
				else break;
            }
		}

		int asteroidCount = Random.Range(minimumAsteroidCount, maximumAsteroidCount);
		for (int i = 0; i < asteroidCount; i++)
		{
			Vector3 position = Random.insideUnitSphere * Random.Range(0f, maximumSpawnRadius);
			position.y = 2;
			Instantiate<GameObject>(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], position, Quaternion.identity);
		}

		int mineCount = Random.Range(minimumMineCount, maximumMineCount);
		for (int i = 0; i < mineCount; i++)
		{
			Vector3 position = Random.insideUnitSphere * Random.Range(0f, maximumSpawnRadius);
			position.y = 2;
			Instantiate<GameObject>(minePrefab, position, Quaternion.identity);
		}
		
		Time.timeScale = 1;
    }

    public void PlayerDeath()
	{
		Time.timeScale = 0;
		gameOverPanel.SetActive(true);
	}

	bool restaring = false;
	public void EnemyDeath()
	{
		if (!restaring)
		{
			enemiesRemaining--;
			if (enemiesRemaining == 0)
			{
				int nextScene = SceneManager.GetActiveScene().buildIndex;

				//if (SceneManager.sceneCount < nextScene) SceneManager.LoadScene(0);
				//SceneManager.LoadScene(nextScene + 1);
				Debug.Break();
			}
		}
    }

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		restaring = true;
    }
}
