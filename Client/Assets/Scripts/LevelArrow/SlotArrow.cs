using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotArrow : MonoBehaviour {

    public CardType slotType;  //箭的类型
    public GameObject leftCard = null;  //左卡牌
    public GameObject rightCard = null;  //右卡牌

    public Vector3 originScale;  //原先尺寸
    public Vector3 mouseOnScale;  //放大尺寸
    private GameObject highlight;   //高光图
    public Sprite fullSprite;
    private Sprite originSprite;
    public bool isActive = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(CardType SlotType, Sprite Tex, Sprite FullTex)
    {
        this.slotType = SlotType;
        this.GetComponent<SpriteRenderer>().sprite = Tex;
        highlight = this.transform.Find("highlight").gameObject;
        originScale = this.transform.localScale;
        originSprite = Tex;
        fullSprite = FullTex;
    }

    void OnMouseOver()
    {
        //this.transform.localScale = mouseOnScale;
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        if(this.leftCard!=null && this.rightCard!=null)
        {
            this.leftCard.GetComponent<CardArrowSingle>().isInSlot = false;
            this.rightCard.GetComponent<CardArrowSingle>().isInSlot = false;
            this.leftCard = null;
            this.rightCard = null;

            this.GetComponent<SpriteRenderer>().sprite = this.originSprite;
        }

        if(this.isActive)    //如果不需要取消Active状态，此处无视
        {

        }
        else if(!this.isActive)
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
            this.transform.localScale = mouseOnScale;
            //开启highlight
        }
        else
        {
            this.isActive = false;
            this.transform.localScale = originScale;
            //关闭highlight
            highlight.SetActive(false);
        }
    }

    public bool SetSlotCardSuccess(GameObject card, bool isLeft)
    {
        if(isLeft)
        {
            if (leftCard != null) return false;
            leftCard = card;
        }
        else
        {
            if (rightCard != null) return false;
            rightCard = card;
        }
        StartCoroutine(arrowAnim(card));
        return true;
    }

    private IEnumerator arrowAnim(GameObject card)
    {
        GameObject arrow = new GameObject("arrow");
        arrow.AddComponent(typeof(SpriteRenderer));
        arrow.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
        arrow.transform.localScale = this.originScale;
        arrow.transform.parent = card.transform;
        arrow.transform.position = this.transform.position;
        Vector3 origin = arrow.transform.position;
        Vector3 diff = new Vector3(0, 0, -0.2f);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            arrow.transform.position = Vector3.Lerp(origin, card.transform.position, t / 1.0f) + diff;
            yield return null;
        }

        if (leftCard != null && rightCard != null)
        {
            ArrowEmpty();
        }
    }

    private void ArrowEmpty()
    {
        Destroy(leftCard.transform.Find("arrow").gameObject);
        Destroy(rightCard.transform.Find("arrow").gameObject);
        SlotArrowManager.Instance.SetActiveSlot(this.slotType);
        this.GetComponent<SpriteRenderer>().sprite = this.fullSprite;

        //自动切换为下一个属性的
        SlotArrowManager.Instance.ArrowEmpty(this.slotType);
    }
}
