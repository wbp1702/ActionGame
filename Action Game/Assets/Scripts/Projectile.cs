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

    private void OnCollisionEnter(Collision collision)
    {
        Entity entity = collision.gameObject.GetComponentInParent<Entity>();
        if (entity != null)
		{
            entity.Damage(damage);
		}

        Destroy(gameObject);
    }
}
