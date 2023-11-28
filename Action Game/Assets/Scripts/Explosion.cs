using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float diameter = 10;
    public float fadeInTime = 0.5f;
    public float fadeOutTime = 2f;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * diameter;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float factor;
        if (time < fadeInTime) factor = (time / fadeInTime);
        else factor = ((fadeOutTime - (time - fadeInTime)) / fadeOutTime);
        transform.localScale = Vector3.one * diameter * factor;

        if (factor < 0) Destroy(gameObject);
    }
}
