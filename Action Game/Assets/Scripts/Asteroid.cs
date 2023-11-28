using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Entity
{
    [Header("Inscribed")]
    public float minInitialVelocity = 0f;
    public float maxInitialVelocity = 5f;
    public int initialHealth = 100;

    [Header("Dynamic")]
    private Rigidbody rigidbody;

    void Start()
    {
        health = maxHealth = initialHealth;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Random.insideUnitSphere * Random.Range(minInitialVelocity, maxInitialVelocity);
        rigidbody.angularVelocity = Random.insideUnitSphere * Random.Range(minInitialVelocity, maxInitialVelocity);
    }

	private void Update()
	{
		if (health <= 0)
		{
            Destroy(gameObject);
		}
	}

	public override float GetSpeed()
	{
        return rigidbody.velocity.magnitude;
	}
}
