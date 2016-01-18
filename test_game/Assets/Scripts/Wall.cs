using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject Default;
    public Gun Gun;

    public WallType WallType;
    // Use this for initialization
    void Start()
    {
        switch(this.WallType)
        {
            case WallType.Default:
                Gun.gameObject.SetActive(false);
                break;
            case WallType.Gun:
                Default.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum WallType
{
    Default,
    Gun
}
