using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;
using Assets.Scripts.Data;
using System.Text;

[CustomEditor(typeof(Room))]
public class RoomEditior : Editor
{
    private const string LevelsRootFolder = "Assets/Resources/Levels";
    public string[] Levels;
    private static int selectedIndex;


    public override void OnInspectorGUI()
    { //Сообщаем редактору, что этот инспектор заменит прежний (встроеный)
        var room = target as Room;
        DrawDefaultInspector();

        this.Levels = GetLevels();
        selectedIndex = EditorGUILayout.Popup(selectedIndex, Levels);

        if(GUILayout.Button("Загрузить уровень"))
        {
            LoadLevel(room);
        }

        if(GUILayout.Button("Сбросить уровень уровень"))
        {
            ResetLevel(room);
        }
        
        if(GUILayout.Button("Добавить уровень"))
        {
            File.Create(LevelsRootFolder + "/level" + Levels.Length + ".json");
            Levels = GetLevels();
            LoadLevel(room);

        }

        if(GUILayout.Button("Сохранить уровень"))
        {
            SaveItemInfo(room);
        }

        if(GUILayout.Button("Удалить уровень"))
        {
            File.Delete(Levels[selectedIndex]);
            Levels = GetLevels();
        }
    }

    private string[] GetLevels()
    {
        return Directory.GetFiles(LevelsRootFolder, "*.json");
    }

    private void LoadLevel(Room room)
    {
        Level level = null;
        using(var file = new FileStream(Levels[selectedIndex], FileMode.Open))
        {
            var serializer = new XmlSerializer(typeof(Level));
            level = serializer.Deserialize(file) as Level;
        }

        foreach(var child in room.GetComponentsInChildren<Cell>())
        {
            DestroyImmediate(child.gameObject);
        }
        foreach(var child in room.GetComponentsInChildren<Wall>())
        {
            DestroyImmediate(child.gameObject);
        }

        room.InitRoom(level);
    }

    private void ResetLevel(Room room)
    {
        foreach(var child in room.GetComponentsInChildren<Cell>())
        {
            DestroyImmediate(child.gameObject);
        }
        foreach(var child in room.GetComponentsInChildren<Wall>())
        {
            DestroyImmediate(child.gameObject);
        }

        room.InitRoom();
    }

    private void SaveItemInfo(Room room)
    {
        Level level = new Level();
        level.Rows = room.Rows;
        level.Columns = room.Columns;

        level.Cells = new Assets.Scripts.Data.Cell[room.Rows][];
        for(int i = 0; i < room.Rows; i++)
        {
            level.Cells[i] = new Assets.Scripts.Data.Cell[room.Columns];
        }

        foreach(var child in room.GetComponentsInChildren<Cell>())
        {
            level.Cells[child.CellData.I][child.CellData.J] = child.CellData;
        }

        level.Walls = new Assets.Scripts.Data.Wall[2][];
        level.Walls[0] = new Assets.Scripts.Data.Wall[room.Rows];
        level.Walls[1] = new Assets.Scripts.Data.Wall[room.Columns];

        foreach(var child in room.GetComponentsInChildren<Wall>())
        {
            level.Walls[child.WallData.I][child.WallData.J] = child.WallData;
        }

        var encoding = Encoding.GetEncoding("UTF-8");
        using(var file = new FileStream(Levels[selectedIndex], FileMode.OpenOrCreate))
        {
            file.SetLength(0);
            var serializer = new XmlSerializer(typeof(Level));
            using(StreamWriter writer = new StreamWriter(file, encoding))
                serializer.Serialize(writer, level);
        }
    }
}
