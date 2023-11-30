using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshCollider))]
public class Level : MonoBehaviour
{
	[System.Serializable]
	public class EnemySpawn
	{
		public GameObject enemyPrefab;
		public int minimumSpawnCount = 1;
		public int maximumSpawnCount = 10;
	}

	public static Level Instance { get; private set; }

	[Header("Level Generation")]
	public float maximumSpawnRadius = 1f;

	public EnemySpawn[] enemySpawns;

	public GameObject[] asteroidPrefabs;
    public int minimumAsteroidCount = 10;
    public int maximumAsteroidCount = 20;

	public GameObject minePrefab;
	public int minimumMineCount = 5;
	public int maximumMineCount = 10;

	[Header("UI")]
	public GameObject gameOverPanel;
	
	void Start()
	{
		if (Instance != null)
		{
			Debug.LogError("Multiple Instances of Level");
			Destroy(gameObject);
		}

		Instance = this;

		// Level Generation
		foreach (EnemySpawn spawn in enemySpawns)
		{
			int enemyCount = Random.Range(spawn.minimumSpawnCount, spawn.maximumSpawnCount);
			for (int j = 0; j < enemyCount; j++)
			{
				Vector3 position = Random.insideUnitSphere * Random.Range(0f, maximumSpawnRadius);
				position.y = 0;
				Instantiate<GameObject>(spawn.enemyPrefab, position, Quaternion.identity);
			}
		}

		int asteroidCount = Random.Range(minimumAsteroidCount, maximumAsteroidCount);
		for (int i = 0; i < asteroidCount; i++)
		{
			Vector3 position = Random.insideUnitSphere * Random.Range(0f, maximumSpawnRadius);
			position.y = 0;
			Instantiate<GameObject>(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], position, Quaternion.identity);
		}

		int mineCount = Random.Range(minimumMineCount, maximumMineCount);
		for (int i = 0; i < mineCount; i++)
		{
			Vector3 position = Random.insideUnitSphere * Random.Range(0f, maximumSpawnRadius);
			position.y = 0;
			Instantiate<GameObject>(minePrefab, position, Quaternion.identity);
		}
		
		Time.timeScale = 1;
    }

    public void PlayerDeath()
	{
		Time.timeScale = 0;
		gameOverPanel.SetActive(true);
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
