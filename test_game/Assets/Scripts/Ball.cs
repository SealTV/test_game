using UnityEngine;
using System;

public class Ball : MonoBehaviour {
    public float Speed;
    public Vector3 Direction;
    public bool IsMove;

    public Action<Ball> OnEndMove;


	// Update is called once per frame
	void Update () {
        if(IsMove)
        {
            transform.position += Direction * Time.deltaTime * Speed;

            if((transform.position.x < -10 || transform.position.z < -10) && OnEndMove != null)
                OnEndMove(this);
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        OnEndMove(this);
    }

    public void OnGameEnd()
    {
        OnEndMove(this);
    }
}
