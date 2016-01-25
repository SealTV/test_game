using Assets.Scripts.Enums;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject Default;
    public GameObject Gun;
    public BallsBuffer Buffer;

    public Vector3 FireDirection;
    public Assets.Scripts.Data.Wall WallData = new Assets.Scripts.Data.Wall();
    private float nextFire = 0.5f;
    private bool isActive = true;

    // Use this for initialization
    void Start()
    {
        nextFire = Time.time;
        switch(WallData.Type)
        {
            case WallType.Default:
                Gun.SetActive(false);
                Default.SetActive(true);
                break;
            case WallType.Gun:
                Gun.SetActive(true);
                Default.SetActive(false);
                break;
        }
    }

    public void Update()
    {
        if(!isActive)
            return;

        if(WallData.Type == WallType.Gun && Time.time >= nextFire)
        {
            nextFire += WallData.FireSpeed;
            var ball = Buffer.Dequeue();

            ball.transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
            ball.Direction = FireDirection;
            ball.Speed = WallData.BollSpeed;
            ball.IsMove = true;
            ball.gameObject.SetActive(true);
        }
    }

    public void OnGameEnd(bool result)
    {
        isActive = false;
    }
}


