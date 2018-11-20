using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResultManager : MonoBehaviour {

    private List<GameObject> slots = new List<GameObject>();
    private int resultNum = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowCardResult()
    {
        if (slots == null) slots = GameObject.Find("_slotManager").GetComponent<SlotManager>().slots;
        GameObject slot = slots[resultNum];
        Vector3 slotLoc = slot.transform.position;
        slotLoc.z = -5;
        slot.transform.position = slotLoc;

        GameObject leftCard = slot.transform.Find("LeftCard").GetComponent<SlotCardInstance>().thisCard;
        GameObject rightCard = slot.transform.Find("RightCard").GetComponent<SlotCardInstance>().thisCard;

        //leftCard.transform.localScale = leftCard.GetComponent<CardSingle>().

        if (!leftCard.GetComponent<CardSingle>().cardInfo.AlwaysShowCard) leftCard.GetComponent<CardSingle>().FlipCard();
        if (!rightCard.GetComponent<CardSingle>().cardInfo.AlwaysShowCard) rightCard.GetComponent<CardSingle>().FlipCard();
    }

    public void nextPage()
    {
        this.resultNum += 1;
        if (this.slots == null || this.resultNum < this.slots.Count) ShowCardResult();
        else Debug.Log("打印完啦");
    }

    
}
