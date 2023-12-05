using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public GameObject[] spawnPoints;
    public bool enemiesDead;

    private List<Enemy> enemies = new List<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy) enemy.state = Enemy.State.Attacking;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enemiesDead)
        {
            var enemy = Instantiate<GameObject>(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].gameObject);
            enemy.transform.position = spawnPoints[Random.Range(0, enemyPrefabs.Length)].transform.position;
            enemiesDead = false;
        }
    }

    private void Update()
    {
        if (enemiesDead == false)
        {
            for (int i = 0; i < enemies.Count; i++) if (enemies[i] == null) { enemiesDead = false; break; }

            if (enemiesDead)
            {
                Player.Instance.Heal(100);
            }
        }
    }
}
