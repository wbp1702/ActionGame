using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Level.Instance.gameObject) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.health = Mathf.Max(enemy.health - damage, 0);
        }

        Destroy(gameObject);
    }
}
