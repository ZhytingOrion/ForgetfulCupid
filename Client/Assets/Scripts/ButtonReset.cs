using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour {
    
    public Sprite originTex;
    public Sprite mouseOnTex;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //变色

        //重置
        GameObject.Find("_cardManager").GetComponent<CardManager>().ResetCards();
        GameObject.Find("_slotManager").GetComponent<SlotManager>().ResetSlots();
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
