using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector3 spawnPos;
    public Vector3 velocity;
    new Rigidbody rigidbody;

    public float explosionRadius;
    public GameObject explosionPrefab;

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
        if (explosionPrefab != null)
        {
            Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("Player", "Enemies", "Asteroids"));

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<Entity>(out Entity entity))
                {
                    float factor = (explosionRadius - Vector3.Distance(transform.position, entity.transform.position)) / explosionRadius;
                    if (factor > 0) entity.Damage((int)(factor * damage));
                }
            }
        }
        else
        {
            Entity entity = collision.gameObject.GetComponentInParent<Entity>();
            if (entity != null)
            {
                entity.Damage(damage);
            }
        }

        Destroy(gameObject);
    }
}
