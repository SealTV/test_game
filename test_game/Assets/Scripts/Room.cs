using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
    public Cell CellPrefab;
    public Wall WallPrefab;

    public Player Player;

    public int Rows = 10;
    public int Columns = 10;

    private Cell[,] CellsMap = new Cell[0,0];
    private Wall[] Left = new Wall[0];
    private Wall[] Right = new Wall[0];

    // Use this for initialization
    void Start () {
        ClearRoom();
        InitRoom();

        Player.OnPosition += PlayerAtPositionHandler;
    }
	

    private void CellClickHandler(Cell cell)
    {
        if(Player.IsMove)
            return;

        Player.Target = new Vector3(cell.transform.position.x, 0.5f, cell.transform.position.z);
        Player.CellType = cell.CellType;
        Player.IsMove = true;
    }

    private void PlayerAtPositionHandler(CellType result)
    {
        if(result == CellType.Finish)
            Debug.Log("Finish");
    }


    public void ClearRoom()
    {
        foreach(var child in this.GetComponentsInChildren<Cell>())
        {
            if(child == null)
                continue;
            Destroy(child.gameObject);
        }

        foreach(var child in this.GetComponentsInChildren<Wall>())
        {
            if(child == null)
                continue;
            Destroy(child.gameObject);
        }
    }

    public void InitRoom()
    {
        this.CellsMap = new Cell[this.Rows, this.Columns];
        this.Left = new Wall[this.Rows];
        this.Right = new Wall[this.Columns];

        for(int i = 0; i < this.Rows; i++)
        {
            for(int j = 0; j < this.Columns; j++)
            {
                Cell cell = Instantiate(this.CellPrefab);
                cell.transform.parent = this.transform;
                cell.transform.position = new Vector3(i, 0, j);
                cell.OnClick += this.CellClickHandler;
                if(i == (this.Rows - 1) && j == 0)
                {
                    cell.CellType = CellType.Finish;
                }
                this.CellsMap[i, j] = cell;
            }
        }

        for(int i = 0; i < this.Rows; i++)
        {
            Wall wall = Instantiate(this.WallPrefab);
            wall.transform.parent = this.transform;
            wall.transform.localPosition = new Vector3(i, 0, this.Columns - 0.45f);
            this.Left[i] = wall;
        }

        for(int i = 0; i < this.Columns; i++)
        {
            Wall wall = Instantiate(this.WallPrefab);
            wall.transform.parent = this.transform;
            wall.transform.localPosition = new Vector3(this.Rows - 0.45f, 0, i);
            wall.transform.Rotate(new Vector3(0, 90, 0));
            this.Right[i] = wall;
        }
    }
}
