using UnityEngine;
using System.Collections.Generic;
using System;

public class BallsBuffer : MonoBehaviour {

    public Ball PallPrefab;
    private Queue<Ball> ballsQeue;
    private Action OnEndGame;

    void Awake()
    {
        ballsQeue = new Queue<Ball>();
    }

    public Ball Dequeue()
    {
        if(ballsQeue.Count > 0)
            return ballsQeue.Dequeue();

        Ball ball = Instantiate(PallPrefab);
        ball.OnEndMove += Enqueue;
        OnEndGame += ball.OnGameEnd;
        return ball;
    }

    private void Enqueue(Ball ball)
    {
        ball.IsMove = false;
        ball.transform.parent = this.transform;
        ball.gameObject.SetActive(false);

        ballsQeue.Enqueue(ball);
    }

    public void OnGameEnd(bool result)
    {
        if(OnEndGame != null)
            OnEndGame();
    }

}
