using UnityEngine;
using System.Collections.Generic;

public class BallsBuffer : MonoBehaviour {

    public Ball PallPrefab;
    private Queue<Ball> ballsQeue;

    void Awake()
    {
        ballsQeue = new Queue<Ball>();
    }

    public Ball Dequeue()
    {
        if(ballsQeue.Count > 0)
            return ballsQeue.Dequeue();

        Ball ball = Instantiate(PallPrefab);
        return ball;
    }

    public void Enqueue(Ball ball)
    {
        ball.transform.parent = this.transform;
        ball.gameObject.SetActive(false);

        ballsQeue.Enqueue(ball);
    }

}
