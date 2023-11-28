using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[Header("Entity")]
	[SerializeField]
    protected int maxHealth;
	[SerializeField]
    protected int health;

	const float MinimumRelativeVelocity = 1f;

    public virtual void Damage(int amount)
	{
		health = Mathf.Max(0, health - amount);
	}

    public virtual void Heal(int amount)
	{
		health = Mathf.Max(maxHealth, health + amount);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent<Entity>(out Entity entity))
		{
			if (collision.relativeVelocity.magnitude > MinimumRelativeVelocity)
			{
				entity.Damage((int)collision.relativeVelocity.magnitude);
			}
		}
	}

	public abstract float GetSpeed();
}
