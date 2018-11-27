using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResultManager : MonoBehaviour {

    public List<GameObject> slots = new List<GameObject>();
    public int resultNum = -1;
    public int lineNum = -1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowCardResult()
    {
        Debug.Log("我在结算界面" + this.resultNum);
        if (slots.Count == 0) slots = GameObject.Find("_slotManager").GetComponent<SlotManager>().slots;
        if (this.resultNum >= slots.Count) return;
        if (this.resultNum > 0) slots[resultNum - 1].transform.position = new Vector3(0, 0, 0);
        GameObject slot = slots[resultNum];

        GameObject leftCard = slot.transform.Find("LeftCard").GetComponent<SlotCardInstance>().thisCard;
        GameObject rightCard = slot.transform.Find("RightCard").GetComponent<SlotCardInstance>().thisCard;
        leftCard.transform.parent = slot.transform;
        rightCard.transform.parent = slot.transform;

        Vector3 slotLoc = slot.transform.position;
        slotLoc.z = -5;
        slotLoc.y = 0;
        slot.transform.position = slotLoc;
        slot.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        leftCard.transform.position = slot.transform.Find("LeftCard").transform.position - new Vector3(0,0,3);
        rightCard.transform.position = slot.transform.Find("RightCard").transform.position - new Vector3(0, 0, 3);
        //leftCard.transform.localScale = leftCard.GetComponent<CardSingle>().

        if (!leftCard.GetComponent<CardSingle>().cardInfo.AlwaysShowCard)
        {
            Debug.Log("卡片信息：" + leftCard.GetComponent<CardSingle>().cardInfo.cardID);
            leftCard.GetComponent<CardSingle>().FlipCard();
        }
        if (!rightCard.GetComponent<CardSingle>().cardInfo.AlwaysShowCard) rightCard.GetComponent<CardSingle>().FlipCard();
    }

    public void nextPage()
    {
        this.resultNum += 1;
        Debug.Log("ResultNum:" + this.resultNum + "/" + this.slots.Count);
        if (this.resultNum < this.slots.Count)
        {
            Debug.Log("显示画面" + resultNum);
            this.lineNum = -1;
            ShowCardResult();
        }
        else
        {
            Debug.Log("打印完啦");
            //返回选关界面
        }
    }    

    public void nextLine()
    {
        this.lineNum += 1;
    }
}
