using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClass : MonoBehaviour {

    public Sprite originTex;
    public Sprite mouseOnTex;
    public GameObject highlight = null;

    // Use this for initialization
    void Start () {
        this.GetComponent<SpriteRenderer>().sprite = originTex;
        Transform hl = this.transform.Find("HighLight");
        if (hl != null)
        {
            highlight = hl.gameObject;
            highlight.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().sprite = mouseOnTex;
        if (highlight != null) highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().sprite = originTex;
        if (highlight != null) highlight.SetActive(false);
    }
}
