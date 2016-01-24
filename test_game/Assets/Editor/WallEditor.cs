using UnityEditor;

[CustomEditor(typeof(Wall))]
public class WallEditior : Editor
{
    private const string LevelsRootFolder = "Assets/Resources/Levels";
    public string[] Levels;
    private int selectedIndex;


    public override void OnInspectorGUI()
    { 
        DrawDefaultInspector();
        var wall = target as Wall;
        if(wall.WallData.Type == Assets.Scripts.Enums.WallType.Default)
        {
            wall.Default.SetActive(true);
            wall.Gun.gameObject.SetActive(false);
        }
        else
        {
            wall.Default.SetActive(false);
            wall.Gun.gameObject.SetActive(true);
        }
    }

}
