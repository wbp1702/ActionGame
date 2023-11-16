using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector3 spawnPos;
    public Vector3 velocity;
    new Rigidbody rigidbody;

	private void Start()
	{
        spawnPos = transform.position;
        rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
        velocity = rigidbody.velocity;
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Level.Instance.gameObject) return;

        if (other.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.health = Mathf.Max(Player.Instance.health - damage, 0);
            Destroy(gameObject);
            return;
        }

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.health = Mathf.Max(enemy.health - damage, 0);
            Destroy(gameObject);
            return;
        }

    }
}
