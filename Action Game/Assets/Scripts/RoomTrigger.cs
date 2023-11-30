using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Enemy[] enemies;

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

    private void Update()
    {
        bool enemiesDead = true;
        for (int i = 0; i < enemies.Length; i++) if (enemies[i] == null) { enemiesDead = false; break; }

        if (enemiesDead)
        {
            Player.Instance.Heal(100);
        }
    }
}
