using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5.0f;
    public float movementSpeed = 2.0f;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Update()
    {
        Vector3 lookDirection;

        {   // Mouse Aiming
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.y = transform.position.y;

            lookDirection = (target - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

        {   // Movement
            Vector3 moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) moveDirection.z += 1f;
            if (Input.GetKey(KeyCode.A)) moveDirection.x -= 1f;
            if (Input.GetKey(KeyCode.S)) moveDirection.z -= 1f;
            if (Input.GetKey(KeyCode.D)) moveDirection.x += 1f;
            controller.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }

        // Projectile
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(projectilePrefab);
            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            projectile.transform.position = transform.position;
            rigidbody.velocity = lookDirection * projectileSpeed;
            rigidbody.excludeLayers = LayerMask.GetMask("Player");
        }
    }

}
