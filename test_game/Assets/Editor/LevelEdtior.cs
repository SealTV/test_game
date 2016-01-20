using UnityEngine;
using System.Collections;

public class LevelEdtior : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGUI()
    {
        GUI.Button(new Rect(10, 10, 50, 50), new GUIContent("botton"));
    }
}
