using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Player Instance { get; private set; }

    [Header("Inscribed")]
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public int initialHealth = 100;
    public float strafeSpeed = 10f;
    public float boostSpeed = 20f;
    public float strafeRotationFactor = 0.5f;
    public float boostRotationFactor = 0.1f;
    public KeyCode boostKey = KeyCode.LeftShift;
    //public int health;

    public Rigidbody rigidbody;
    private Vector3 wasd_input;
    private bool boostActive;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Instances of Player");
            Destroy(gameObject);
        }

        Instance = this;
        rigidbody = GetComponent<Rigidbody>();
        health = maxHealth = initialHealth;
    }

    void Update()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.y = transform.position.y;

        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);

        boostActive = Input.GetKey(boostKey);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, boostActive ? boostRotationFactor : strafeRotationFactor);
        wasd_input = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Projectile
        if (Input.GetMouseButtonDown(0)) primaryWeapon.SetTrigger(true);
        else if (Input.GetMouseButtonUp(0)) primaryWeapon.SetTrigger(false);

        if (Input.GetMouseButtonDown(1)) secondaryWeapon.SetTrigger(true);
        else if (Input.GetMouseButtonUp(1)) secondaryWeapon.SetTrigger(false);
    }

	private void FixedUpdate()
	{
        if (boostActive)
		{
            rigidbody.velocity = transform.forward * Mathf.Lerp(rigidbody.velocity.magnitude, boostSpeed, 0.5f);
        }
        else
		{
            rigidbody.velocity = wasd_input.normalized * Mathf.Lerp(rigidbody.velocity.magnitude, strafeSpeed, 0.9f);
        }
	}

	public override float GetSpeed()
	{
        return rigidbody.velocity.magnitude;
	}
}
