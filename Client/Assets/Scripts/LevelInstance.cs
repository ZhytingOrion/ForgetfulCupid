using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelInfo
{
    public int levelIndex;
    public int leftStep;
    public int rightStep;
}

public class LevelInstance : MonoBehaviour {

    public LevelInfo levelInfo;
    public int remainLeftStep
    {
        get;set;
    }
    public int remainRightStep
    {
        get;set;
    }

    public List<GameObject> leftFlippedCards;
    public bool leftCanBeClick = true;

    public List<GameObject> rightFlippedCards;
    public bool rightCanBeClick = true;

    public RoleInfo leftRole;
    public RoleInfo rightRole;

	// Use this for initialization
	void Awake () {

        ///
        /// levelInstance的信息：
        /// 
        int level = Game.Instance.gameLevel;
        CardManagerInfo cardManagerInfo = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().levelInfoDic.cardManagerInfoDic[level];

        levelInfo.leftStep = cardManagerInfo.leftStep;
        levelInfo.rightStep = cardManagerInfo.rightStep;
        levelInfo.levelIndex = level;

        this.remainLeftStep = cardManagerInfo.leftStep;
        this.remainRightStep = cardManagerInfo.rightStep;

        ///
        /// CardManager信息
        ///
        GameObject cardManager = GameObject.Find("_cardManager");
        CardInfoDic cardInfoDic = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().cardInfoDic;
        
        //卡片信息：
        List<CardInfo> leftCardInfo = new List<CardInfo>();
        for(int i = 0; i<cardManagerInfo.CardsLeftID.Length; ++i)
        {
            leftCardInfo.Add(cardInfoDic.cardInfoDic[cardManagerInfo.CardsLeftID[i]]);
        }
        cardManager.GetComponent<CardManager>().cardsInfosLeft = leftCardInfo;
        List<CardInfo> rightCardInfo = new List<CardInfo>();
        for(int i = 0; i<cardManagerInfo.CardsRightID.Length; ++i)
        {
            rightCardInfo.Add(cardInfoDic.cardInfoDic[cardManagerInfo.CardsRightID[i]]);
        }
        cardManager.GetComponent<CardManager>().cardsInfosRight = rightCardInfo;

        //卡片位置信息：
        cardManager.GetComponent<CardManager>().cardsLocsLeft = new List<int>(cardManagerInfo.CardsLeftLocs);
        cardManager.GetComponent<CardManager>().cardsLocsRight = new List<int>(cardManagerInfo.CardsRightLocs);

        //卡面图案信息：
        cardManager.GetComponent<CardManager>().contentTexsLeft = (Texture2D)Resources.Load(cardManagerInfo.contentTexsAddrsLeft);
        cardManager.GetComponent<CardManager>().contentTexsRight = (Texture2D)Resources.Load(cardManagerInfo.contentTexsAddrsRight);
        cardManager.GetComponent<CardManager>().leftBackTex = (Texture2D)Resources.Load(cardManagerInfo.backTexsAddrsLeft);
        cardManager.GetComponent<CardManager>().rightBackTex = (Texture2D)Resources.Load(cardManagerInfo.backTexsAddrsRight);
        Texture2D[] typeFrontTexs = new Texture2D[cardManagerInfo.ContentTypeTexsAddrs.Length];
        for(int i = 0; i<cardManagerInfo.ContentTypeTexsAddrs.Length; ++i)
        {
            typeFrontTexs[i] = (Texture2D)Resources.Load(cardManagerInfo.ContentTypeTexsAddrs[i]);
        }
        cardManager.GetComponent<CardManager>().typeFrontTexs = typeFrontTexs;
        Texture2D[] typeTexs = new Texture2D[cardManagerInfo.typeTexsAddrs.Length];
        for (int i = 0; i < cardManagerInfo.typeTexsAddrs.Length; ++i)
        {
            typeTexs[i] = (Texture2D)Resources.Load(cardManagerInfo.typeTexsAddrs[i]);
        }
        cardManager.GetComponent<CardManager>().typeTexs = typeTexs;

        ///
        /// SlotManager的信息
        /// 
        GameObject slotManager = GameObject.Find("_slotManager");
        List<CardResultInfo> cardResultInfoArray = new List<CardResultInfo>(GameObject.Find("_dataAssets").GetComponent<ReadAssets>().cardResultInfoArray.dataArray).FindAll(x => x.levelID == level);
        slotManager.GetComponent<SlotManager>().answers = cardResultInfoArray;
        Texture2D[] slotTypeTexs = new Texture2D[cardManagerInfo.slotTypeTexsAddrs.Length];
        for (int i = 0; i < cardManagerInfo.slotTypeTexsAddrs.Length; ++i)
        {
            slotTypeTexs[i] = (Texture2D)Resources.Load(cardManagerInfo.slotTypeTexsAddrs[i]);
        }
        slotManager.GetComponent<SlotManager>().slotTypeTexs = slotTypeTexs;
        CardType[] slotCardTypes = new CardType[cardManagerInfo.slotTypes.Length];
        for(int i = 0;i<cardManagerInfo.slotTypes.Length; ++i)
        {
            slotCardTypes[i] = (CardType)cardManagerInfo.slotTypes[i];
        }
        slotManager.GetComponent<SlotManager>().slotCardTypes = slotCardTypes;

        ///
        /// RoleInfo信息
        ///
        leftRole = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().roleInfoDic.roleInfoDic[cardManagerInfo.roleLeftID];
        rightRole = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().roleInfoDic.roleInfoDic[cardManagerInfo.roleRightID];

        ///
        /// CardResult信息
        /// 
        List<CardResultInfo> cardResultInfos = new List<CardResultInfo>(GameObject.Find("_dataAssets").GetComponent<ReadAssets>().cardResultInfoArray.dataArray);
        LevelResultInfoDic levelresultInfoDic = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().levelResultInfoDic;
        GameObject.Find("_resultManager").GetComponent<LevelResultManager>().cardResultInfo = cardResultInfos.FindAll(x => x.levelID == level);

        ///
        /// LevelResult信息
        /// 
        GameObject.Find("_resultManager").GetComponent<LevelResultManager>().levelResultInfo = levelresultInfoDic.dic[level];
        Debug.Log("LevelInstance获取到的关卡信息：" + levelresultInfoDic.dic[level].levelID);
    }

    private void OnEnable()
    {
        
    }
    
    /// <summary>
    /// 将卡片都翻回来
    /// </summary>
    /// <param name="isLeft"> 需要翻回来的卡片是否是左边的 </param>
    /// <param name="time"> 翻回来前停留的时间 </param>
    public void FlipCards(bool isLeft, float time)
    {
        if (isLeft) leftCanBeClick = false;
        else rightCanBeClick = false;
        StartCoroutine(flipCards(isLeft, time));
    }

    IEnumerator flipCards(bool isLeft, float time)
    {
        yield return new WaitForSeconds(time);
        List<GameObject> cards2Flip = isLeft ? leftFlippedCards : rightFlippedCards;
        for(int i = 0; i<cards2Flip.Count; ++i)
        {
            if (cards2Flip[i].GetComponent<CardSingle>().isFlip)
                cards2Flip[i].GetComponent<CardSingle>().FlipCard();
        }
        if (isLeft) leftFlippedCards.Clear();
        else rightFlippedCards.Clear();
        if (isLeft)
        {
            leftCanBeClick = true;
            remainLeftStep = levelInfo.leftStep;
        }
        else
        {
            rightCanBeClick = true;
            remainRightStep = levelInfo.rightStep;
        }
    }

    public void ResetSteps()
    {
        this.remainLeftStep = this.levelInfo.leftStep;
        this.remainRightStep = this.levelInfo.rightStep;
        this.leftCanBeClick = true;
        this.rightCanBeClick = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
