using UnityEngine;
using System.Collections;
using System;

public class Cell : MonoBehaviour
{
    public Color DefaultColor;
    public Color OnEnterColor;
    public Color FinishColor;


    public CellType CellType;

    public Action<Cell> OnClick;

    private Material material;
    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = this.CellType == CellType.Finish ? FinishColor : DefaultColor;
    }

    public void OnMouseEnter()
    {
        material.color = OnEnterColor;
    }

    public void OnMouseExit()
    {
        material.color = this.CellType == CellType.Finish ? FinishColor : DefaultColor;
    }

    public void OnMouseUpAsButton()
    {
        if(OnClick != null)
            OnClick(this);
    }
}


public enum CellType
{
    Default,
    Target,
    Finish,
}
