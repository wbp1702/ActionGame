using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType { center, inset, outset };

    [System.Flags]
    public enum eScreenLocs
    {
        onScreen = 0,
        offRight = 1,
        offLeft = 2,
        offUp = 4,
        offDown = 8,
    }

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        float checkRadius = boundsType switch
        {
            eType.inset => -radius,
            eType.outset => radius,
            _ => 0
        };

        Vector3 position = transform.position;
        screenLocs = eScreenLocs.onScreen;

        if (position.x > camWidth + checkRadius)
        {
            position.x = camWidth + checkRadius;
            screenLocs |= eScreenLocs.offRight;
        }
        if (position.x < -camWidth - checkRadius)
        {
            position.x = -camWidth - checkRadius;
            screenLocs |= eScreenLocs.offLeft;
        }
        if (position.y > camHeight + checkRadius)
        {
            position.y = camHeight + checkRadius;
            screenLocs |= eScreenLocs.offUp;
        }
        if (position.y < -camHeight - checkRadius)
        {
            position.y = -camHeight - checkRadius;
            screenLocs |= eScreenLocs.offDown;
        }

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = position;
            screenLocs = eScreenLocs.onScreen;
        }

    }

    public bool isOnScreen { get { return screenLocs == eScreenLocs.onScreen; } }

    public bool locIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return ((screenLocs & checkLoc) == checkLoc);
    }
}
