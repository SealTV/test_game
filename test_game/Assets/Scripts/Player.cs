using UnityEngine;
using System;
using Assets.Scripts.Enums;

public class Player : MonoBehaviour
{
    public float Speed;


    [HideInInspector]
    public bool IsMove;
    public Action<CellType> OnPosition;
    public Action OnBallTrigger;

    private int currentStep;
    private Assets.Scripts.Data.Cell[] route;

    private Vector3 target;

    public bool IsAlive;

    // Update is called once per frame
    void Update()
    {
        if(!IsAlive)
            return;

        if(IsMove)
        {
            target = new Vector3(route[currentStep].I, transform.position.y, route[currentStep].J);

            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            if(transform.position == target)
            {
                if(currentStep == route.Length - 1)
                {
                    IsMove = false;
                    if(OnPosition != null)
                        OnPosition(route[currentStep].Type);
                }
                else
                {
                    currentStep++;
                }
            }
        }
    }

    internal void Move(Assets.Scripts.Data.Cell[] route, int step = 0)
    {
        IsMove = true;
        this.route = route;
        currentStep = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!IsAlive)
            return;

        IsAlive = false;
        IsMove = false;

        if(OnBallTrigger != null)
            OnBallTrigger();
    }
}
