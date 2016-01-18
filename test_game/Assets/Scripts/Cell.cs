using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    public Color DefaultColor;
    public Color OnEnterColor;

    private Material material;
    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseEnter()
    {
        material.color = OnEnterColor;
    }

    public void OnMouseExit()
    {
        material.color = DefaultColor;
    }
}
