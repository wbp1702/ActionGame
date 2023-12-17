using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Entity
{
	[Header("Inscribed")]
	public GameObject explosionPrefab;
	public LayerMask effectedLayers;
	public int maxDamage = 100;
	public float damageRadius = 10f;
	public int initialHealth = 10;

	private Rigidbody rigidbody;
	private bool detonating = false;

	private void Awake()
	{
		health = maxHealth = initialHealth;
		rigidbody = GetComponent<Rigidbody>();
	}

	public override void Damage(int amount)
	{
		base.Damage(amount);

		if (health <= 0 && !detonating) Detonate();
	}

	private void Detonate()
	{
		detonating = true;
		Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity);
		
		Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, effectedLayers);

		foreach (Collider collider in colliders)
		{
			if (collider.TryGetComponent<Entity>(out Entity entity))
			{
				float factor = (damageRadius - Vector3.Distance(transform.position, entity.transform.position)) / damageRadius;
				if (factor > 0) entity.Damage((int)(factor * maxDamage));
			}
		}

		Destroy(gameObject);
	}

	public override float GetSpeed()
	{
		return rigidbody.velocity.magnitude;
	}
}
