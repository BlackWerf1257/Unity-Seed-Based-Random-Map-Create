using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Hide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
            Destroy(other.gameObject);
    }
}
