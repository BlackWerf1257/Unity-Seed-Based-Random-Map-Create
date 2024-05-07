using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Seed_Change : MonoBehaviour
{
    [Serializable]
    public struct PlayerStats
    {
        [SerializeField] int movementSpeed;
        public int hitPoints;
        public bool hasHealthPotion;
    }

    //Use [SerializeField] attribute to ensure that the private field is serialized
    
    private PlayerStats stats;
    
    [SerializeField] private int seed;
    
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(seed);
    }

}
