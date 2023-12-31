using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [Header("Inscribed")]
    public float rateOfFire = 1.0f;
    [Range(0f, 180f)]
    public float spreadAngle = 1.0f;
    public float reloadTime = 1.0f;
    public int magazineSize = 10;

    public int roundsPerBurst = 1;
    public float burstDelay = 0.1f;
    
    public bool hasAutomatic = false;

    public float exitVelocity = 10.0f;
    public GameObject projectilePrefab;

    public Entity parent;


    [Header("Dynamic")]
    [SerializeField]
    private bool triggerHeld = false;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private int burstIndex;
    [SerializeField]
    private int remainingRounds;

    private AudioSource audioSource;
    
    void Awake()
    {
        remainingRounds = magazineSize;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        delayTime = Mathf.Max(delayTime - Time.deltaTime, 0);

        if (remainingRounds == 0 && delayTime <= 0f) remainingRounds = magazineSize;
		
        if (remainingRounds != 0 && ((hasAutomatic && triggerHeld) || burstIndex > 0) && delayTime <= 0f) Fire();
    }

    public void SetTrigger(bool trigger)
    {
        triggerHeld = trigger;

        if (trigger && remainingRounds > 0 && burstIndex == 0 && delayTime <= 0f)
        {
            burstIndex = roundsPerBurst;
            Fire();
        }
    }

    private void Fire()
    {
        remainingRounds--;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        
        Quaternion spreadRotation = Quaternion.AngleAxis(Random.Range(0, spreadAngle) - spreadAngle / 2, Vector3.up);
        rigidbody.velocity = (spreadRotation * transform.forward) * (exitVelocity + parent.GetSpeed());
        rigidbody.excludeLayers = (1 << parent.gameObject.layer);

        Collider collider = projectile.GetComponent<Collider>();
        collider.excludeLayers = (1 << parent.gameObject.layer);

        audioSource.Play();

        if (remainingRounds == 0)
        {
            delayTime += reloadTime;
            burstIndex = 0;
            return;
        }

        burstIndex--;
        if (burstIndex > 0) delayTime += burstDelay;
        else delayTime += 1f / rateOfFire;
    }
}
