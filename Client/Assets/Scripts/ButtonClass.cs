using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClass : MonoBehaviour {

    public Sprite originTex;
    public Sprite mouseOnTex;

    // Use this for initialization
    void Start () {
        this.GetComponent<SpriteRenderer>().sprite = originTex;
    }

    private void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().sprite = mouseOnTex;
    }

    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().sprite = originTex;
    }
}
