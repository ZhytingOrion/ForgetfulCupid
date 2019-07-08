
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotButtonArrow : MonoBehaviour {

    public CardType slotType;
    //public List<CardArrowSingle> = new List<CardArrowSingle>();
    public bool isLeftSlot = false;  //判断按钮在左边还是右边
    public bool isActive = false;    //判断是否激活
    private GameObject highlight;    //Highlight

    // Use this for initialization
    void Start ()
    {
        highlight = this.transform.Find("highlight").gameObject;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Init(CardType SlotType, Sprite Tex, bool isLeft)
    {
        this.slotType = SlotType;
        this.GetComponent<SpriteRenderer>().sprite = Tex;
        this.isLeftSlot = isLeft;
        highlight = this.transform.Find("highlight").gameObject;
    }

    void OnMouseDown()
    {
        if (this.isActive)    //如果不需要取消Active状态，此处无视
        {

        }
        else if (!this.isActive)
        {
            //告知SlotArrowManager谁被Active了，让Manager广播其他取消Active状态
            SlotArrowManager.Instance.SetActiveSlot(this.slotType);
            //告知Controller切换Button和Card状态
        }
    }

    public void SetActiveState(bool IsActive)
    {
        if (IsActive)
        {
            this.isActive = true;
            //开启highlight
            highlight.SetActive(true);
        }
        else
        {
            this.isActive = false;
            //关闭highlight
            highlight.SetActive(false);
        }
    }
}
