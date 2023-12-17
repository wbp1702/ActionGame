using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    public enum State
	{
        Idle = 0,
        Attacking = 1,
	}

    [Header("Inscribed")]
    public int initialHealth = 100;
    public float attackDistance = 10f;
    public float separationDistance = 5;
    public int playerHealOnDeath = 10;

    [Header("Dynamic")]
    public State state = State.Attacking;
    public Weapon weapon;
    private NavMeshAgent agent;

    void Start()
    {
        state = State.Attacking;
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth = initialHealth;
        weapon.parent = this;
    }

    void Update()
    {
        if (health <= 0)
		{
            Destroy(gameObject);
            return;
        }

        Vector3 target = Player.Instance.transform.position;

        if (state == State.Idle)
		{
            agent.SetDestination(transform.position);
            if (Vector3.Distance(target, transform.position) <= attackDistance && Vector3.Angle(target - transform.position, transform.forward) <= 90)
			{
                Ray ray = new Ray(transform.position, target - transform.position);
                Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Player", "Asteroids"));
                if (hit.collider.gameObject == Player.Instance.gameObject) state = State.Attacking;
			}
        }
        if (state == State.Attacking)
		{
            Vector3 minimumDistanceOffset = (transform.position - target).normalized * separationDistance;
            agent.SetDestination(minimumDistanceOffset + target);
            target.y = transform.position.y;

            // Estimate Player Position when projectile would hit player
            target += Player.Instance.rigidbody.velocity * ((target - transform.position).magnitude / weapon.exitVelocity);
            
            var targetRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            
		}
    }

    private void FixedUpdate()
    {
        if (state == State.Idle) weapon.SetTrigger(false);
        else
		{
            Ray ray = new Ray(transform.position, Player.Instance.transform.position - transform.position);
            Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Player", "Asteroids"));
            weapon.SetTrigger(hit.collider.gameObject == Player.Instance.gameObject);
		}
    }

    public override void Damage(int amount)
	{
		base.Damage(amount);

        if (state == State.Idle) state = State.Attacking;

        if (health <= 0)
        {
            Level.Instance.EnemyDeath();
            Player.Instance.Heal(playerHealOnDeath);
        }
	}

	public override float GetSpeed()
	{
        return agent.velocity.magnitude;
	}
}
