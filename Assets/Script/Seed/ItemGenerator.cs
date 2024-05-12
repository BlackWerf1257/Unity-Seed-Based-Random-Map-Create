using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemGenerator : MonoBehaviour
{
    Transform parentObj;
    private List<Transform> floorObj;
    [SerializeField] public List<GameObject> enemyObj;
    [SerializeField] public List<GameObject> trapObj;
    [SerializeField] public List<GameObject> itemObj;

    [HideInInspector]
    public List<Transform> objTrans = new List<Transform>(0);
    public Transform SetParentTrans
    {
        get => parentObj;
        set => parentObj = value;
    }

    public int roomSize { get; set; }



    [SerializeField] int maxPropCount = 4;
    [SerializeField] int maxEnemyCount = 10;
    [SerializeField] int maxTrapCount = 5;
    // Start is called before the first frame update
    public void ItemCreate()
    {
        //Prop
        //MapSetting(itemObj, Random.Range(0, maxPropCount));
        //Trap
        MapSetting(trapObj, Random.Range(0, maxTrapCount));
        //Enemy
        //MapSetting(enemyObj, Random.Range(0, maxEnemyCount));
        
    }

    void MapSetting(List<GameObject> obj, int objCnt)
    {
        for (int i = 0; i < objCnt; i++)
        {
            Vector3 pos =  Random.insideUnitSphere * roomSize;

                if (objTrans.Count != 0)
                {
                    for (int objIdx = 0; i < objTrans.Count; objIdx++)
                    {
                        if (!(Mathf.Abs(objTrans[objIdx].position.x - pos.x) > 5) ||
                            !(Mathf.Abs(objTrans[objIdx].position.z - pos.z) > 5))
                            break;
                        else MapSetting(obj, objCnt);
                    }
                }

                pos.y = 1f;
            GameObject selected = obj[Random.Range(0, obj.Count)];
            
            GameObject ground = Instantiate(selected, Vector3.zero, Quaternion.identity, SetParentTrans);
            ground.transform.localPosition = pos;
            objTrans.Add(ground.transform);
            ground.transform.localScale = Vector3.one;
            ground.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
          
            StartCoroutine(DestoryRigidbody(ground.GetComponent<Rigidbody>()));
        }

        IEnumerator DestoryRigidbody(Rigidbody rigid)
        {
            yield return new WaitForSeconds(2f);
            Destroy(rigid);
        }
    }
}
