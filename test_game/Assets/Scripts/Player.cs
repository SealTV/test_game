using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {
    public float Speed;

    [HideInInspector]
    public Vector3 Target;

    [HideInInspector]
    public bool IsMove;

    [HideInInspector]
    public CellType CellType;

    public Action<CellType> OnPosition;
	
	// Update is called once per frame
	void Update () {
        if(IsMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
            if(transform.position == Target)
            {
                IsMove = false;
                if(OnPosition != null)
                    OnPosition(this.CellType);
            }
        }
	}


}
