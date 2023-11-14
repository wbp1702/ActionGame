using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector3 spawnPos;
    public Vector3 velocity;
    Rigidbody rigidbody;

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
        if (other.gameObject == Level.Instance.gameObject || other.gameObject == Player.Instance.gameObject) return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.health = Mathf.Max(enemy.health - damage, 0);
        }

        Debug.Log($"Projectile Destroyed at {{{transform.position}}} by {other.gameObject.name}, spawnPos: {spawnPos}, vel: {GetComponent<Rigidbody>().velocity}");
        //Debug.Break();

        Destroy(gameObject);
    }
}
