using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Cell))]
public class CellEditor :  Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //if(GUILayout.Button("Обновить"))
        //{
        //    Cell cell = (Cell)target;
        //}
    }
}
