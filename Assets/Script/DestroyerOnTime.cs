using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOnTime : MonoBehaviour
{
    public float lifetime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
