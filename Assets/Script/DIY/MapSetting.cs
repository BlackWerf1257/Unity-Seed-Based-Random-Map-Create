using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSetting : MonoBehaviour
{
    [Tooltip("맵 설정")]
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private Vector2Int mapOffSet;
    [SerializeField] private GameObject roomObj;
    [SerializeField] private int startPos;
    class Room_Cell
    {
        public bool isVisited;
        public bool[] wallStatus = new bool[4];
    }

    private List<Room_Cell> rCell;

    private void Start()
    {
        rCell = new List<Room_Cell>();
        MapVirSet();
    }

    void MapVirSet()
    {
        for (int i = 0; i < mapSize.x; i++)
        {
        for (int j = 0; j < mapSize.y; j++)
            rCell.Add(new Room_Cell());
        }


    int curCellPos = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;
        while (k<1000)
        {
            k++;
            rCell[curCellPos].isVisited = true;
            if(curCellPos == rCell.Count-1)
                break;

            List<int> nearCell = CheckDuplicate(curCellPos);

            if (nearCell.Count == 0)
            {
                if (path.Count == 0)
                    break;
                else
                    curCellPos = path.Pop();
            }
            else
            {
                path.Push(curCellPos);
                int newCellPos = nearCell[Random.Range(0, nearCell.Count)];
                
                if (newCellPos > curCellPos)
                {
                    //down or right
                    if (newCellPos - 1 == curCellPos)
                    {
                        rCell[curCellPos].wallStatus[3] = true;
                        curCellPos = newCellPos;
                        rCell[curCellPos].wallStatus[2] = true;
                    }
                    else
                    {
                        rCell[curCellPos].wallStatus[1] = true;
                        curCellPos = newCellPos;
                        rCell[curCellPos].wallStatus[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCellPos + 1 == curCellPos)
                    {
                        rCell[curCellPos].wallStatus[2] = true;
                        curCellPos = newCellPos;
                        rCell[curCellPos].wallStatus[3] = true;
                    }
                    else
                    {
                        rCell[curCellPos].wallStatus[0] = true;
                        curCellPos = newCellPos;
                        rCell[curCellPos].wallStatus[1] = true;
                    }
                }
            }
        }
        MapGen();
    }

    List<int> CheckDuplicate(int cellPos)
    {
        List<int> nearCell = new List<int>();
        nearCell.Add(cellPos);
        
        //Check for Up
        if(cellPos - mapSize.x >= 0 && !rCell[cellPos-mapSize.x].isVisited)
            nearCell.Add(cellPos - mapSize.x);
        //Check for Down
        if(cellPos + mapSize.x < rCell.Count && !rCell[cellPos+mapSize.x].isVisited)
            nearCell.Add(cellPos + mapSize.x);
        //Check for Left
        if(cellPos % mapSize.x != 0 && !rCell[cellPos-1].isVisited)
            nearCell.Add(cellPos - 1);
        //Check for Right
        if((cellPos+1) % mapSize.x != 0 && !rCell[cellPos+1].isVisited)
            nearCell.Add(cellPos + 1);
        
        return nearCell;
    }

    void MapGen()
    {
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                var nRoom = Instantiate(roomObj, new Vector3(i * mapOffSet.x, 0, -j * mapOffSet.y)
                    , Quaternion.identity, transform).GetComponent<Room_Variable>();
                nRoom.MapSetting(rCell[j * mapSize.x].wallStatus);
                nRoom.name = "Room " + i + "-" + j;
            }
        }
    }
}
