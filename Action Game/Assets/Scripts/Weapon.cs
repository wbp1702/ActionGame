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

    private float delayTime;
    private int burstIndex;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delayTime = Mathf.Max(delayTime - Time.deltaTime, 0);

        if (burstIndex > 0 && delayTime <= 0f)
        {
            Fire();

            burstIndex--;
            if (burstIndex > 0) delayTime += burstDelay;
            else delayTime += 1f / rateOfFire;
        }
        else if (hasAutomatic && triggerHeld && delayTime <= 0f)
        {
            Fire();


            delayTime += 1f / rateOfFire;
        }
    }

    public void SetTrigger(bool trigger)
    {
        triggerHeld = trigger;

        if (trigger && delayTime <= 0f)
        {
            Fire();
            
            if (burstCount > 1)
            {
                burstIndex = burstCount - 1;
                delayTime += burstDelay;
            }
            else delayTime += 1f / rateOfFire;
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        projectile.transform.position = transform.position;
        Quaternion spreadRotation = Quaternion.Euler(0, Random.Range(0, spreadAngle), 0);
        rigidbody.velocity = spreadRotation * transform.forward * exitVelocity;
        rigidbody.excludeLayers = LayerMask.GetMask("Player");
    }
}
