using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Inscribed")]
    public float rateOfFire = 1.0f;
    [Range(0f, 180f)]
    public float spreadAngle = 1.0f;
    public float reloadTime = 1.0f;
    public int magazineSize = 10;

    public int burstCount = 1;
    public float burstDelay = 0.1f;
    
    public bool hasAutomatic = false;

    public float exitVelocity = 10.0f;
    public GameObject projectilePrefab;

    // Kickback

    [Header("Dynamic")]
    [SerializeField]
    private bool triggerHeld = false;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private int burstIndex;
    [SerializeField]
    private int remainingRounds;
    
    void Start()
    {
        remainingRounds = magazineSize;
    }

    void OnEnable()
	{
        if (remainingRounds == 0) delayTime = reloadTime;
	}

    // Update is called once per frame
    void Update()
    {
        delayTime = Mathf.Max(delayTime - Time.deltaTime, 0);

        if (remainingRounds == 0)
		{
            if (delayTime <= 0f) remainingRounds = magazineSize;
		}
        else if (burstIndex > 0 && delayTime <= 0f)
        {
            Fire();
        }
        else if (hasAutomatic && triggerHeld && delayTime <= 0f)
        {
            Fire();
        }
    }

    public void SetTrigger(bool trigger)
    {
        triggerHeld = trigger;

        if (trigger && remainingRounds > 0 && burstIndex == 0 && delayTime <= 0f)
        {
            burstIndex = burstCount;
            Fire();
        }
    }

    private void Fire()
    {
        remainingRounds--;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        
        Quaternion spreadRotation = Quaternion.AngleAxis(Random.Range(0, spreadAngle), Vector3.up);
        rigidbody.velocity = spreadRotation * (transform.forward * exitVelocity);
        rigidbody.excludeLayers = LayerMask.GetMask("Player");

        SphereCollider collider = projectile.GetComponent<SphereCollider>();
        collider.excludeLayers = LayerMask.GetMask("Player");

        if (remainingRounds == 0)
        {
            delayTime += reloadTime;
            burstIndex = 0;
            return;
        }

        burstIndex--;
        if (burstIndex > 0) delayTime += burstDelay;
        else delayTime += 1f / rateOfFire;

        //Debug.Break();
    }
}
