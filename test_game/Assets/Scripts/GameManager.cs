using UnityEngine;
using Assets.Scripts.Data;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;

public class GameManager : MonoBehaviour
{
    public Room Room;
    List<Level> levels;

    public GameObject MainPanel;
    public GameObject GamePanel;
    public ResultPanel ResultPanel;

    private int lastSelectedLevelId;

    void Start()
    {
        TextAsset[] levesFiles = Resources.LoadAll<TextAsset>("Levels");
        levels = new List<Level>();
        foreach(var file in levesFiles)
        {
            Debug.LogError(file.text);

            Level level = null;
            using(var stream = new MemoryStream(file.bytes))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Level));
                var encoding = Encoding.GetEncoding("UTF-8");
                using(StreamReader reader = new StreamReader(stream, encoding))
                    level = serializer.Deserialize(reader) as Level;
            }

            levels.Add(level);
        }
        this.Room.OnGameEndAction += EndLevel;

        this.Room.ClearRoom();
        this.Room.gameObject.SetActive(false);
        this.MainPanel.SetActive(true);
        this.GamePanel.SetActive(false);
        this.ResultPanel.gameObject.SetActive(false);
    }

    private void EndLevel(bool isSuccess)
    {
        this.ResultPanel.OpenResultPanel(isSuccess);
    }


    public void RestartLevel()
    {
        StartLevel(lastSelectedLevelId);
    }

    public void StartLevel(int levelId)
    {
        lastSelectedLevelId = levelId;
        this.Room.InitRoom(levels[levelId]);
        this.Room.gameObject.SetActive(true);
        this.MainPanel.SetActive(false);
        this.GamePanel.SetActive(true);
        this.ResultPanel.gameObject.SetActive(false);
    }

    public void ExitLevel()
    {
        Room.OnGameEnd();

        this.Room.ClearRoom();
        this.Room.gameObject.SetActive(false);
        this.MainPanel.SetActive(true);
        this.GamePanel.SetActive(false);
        this.ResultPanel.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
