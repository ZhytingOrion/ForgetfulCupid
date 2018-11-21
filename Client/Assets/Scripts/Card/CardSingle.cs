using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSingle : MonoBehaviour {

    public Texture2D BackTex;
    public Texture2D ContentTex;
    public Texture2D TypeTex;
    private Vector3 oldScale;
    private Vector3 oldLoc;
    public bool isDrag = false;

    public CardInfo cardInfo;
    private GameObject levelInstance;
    public float showTypeTime = 3.0f;
    public float mouseOnScaleSize = 1.5f;
    public float inSlotCardSize = 0.4f;

    public bool isFlip;
    private GameObject slot = null;
    public bool isInSlot = false;

    private Vector2 colliderSize;

    public void setLoc(Vector3 locs)
    {
        this.oldLoc = locs;
    }

    public void resetLoc(Vector3 locs)
    {
        GameObject cardsCenter = GameObject.Find("CardsCenter");
        StartCoroutine(resetLocAnim(this.transform.position, cardsCenter.transform.position, locs, 1.0f, 1.0f));
        this.oldLoc = locs;
        this.slot = null;
        this.isInSlot = false;
    }

    IEnumerator resetLocAnim(Vector3 startLoc, Vector3 centerLoc, Vector3 newLoc, float moveTime, float stayTime)
    {
        Debug.Log("平移动画：" + this.gameObject.name + " " + startLoc + " " + centerLoc + " " + newLoc);
        if (this.isFlip) FlipCard();
        yield return moveCardAnim(startLoc, centerLoc, moveTime);    //移动到中心位置
        this.transform.localScale = oldScale;
        yield return new WaitForSeconds(stayTime);
        yield return moveCardAnim(centerLoc, newLoc, moveTime);
        if (this.cardInfo.AlwaysShowCard) FlipCard();

        ShowType(this.showTypeTime);
    }

    IEnumerator moveCardAnim(Vector3 startLoc, Vector3 endLoc, float moveTime)
    {
        Vector3 deltaLoc = (endLoc - startLoc) / moveTime;
        for(float time = 0; time < moveTime; time+=Time.deltaTime)
        {
            this.transform.position = startLoc + time * deltaLoc;
            yield return null;
        }
        this.transform.position = endLoc;
    }

    public void setText(string text)
    {
        this.transform.Find("Text").GetComponent<TextMesh>().text = text.Replace("-", "\n");
    }

    public void ShowType(float time)
    {
        ShowType();

        //开启协程，time秒后关闭
        StartCoroutine(HideRimAfterSeconds(time));
    }

    public void ShowType()
    {
        //Show Type的方法
        StartCoroutine(ChangeRimDegree(0.02f));
    }

    public void HideType()
    {
        StartCoroutine(ChangeRimDegree(-0.02f));
    }

    IEnumerator ChangeRimDegree(float delta)
    {
        Material mat = this.GetComponent<Renderer>().material;
        for (int i = 0; i < 1 / Mathf.Abs(delta); i++)
        {
            float degree = mat.GetFloat("_TypeDegree") + delta;
            if(degree <= 1 && degree >= 0)
                mat.SetFloat("_TypeDegree", degree);
            yield return null;
        }
    }

    IEnumerator HideRimAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        HideType();
    }

    IEnumerator RotateSelf(float degree)
    {
        Debug.Log("我翻了！" + this.cardInfo.cardID);
        Destroy(this.GetComponent<BoxCollider2D>());

        float rotateY = 0.0f;
        Vector3 loc = this.transform.position;
        if(Game.Instance.gameState == GameState.Play) loc.z = -5.0f;
        this.transform.position = loc;
        for(int i = 0; i < 180 / Mathf.Abs(degree); i++)
        {
            if (Mathf.Abs(rotateY) >= 90  &&  Mathf.Abs(rotateY)<=100)
            {
                if (this.isFlip)
                {

                    this.GetComponent<Renderer>().material.SetFloat("_isBack", 0);
                }
                else
                {
                    this.GetComponent<Renderer>().material.SetFloat("_isBack", 1);
                }
            }
            rotateY += degree;
            this.transform.Rotate(new Vector3(0, 1, 0), degree);
            yield return null;
        }
        rotateY = degree < 0 ? -180 - rotateY : 180 - rotateY;
        this.transform.Rotate(new Vector3(0, 1, 0), rotateY);
        if (Game.Instance.gameState == GameState.Play) loc.z = 0.0f;
        this.transform.position = loc;

        this.gameObject.AddComponent<BoxCollider2D>();
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    
    public void FlipCard(float time = 3.0f)
    {
        this.isFlip = !this.isFlip;
        Debug.Log("我要翻牌了！" + this.cardInfo.cardID);
        StartCoroutine(RotateSelf(time));
    }

    private void Start()
    {

        Debug.Log("myname is " + this.cardInfo.cardID + " and im start!");
        this.transform.position = oldLoc;
        this.isFlip = false;
        if(this.cardInfo.AlwaysShowCard)
        {
            FlipCard(3);
        }
        ShowType(this.showTypeTime);
        //FlipCard(3);
        levelInstance = GameObject.Find("_levelManager");
    }
    
    public void Init()
    {
        Debug.Log("myname is " + this.cardInfo.cardID + " and im init!");
        oldScale = this.transform.localScale;
        this.colliderSize = this.GetComponent<BoxCollider2D>().size;
        if (BackTex != null)
        {
            this.GetComponent<Renderer>().material.SetTexture("_MainTex", BackTex);
            Sprite sp = Sprite.Create(BackTex, this.GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f));
            this.GetComponent<SpriteRenderer>().sprite = sp;
        }
        if (ContentTex != null) this.GetComponent<Renderer>().material.SetTexture("_ContentTex", ContentTex);
        if (TypeTex != null) this.GetComponent<Renderer>().material.SetTexture("_TypeTex", TypeTex);
        this.setText(this.cardInfo.context);
    }

    private void onMouseDown()
    {

        //如果牌处于已翻开状态：不能再翻一次，返回
        if (this.isFlip) return;

        if (this.isInSlot) return;  //如果牌已经置于槽中，返回

        if (this.cardInfo.locType == LocType.Left)
        {
            if (!levelInstance.GetComponent<LevelInstance>().leftCanBeClick) return; //左边的牌正在恢复中，不能点击

            int rLS = levelInstance.GetComponent<LevelInstance>().remainLeftStep - 1;
            int rRS = levelInstance.GetComponent<LevelInstance>().remainRightStep;
            levelInstance.GetComponent<LevelInstance>().remainLeftStep = rLS;
            levelInstance.GetComponent<LevelInstance>().leftFlippedCards.Add(this.gameObject);
            if(rLS == 0)
            {
                levelInstance.GetComponent<LevelInstance>().leftCanBeClick = false;
                if (rRS == 0)
                {
                    levelInstance.GetComponent<LevelInstance>().FlipCards(true, this.cardInfo.stayTime);
                    levelInstance.GetComponent<LevelInstance>().FlipCards(false, this.cardInfo.stayTime);
                }
            }
        }
        else
        {
            if (!levelInstance.GetComponent<LevelInstance>().rightCanBeClick) return; //右边的牌正在恢复中，不能点击

            int rRS = levelInstance.GetComponent<LevelInstance>().remainRightStep - 1;
            int rLS = levelInstance.GetComponent<LevelInstance>().remainLeftStep;
            levelInstance.GetComponent<LevelInstance>().remainRightStep = rRS;
            levelInstance.GetComponent<LevelInstance>().rightFlippedCards.Add(this.gameObject);
            if (rRS == 0)
            {
                levelInstance.GetComponent<LevelInstance>().rightCanBeClick = false;
                if (rLS == 0)
                {
                    levelInstance.GetComponent<LevelInstance>().FlipCards(true, this.cardInfo.stayTime);
                    levelInstance.GetComponent<LevelInstance>().FlipCards(false, this.cardInfo.stayTime);
                }
            }
        }

        FlipCard(5.0f);
    }

    private void OnMouseEnter()
    {
        if (Game.Instance.gameState != GameState.Play) return;
        if (this.isInSlot && !this.cardInfo.AlwaysShowCard) return;
        this.transform.localScale = this.transform.localScale * this.mouseOnScaleSize;
        Vector3 localPos = this.transform.position;
        localPos.z = -3.0f;
        this.transform.position = localPos;
    }

    private void OnMouseExit()
    {
        if (Game.Instance.gameState != GameState.Play) return;
        this.transform.localScale = this.isInSlot ? oldScale * this.inSlotCardSize : oldScale;
        Vector3 localPos = this.transform.position;
        localPos.z = 0.0f;
        this.transform.position = localPos;
    }

    private void OnMouseUp()
    {
        if(Game.Instance.gameState != GameState.Play) return;
        Debug.Log("距离：" + Vector3.Distance(this.transform.position, this.oldLoc));
        if (Vector3.Distance(this.transform.position, this.oldLoc) <= 1.5
            ) onMouseDown();
        ResetLoc();
        if(this.GetComponent<BoxCollider2D>()!=null) this.GetComponent<BoxCollider2D>().size = this.colliderSize;
    }

    public void ResetLoc()
    {
        if (this.isDrag)
        {
            if (this.slot == null)
            {
                this.transform.position = this.oldLoc;
                this.transform.localScale = this.oldScale;
            }
            else  //置于槽中
            {
                this.transform.position = this.slot.transform.position;
                this.transform.localScale = new Vector3(this.inSlotCardSize, this.inSlotCardSize, 0);
                this.slot.GetComponent<SlotCardInstance>().thisCard = this.gameObject;
                this.isInSlot = true;
                if (this.isFlip && !this.cardInfo.AlwaysShowCard) FlipCard();
            }
        }
        this.isDrag = false;
    }

    public void setSlot(GameObject slot = null)
    {
        this.slot = slot;
    }
}
