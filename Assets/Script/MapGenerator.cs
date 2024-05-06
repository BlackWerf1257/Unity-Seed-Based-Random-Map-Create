using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool isVisited = false;
        public bool[] status = new bool[4];
    }

    
    [Tooltip("맵 사이즈")]
    [SerializeField] private Vector2Int size;
    [SerializeField] private int startPos = 0;

    [SerializeField] private GameObject room;
    [SerializeField] private Vector2 offSet;

    private List<Cell> board;
    
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();

    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var newRoom =
                    Instantiate(room, new Vector3(i * offSet.x, 0, -j * offSet.y), Quaternion.identity, transform)
                        .GetComponent<Room_Variable>();
                newRoom.MapSetting(board[Mathf.FloorToInt(i+j*size.x)].status);
                newRoom.name = "Room " + i + "-" + j;
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();
        for(int i=0; i<size.x; i++)
            for(int j=0; j<size.y; j++)
                board.Add(new Cell());

        int curCell = startPos;
        Stack<int> path = new Stack<int>();

        //어느 루프에 있는지 추적용 변수
        int k = 0;
        //던전 크기에 따라 수정하기
        
        while (k<1000)
        {
            k++;

            board[curCell].isVisited = true;

            if(curCell == board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbor(curCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    curCell = path.Pop();
                }
            }
            else
            {
                path.Push(curCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > curCell)
                {
                    //down or right
                    if (newCell - 1 == curCell)
                    {
                        board[curCell].status[3] = true;
                        curCell = newCell;
                        board[curCell].status[1] = true;
                    }
                    else
                    {
                        board[curCell].status[1] = true;
                        curCell = newCell;
                        board[curCell].status[3] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == curCell)
                    {
                        board[curCell].status[2] = true;
                        curCell = newCell;
                        board[curCell].status[0] = true;
                    }
                    else
                    {
                        board[curCell].status[0] = true;
                        curCell = newCell;
                        board[curCell].status[2] = true;
                    }
                }

            }

        }
        
        /*while (k < 1000)
        {
            k++;

            board[curCell].isVisited = true;

            //주변 셀 확인
            List<int> neighbors = CheckNeighbor(curCell);
            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                    break;
                else
                    curCell = path.Pop();
            }
            else
            {
                path.Push(curCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                if (newCell > curCell)
                {
                    //Down or R
                    if (newCell - 1 == curCell)
                    {
                        board[curCell].status[2] = true;
                        curCell = newCell;
                        board[curCell].status[3] = true;
                    }
                    else
                    {
                        board[curCell].status[1] = true;
                        curCell = newCell;
                        board[curCell].status[0] = true;
                    }
                }
                else
                {
                    //Up or Left
                    if (newCell + 1 == curCell)
                    {
                        board[curCell].status[3] = true;
                        curCell = newCell;
                        board[curCell].status[2] = true;
                    }
                    else
                    {
                        board[curCell].status[0] = true;
                        curCell = newCell;
                        board[curCell].status[1] = true;
                    }
                }
            }
        }*/
        GenerateDungeon();
    }

    List<int> CheckNeighbor(int cell)
    {
        List<int> neightbors = new List<int>();
        //위쪽 확인
        if (cell - size.x >= 0 && !board[cell - size.x].isVisited)
            neightbors.Add(Mathf.FloorToInt(cell - size.x));
        //아래쪽 확인
        if (cell + size.x <= board.Count && !board[cell + size.x].isVisited)
            neightbors.Add(Mathf.FloorToInt(cell + size.x));
        
        //왼쪽 확인
        if (cell % size.x != 0 && !board[cell - 1].isVisited)
            neightbors.Add(Mathf.FloorToInt(cell - 1));
        
        //오른쪽 확인
        if ((cell+1) % size.x != 0 && !board[cell + 1].isVisited)
            neightbors.Add(Mathf.FloorToInt(cell + 1));

        return neightbors;
    }
}
