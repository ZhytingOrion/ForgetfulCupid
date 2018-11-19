using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSingle : MonoBehaviour {

    public Texture BackTex;
    public Texture ContentTex;
    public Texture TypeTex;
    private Vector3 oldScale;
    private Vector3 oldLoc;
    private bool isDrag = true;

    public CardInfo cardInfo;
    private GameObject levelInstance;
    public float showTypeTime = 3.0f;

    public bool isFlip;

    public void setLoc(Vector3 locs)
    {
        this.oldLoc = locs;
    }

    public void resetLoc(Vector3 locs)
    {
        GameObject cardsCenter = GameObject.Find("CardsCenter");
        StartCoroutine(resetLocAnim(this.oldLoc, cardsCenter.transform.position, locs, 1.0f, 1.0f));
        this.oldLoc = locs;
    }

    IEnumerator resetLocAnim(Vector3 oldLoc, Vector3 centerLoc, Vector3 newLoc, float moveTime, float stayTime)
    {
        if (this.isFlip) FlipCard();
        yield return moveCardAnim(oldLoc, centerLoc, moveTime);    //移动到中心位置
        yield return new WaitForSeconds(stayTime);
        yield return moveCardAnim(centerLoc, newLoc, moveTime);
        if (this.cardInfo.AlwaysShowCard) FlipCard();
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
        float rotateY = 0.0f;
        Vector3 loc = this.transform.position;
        loc.z -= 20.0f;
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
        loc.z += 20.0f;
        this.transform.position = loc;
    }
    
    public void FlipCard(float time = 3.0f)
    {
        this.isFlip = !this.isFlip;
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
        if (BackTex != null) this.GetComponent<Renderer>().material.SetTexture("_MainTex", BackTex);
        if (ContentTex != null) this.GetComponent<Renderer>().material.SetTexture("_ContentTex", ContentTex);
        if (TypeTex != null) this.GetComponent<Renderer>().material.SetTexture("_TypeTex", TypeTex);
        this.setText(this.cardInfo.context);
    }

    private void OnMouseDown()
    {
        //如果没有行动力，返回（根据gender获取剩余行动力数值）

        //如果牌处于已翻开状态：不能再翻一次，返回
        if (this.isFlip) return;
        if(this.cardInfo.locType == LocType.Left)
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
        this.transform.localScale = oldScale * 1.5f;
        Vector3 localPos = this.transform.position;
        localPos.z -= 3.0f;
        this.transform.position = localPos;
    }

    private void OnMouseExit()
    {
        this.transform.localScale = oldScale;
        Vector3 localPos = this.transform.position;
        localPos.z += 3.0f;
        this.transform.position = localPos;
    }

    /*
    private void OnMouseDrag()
    {
        this.transform.position = Input.mousePosition;
    }*/

    private void OnMouseUp()
    {
        ResetLoc();
    }

    public void ResetLoc()
    {
        if (this.isDrag) this.transform.position = this.oldLoc;
    }
}
