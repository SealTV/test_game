using UnityEngine;
using Assets.Scripts.Enums;
using System;
using Assets.Scripts.Data;

public class Room : MonoBehaviour {
    public Cell CellPrefab;
    public Wall WallPrefab;
    public BallsBuffer BallsBuffer;

    public Player Player;

    public int Rows = 10;
    public int Columns = 10;

    public Cell[][] CellsMap = new Cell[0][];
    [HideInInspector]
    public Wall[][] Walls = new Wall[0][];

    public Action<bool> OnGameEndAction;

    // Use this for initialization
    void Start () {
        Player.OnPosition += PlayerAtPositionHandler;
        Player.OnBallTrigger += OnGameEnd;
    }

    private Assets.Scripts.Data.Cell LastCell;
    private WaveRouteSearch wave;

    private void CellClickHandler(Cell cell)
    {
        if(Player.IsMove)
            return;
        var route = wave.SearchRoute(LastCell, cell.CellData);

        LastCell = cell.CellData;

        Player.Move(route);
        OnGameEndAction += BallsBuffer.OnGameEnd;
    }

    private void PlayerAtPositionHandler(CellType result)
    {
        if(result == CellType.Finish && OnGameEndAction != null)
            OnGameEndAction(true);
    }

    public void OnGameEnd()
    {
        OnGameEndAction(false);
    }


    public void ClearRoom()
    {
        foreach(var child in this.GetComponentsInChildren<Cell>())
        {
            this.OnGameEndAction -= child.OnGameEnd;
            Destroy(child.gameObject);
        }

        foreach(var child in this.GetComponentsInChildren<Wall>())
        {
            this.OnGameEndAction -= child.OnGameEnd;
            Destroy(child.gameObject);
        }
    }

    public void InitRoom()
    {
        ClearRoom();

        this.CellsMap = new Cell[this.Rows][];
        this.Walls = new Wall[this.Rows][];

        for(int i = 0; i < this.Rows; i++)
        {
            this.CellsMap[i] = new Cell[this.Columns];
            this.Walls[i] = new Wall[this.Columns];

            for(int j = 0; j < this.Columns; j++)
            {
                Cell cell = Instantiate(this.CellPrefab);
              
                cell.transform.parent = this.transform;
                cell.transform.position = new Vector3(i, 0, j);
                cell.OnClick += this.CellClickHandler;
              
                this.CellsMap[i][j] = cell;
                this.CellsMap[i][j].CellData = new Assets.Scripts.Data.Cell
                {
                    I = i,
                    J = j,
                    Type = CellType.Default
                };
            }
        }

        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < (i == 0 ? this.Rows : this.Columns); j++)
            {
                Wall wall = Instantiate(this.WallPrefab);
                wall.WallData.I = i;
                wall.WallData.J = j;
                wall.WallData.Type = WallType.Default;
                wall.Buffer = BallsBuffer;

                wall.transform.parent = this.transform;
                wall.transform.localPosition = i == 0
                    ? new Vector3(j, 0, this.Columns - 0.45f)
                    : new Vector3(this.Rows - 0.45f, 0, j);
            
                if(i == 1)
                    wall.transform.Rotate(new Vector3(0, 90, 0));
                this.Walls[i][j] = wall;
            }
        }
    }

    public void InitRoom(Assets.Scripts.Data.Level level)
    {
        this.Rows = level.Rows;
        this.Columns = level.Columns;

        this.CellsMap = new Cell[level.Rows][];
        wave = new WaveRouteSearch(level.Cells, level.Rows, level.Columns);

        for(int i = 0; i < this.Rows; i++)
        {
            this.CellsMap[i] = new Cell[this.Columns];

            for(int j = 0; j < this.Columns; j++)
            {
                if(level.Cells[i][j] == null)
                    continue;

                Cell cell = Instantiate(this.CellPrefab);
                cell.CellData = level.Cells[i][j];

                cell.transform.parent = this.transform;
                cell.transform.position = new Vector3(i, 0, j);

                if(level.Cells[i][j].Type == CellType.Start)
                {
                    Player.transform.position = new Vector3(i, Player.transform.position.y, j);
                    LastCell = cell.CellData;
                }

                cell.OnClick += this.CellClickHandler;

                this.CellsMap[i][j] = cell;

                this.OnGameEndAction += cell.OnGameEnd;
            }
        }

        this.Walls = new Wall[2][] { new Wall[this.Rows], new Wall[this.Columns] };

        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < (i == 0 ? this.Rows : this.Columns); j++)
            {
                Wall wall = Instantiate(this.WallPrefab);
                wall.WallData = level.Walls[i][j];

                wall.Buffer = BallsBuffer;

                wall.transform.parent = this.transform;
                wall.transform.localPosition = i == 0
                    ? new Vector3(j, 0, this.Columns - 0.45f)
                    : new Vector3(this.Rows - 0.45f, 0, j);

                if(i == 1)
                    wall.transform.Rotate(new Vector3(0, 90, 0));


                wall.FireDirection = i == 0
                    ? new Vector3(0, 0, -10)
                    : new Vector3(-10, 0, 0);
                this.Walls[i][j] = wall;
                this.OnGameEndAction += wall.OnGameEnd;
            }
        }

        Player.IsAlive = true;
    }
}
