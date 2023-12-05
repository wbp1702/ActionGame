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

    [Header("Dynamic")]
    public State state = State.Idle;
    public Weapon weapon;
    private NavMeshAgent agent;
    private float lastRandom;

    void Start()
    {
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
            lastRandom = Random.Range(Mathf.Min(lastRandom - 0.1f, 0), Mathf.Max(lastRandom + 0.1f, 1));
            target += Player.Instance.rigidbody.velocity * ((target - transform.position).magnitude / weapon.exitVelocity) * lastRandom;
            transform.LookAt(target, Vector3.up);
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
	}

	public override float GetSpeed()
	{
        return agent.velocity.magnitude;
	}
}
