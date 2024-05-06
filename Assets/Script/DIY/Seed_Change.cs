using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_Change : MonoBehaviour
{
    [SerializeField] private int seed;
    
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(seed);
    }

}
