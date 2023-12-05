using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPoints;

    [SerializeField]
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
        bool enemiesDead = true;
        
        for (int i = 0; i < enemies.Count; i++) if (enemies[i] != null) { enemiesDead = false; break; }

        if (enemiesDead == true)
        {
            enemies.Clear();

            Player.Instance.Heal(100);

            int val = Random.Range(0, enemyPrefabs.Length);
            if (val < enemyPrefabs.Length)
            {
                var enemy = Instantiate<GameObject>(enemyPrefabs[val]);
                val = Random.Range(0, spawnPoints.Length);
                enemy.transform.position = spawnPoints[val].transform.position;
                var e = enemy.GetComponent<Enemy>();
                e.state = Enemy.State.Attacking;
                enemies.Add(e);

            }
            
            enemiesDead = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy) enemy.state = Enemy.State.Idle;
            }
        }
    }
}
