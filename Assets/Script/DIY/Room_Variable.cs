using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Variable : MonoBehaviour
{
   [SerializeField] private GameObject[] doorObj, wallObj;

   public void MapSetting(bool[] value)
   {
      for (int i = 0; i < doorObj.Length; i++)
      {
         doorObj[i].SetActive(value[i]);
         wallObj[i].SetActive(value[i]);
      }
   }
}
