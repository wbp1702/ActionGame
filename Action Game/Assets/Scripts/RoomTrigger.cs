using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform nextRoom;

    private bool enemiesDead;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //foreach (Enemy enemy in enemies)
            //{
            //    if (enemy) enemy.state = Enemy.State.Attacking;
            //}
        }
    }

    private void Update()
    {
        if (enemiesDead) return;

        for (int i = 0; i < enemies.Length; i++) if (enemies[i] != null) { enemiesDead = false; break; }

        if (enemiesDead == true)
        {
            Player.Instance.Heal(100);
            Player.Instance.transform.position = nextRoom.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //foreach (Enemy enemy in enemies)
            //{
            //    if (enemy) enemy.state = Enemy.State.Idle;
            //}
        }
    }
}
