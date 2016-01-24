using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {

    public Text text;

    public void OpenResultPanel(bool isSuccess)
    {
        text.text = isSuccess ? "Success! =)" : "Fail =( ";
        this.gameObject.SetActive(true);
    }
}
