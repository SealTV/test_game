using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Room))]
public class RoomEditior : Editor
{
    public override void OnInspectorGUI()
    { //Сообщаем редактору, что этот инспектор заменит прежний (встроеный)
        DrawDefaultInspector();


        if(GUILayout.Button("Обновить поле"))
        {
            Room room = (Room)target;

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

            var level = Resources.Load("level_1");

            SaveItemInfo();
            room.InitRoom();
        }
    }

    public void SaveItemInfo()
    {
        string path = "Assets/Resources/level_1.json";

        string str = string.Empty;
        using(FileStream fs = new FileStream(path, FileMode.Open))
        {
            using(StreamReader reader = new StreamReader(fs))
            {
                str = reader.ReadToEnd();
                Debug.Log(str);
            }
        }

        str = @"{
    rows : 10,
    columns : 10,
    array : []
}
";
        using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            using(StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(str);
            }
        }
    }
}
