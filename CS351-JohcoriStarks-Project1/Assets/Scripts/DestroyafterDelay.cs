using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyafterDelay : MonoBehaviour
{
    public float time = 1f;

    public void BeginDestroy()
    {
        Destroy(gameObject, time);
    }
}