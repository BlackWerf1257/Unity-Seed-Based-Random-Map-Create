using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Generator : MonoBehaviour
{
    [SerializeField] Transform parentObj;
    private List<Transform> floorObj;
    [SerializeField] public List<GameObject> enemyObj;
    [SerializeField] public List<GameObject> trapObj;
    [SerializeField] public List<GameObject> itemObj;

    private List<Transform> objTrans = new List<Transform>(0);

    [SerializeField] private int mapSize;

    [SerializeField] int propCount = 4;
    [SerializeField] int enemyCount = 10;
    [SerializeField] int trapCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        //Prop
        MapSetting(itemObj, propCount);
        //Trap
        MapSetting(trapObj, trapCount);
        //Enemy
        MapSetting(enemyObj, enemyCount);
        
    }

    void MapSetting(List<GameObject> obj, int objCnt)
    {
        for (int i = 0; i < objCnt; i++)
        {
            Vector3 pos = Random.insideUnitSphere * mapSize;

                if (objTrans.Count != 0)
                {
                    for (int objIdx = 0; i < objTrans.Count; objIdx++)
                    {
                        if (!(Mathf.Abs(objTrans[objIdx].position.x - pos.x) > 10) ||
                            !(Mathf.Abs(objTrans[objIdx].position.z - pos.z) > 10))
                            break;
                        else MapSetting(obj, objCnt);
                    }
                }

                pos.y = 1f;
            GameObject selected = obj[Random.Range(0, obj.Count)];
            
            GameObject ground = Instantiate(selected, pos, Quaternion.identity);
            objTrans.Add(ground.transform);
            ground.transform.localScale = Vector3.one;
            ground.transform.parent = parentObj;
            ground.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
          
            StartCoroutine(DestoryRigidbody(ground.GetComponent<Rigidbody>()));
        }

        IEnumerator DestoryRigidbody(Rigidbody rigid)
        {
            yield return new WaitForSeconds(.5f);
            Destroy(rigid);
        }
    }
}
