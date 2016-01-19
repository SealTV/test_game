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

            room.InitRoom();
        }
    }
}
