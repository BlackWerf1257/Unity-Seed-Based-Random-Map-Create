using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Generator : MonoBehaviour
{
    [SerializeField] Transform parentObj;
    private List<Transform> floorObj;
    [SerializeField] public List<GameObject> wallObj;
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
        MapSetting(trapObj, propCount);
        
        /*for(int i = 0; i < GroundObjectCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 7;
            pos.y = 0;
            GameObject selected = GroundObjects[Random.Range(0, GroundObjects.Length)];
            
            GameObject ground = Instantiate(selected, pos, selected.transform.rotation);
            ground.transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
        }
        for (int i = 0; i < RockObjectCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 7;
            pos.y = 20;
            RaycastHit hit;
            if (Physics.Raycast(pos, Vector3.down, out hit))
            {
                GameObject selected = RockObjects[Random.Range(0, RockObjects.Length)];

                GameObject ground = Instantiate(selected, hit.point, selected.transform.rotation);
                ground.transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
            }
        }
        for (int i = 0; i < TreeObjectCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 7;
            pos.y = 20;
            RaycastHit hit;
            if (Physics.Raycast(pos, Vector3.down, out hit))
            {
                GameObject selected = TreeObjects[Random.Range(0, TreeObjects.Length)];

                GameObject ground = Instantiate(selected, hit.point, selected.transform.rotation);
                ground.transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
            }
        }*/

    }

    void MapSetting(List<GameObject> obj, int objCnt)
    {
        
        
        for (int i = 0; i < objCnt; i++)
        {
            Vector3 pos;

                pos = Random.insideUnitSphere * mapSize;

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
            ground.AddComponent<Rigidbody>();
            
            if (ground.transform.childCount == 0)
            {
                ground.AddComponent<BoxCollider>();
                StartCoroutine(DestoryRigidbody(ground.GetComponent<Rigidbody>(), ground.GetComponent<BoxCollider>()));
            }
            else
            {
                ground.transform.GetChild(1).gameObject.AddComponent<BoxCollider>();
                StartCoroutine(DestoryRigidbody(ground.GetComponent<Rigidbody>(), ground.transform.GetChild(1).
                    gameObject.GetComponent<BoxCollider>()));
            }
            
            
        }

        IEnumerator DestoryRigidbody(Rigidbody rigid, BoxCollider collider)
        {
            yield return new WaitForSeconds(.5f);
            //Destroy(collider);
            //Destroy(rigid);
        }
    }
        //Prop
        //Trap
        //Enemy
}
