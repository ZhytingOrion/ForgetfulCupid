using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArrowSingle : MonoBehaviour {

    public CardInfo m_cardInfo;  //卡牌信息内容
    public CardType cardType;    //卡牌类型
    public bool isInSlot = false;  //是否位于槽中
    private GameObject highlight;    //高亮
    private GameObject text;    //文字

    // Use this for initialization
    void Start () {
        highlight = this.transform.Find("highlight").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(CardInfo cardInfo)
    {
        this.m_cardInfo = cardInfo;
        highlight = this.transform.Find("highlight").gameObject;
        text = this.transform.Find("text").gameObject;
        text.GetComponent<TextMesh>().text = cardInfo.context.Replace("-", "\n");
        this.cardType = cardInfo.type;
    }

    void OnMouseOver()
    {
        if(!this.isInSlot)
            highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        if (this.isInSlot) return;
        GameObject arrow = SlotArrowManager.Instance.ActiveArrow;
        if(arrow.GetComponent<SlotArrow>().SetSlotCardSuccess(this.gameObject, this.m_cardInfo.locType == LocType.Left))
        {
            this.isInSlot = true;
        }
    }
}
