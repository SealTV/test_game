using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public float Speed;
    public Vector3 Direction;
    public bool IsMove;

    private Transform mTransform;

    void Start()
    {
        mTransform = transform;
    }

	// Update is called once per frame
	void Update () {
        if(IsMove)
        {
            mTransform.position += Direction * Time.deltaTime;
        }
	}
}
