using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCardInstance : MonoBehaviour {

    public GameObject thisCard;
    private LocType slotLocType = LocType.Right;

	// Use this for initialization
	void Start () {
        if (this.gameObject.name == "LeftCard") slotLocType = LocType.Left;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool setCard(GameObject card)
    {
        //判断Card类型/左右
        if (!(card.GetComponent<CardSingle>().cardInfo.locType == this.slotLocType)) return false;   //不是同一侧的            

        //判断slot是否有牌了
        if (this.thisCard != null) return false;
        
        return true;
    }

    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {
            GameObject card = collision.gameObject;

            //判断Card类型/左右
            if (!(card.GetComponent<CardSingle>().cardInfo.locType == this.slotLocType)) return;   //不是同一侧的            

            //判断slot是否有牌了
            if (this.thisCard != null) return;

            card.GetComponent<CardSingle>().setSlot(this.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {
            GameObject card = collision.gameObject;
            card.GetComponent<CardSingle>().setSlot(null);
        }
    }*/

    public void resetSlot()
    {
        this.thisCard = null;
    }
}
