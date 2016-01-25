using UnityEngine;
using System;
using Assets.Scripts.Enums;

public class Cell : MonoBehaviour
{
    public Color DefaultColor;
    public Color OnEnterColor;
    public Color StartColor;
    public Color FinishColor;

    public Assets.Scripts.Data.Cell CellData = new Assets.Scripts.Data.Cell();

    public Action<Cell> OnClick;

    private Material material;
    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
        SetColor();
    }

    public void OnMouseEnter()
    {
        material.color = OnEnterColor;
    }

    public void OnMouseExit()
    {
        SetColor();
    }

    public void OnMouseUpAsButton()
    {
        if(OnClick != null)
            OnClick(this);
    }

    private void SetColor()
    {
        switch(CellData.Type)
        {
            case CellType.Default:
                material.color = DefaultColor;
                break;
            case CellType.Start:
                material.color = StartColor;
                break;
            case CellType.Finish:
                material.color = FinishColor;
                break;
        }
    }

    public void OnGameEnd(bool result)
    {
        var collder = this.GetComponent<BoxCollider>();
        collder.enabled = false;
    }
}


