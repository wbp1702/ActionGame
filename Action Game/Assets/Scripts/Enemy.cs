using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public Weapon weapon;

    private NavMeshAgent agent;
    private float lastRandom;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (health <= 0) Destroy(gameObject);

        Vector3 target = Player.Instance.transform.position;
        agent.SetDestination(target);

        target.y = transform.position.y;
        lastRandom = Random.Range(Mathf.Min(lastRandom - 0.1f, 0), Mathf.Max(lastRandom + 0.1f, 1));
        target += Player.Instance.controller.velocity * ((target - transform.position).magnitude / weapon.exitVelocity) * lastRandom;
        //transform.rotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
        transform.LookAt(target, Vector3.up);
    }

    private void FixedUpdate()
    {
        weapon.SetTrigger(Physics.Raycast(transform.position, Player.Instance.transform.position - transform.position, float.PositiveInfinity, LayerMask.GetMask("Player")));
    }
}
