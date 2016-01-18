using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class RoomEditior : Editor
{
    public override void OnInspectorGUI()
    { //Сообщаем редактору, что этот инспектор заменит прежний (встроеный)
        DrawDefaultInspector();


        if(GUILayout.Button("Обновить поле"))
        {
            Room room = (Room)target;
            room.CellsMap = new Cell[room.Rows, room.Columns];
            room.Left = new Wall[room.Rows];
            room.Right = new Wall[room.Columns];

            foreach(var child in room.GetComponentsInChildren<Cell>())
            {
                if(child == null)
                    continue;
                DestroyImmediate(child.gameObject);
            }

            foreach(var child in room.GetComponentsInChildren<Wall>())
            {
                if(child == null)
                    continue;
                DestroyImmediate(child.gameObject);
            }



            for(int i = 0; i < room.Rows; i++)
            {
                for(int j = 0; j < room.Columns; j++)
                {
                    Cell cell = Instantiate(room.CellPrefab);
                    cell.transform.parent = room.transform;
                    cell.transform.position = new Vector3(i, 0, j);
                    room.CellsMap[i, j] = cell;
                }
            }

            for(int i = 0; i < room.Rows; i++)
            {
                Wall wall = Instantiate(room.WallPrefab);
                wall.transform.parent = room.transform;
                wall.transform.localPosition = new Vector3(i, 0, room.Columns - 0.45f);
                room.Left[i] = wall;
            }

            for(int i = 0; i < room.Columns; i++)
            {
                Wall wall = Instantiate(room.WallPrefab);
                wall.transform.parent = room.transform;
                wall.transform.localPosition = new Vector3(room.Rows - 0.45f, 0, i);
                wall.transform.Rotate(new Vector3(0, 90, 0));
                room.Left[i] = wall;
            }
        }
    }
}
