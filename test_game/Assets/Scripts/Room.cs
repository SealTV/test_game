using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
    public Cell CellPrefab;
    public Wall WallPrefab;

    public int Rows = 10;
    public int Columns = 10;

    public Cell[,] CellsMap = new Cell[0,0];
    public Wall[] Left = new Wall[0];
    public Wall[] Right = new Wall[0];

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
