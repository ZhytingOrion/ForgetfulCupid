using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelInfo
{
    public int levelIndex;
    //public int leftStep;
    //public int rightStep;
    public int stepNum;
}

public class LevelInstance : MonoBehaviour {

    public LevelInfo levelInfo;
    public int remainStepNum
    {
        get; set;
    }
    public List<GameObject> flippedCards;
    public bool CanBeClick = true;

    //public int remainLeftStep
    //{
    //    get;set;
    //}
    //public int remainRightStep
    //{
    //    get;set;
    //}

    //public List<GameObject> leftFlippedCards;
    //public bool leftCanBeClick = true;

    //public List<GameObject> rightFlippedCards;
    //public bool rightCanBeClick = true;

    public RoleInfo leftRole;
    public RoleInfo rightRole;

	// Use this for initialization
	void Awake () {

        ///
        /// levelInstance的信息：
        /// 
        int level = Game.Instance.gameLevel;
        CardManagerInfo cardManagerInfo = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().levelInfoDic.cardManagerInfoDic[level];

        //levelInfo.leftStep = cardManagerInfo.leftStep;
        //levelInfo.rightStep = cardManagerInfo.rightStep;
        levelInfo.stepNum = 2;
        levelInfo.levelIndex = level;

        //this.remainLeftStep = cardManagerInfo.leftStep;
        //this.remainRightStep = cardManagerInfo.rightStep;
        this.remainStepNum = levelInfo.stepNum;

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
        int[] locs = { 3, 3, 3 };
        cardManager.GetComponent<CardManager>().cardsLocsLeft = new List<int>(locs);
        cardManager.GetComponent<CardManager>().cardsLocsRight = new List<int>(locs);

        //卡面图案信息：
        cardManager.GetComponent<CardManager>().contentTexsLeft = (Texture2D)Resources.Load("Arts/ImagesNew/CardFront");
        cardManager.GetComponent<CardManager>().contentTexsRight = (Texture2D)Resources.Load("Arts/ImagesNew/CardFront");
        cardManager.GetComponent<CardManager>().leftBackTex = (Texture2D)Resources.Load("Arts/ImagesNew/CardBack");
        cardManager.GetComponent<CardManager>().rightBackTex = (Texture2D)Resources.Load("Arts/ImagesNew/CardBack");

        Texture2D[] typeFrontTexs = new Texture2D[3];
        string[] typeFrontAddress = { "Arts/ImagesNew/CardAttri_time_front", "Arts/ImagesNew/CardAttri_dialo_front", "Arts/ImagesNew/CardAttri_action_front" };
        for(int i = 0; i<3; ++i)
        {
            typeFrontTexs[i] = (Texture2D)Resources.Load(typeFrontAddress[i]);
        }
        cardManager.GetComponent<CardManager>().typeFrontTexs = typeFrontTexs;

        Texture2D[] typeTexs = new Texture2D[3];
        string[] typeBackAddress = { "Arts/ImagesNew/CardAttri_time_back", "Arts/ImagesNew/CardAttri_dialo_back", "Arts/ImagesNew/CardAttri_action_back" };
        for (int i = 0; i < 3; ++i)
        {
            typeTexs[i] = (Texture2D)Resources.Load(typeBackAddress[i]);
        }
        cardManager.GetComponent<CardManager>().typeTexs = typeTexs;

        GameObject.Find("LeftDesPic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardManagerInfo.roleLeftDesPic);
        GameObject.Find("RightDesPic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardManagerInfo.roleRightDesPic);
        GameObject.Find("LeftRoleName").GetComponent<TextMesh>().text = cardManagerInfo.roleLeftName;
        GameObject.Find("RightRoleName").GetComponent<TextMesh>().text = cardManagerInfo.roleRightName;
        GameObject.Find("LevelName").GetComponent<TextMesh>().text = cardManagerInfo.levelName.Replace('-','\n');

        ///
        /// RoleInfo信息
        ///
        leftRole = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().roleInfoDic.roleInfoDic[cardManagerInfo.roleLeftID];
        rightRole = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().roleInfoDic.roleInfoDic[cardManagerInfo.roleRightID];

        cardManager.GetComponent<CardManager>().LeftRolePic = leftRole.roleHeadPicAddr;
        cardManager.GetComponent<CardManager>().RightRolePic = rightRole.roleHeadPicAddr;

        ///
        /// SlotManager的信息
        /// 
        GameObject slotManager = GameObject.Find("_slotManager");
        List<CardResultInfo> cardResultInfoArray = new List<CardResultInfo>(GameObject.Find("_dataAssets").GetComponent<ReadAssets>().cardResultInfoArray.dataArray).FindAll(x => x.levelID == level);
        slotManager.GetComponent<SlotManager>().answers = cardResultInfoArray;
        Texture2D[] slotTypeTexs = new Texture2D[3];
        string[] slotTypeTexsAddrs = { "Arts/ImagesNew/Attribute_time", "Arts/ImagesNew/Attribute_dialo", "Arts/ImagesNew/Attribute_action" };
        for (int i = 0; i < 3; ++i)
        {
            slotTypeTexs[i] = (Texture2D)Resources.Load(slotTypeTexsAddrs[i]);
        }
        slotManager.GetComponent<SlotManager>().slotTypeTexs = slotTypeTexs;
        CardType[] slotCardTypes = new CardType[cardManagerInfo.slotTypes.Length];
        for(int i = 0;i<cardManagerInfo.slotTypes.Length; ++i)
        {
            slotCardTypes[i] = (CardType)cardManagerInfo.slotTypes[i];
        }
        slotManager.GetComponent<SlotManager>().slotCardTypes = slotCardTypes;

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
        GameObject.Find("_resultManager").GetComponent<LevelResultManager>().leftRole = leftRole;
        GameObject.Find("_resultManager").GetComponent<LevelResultManager>().rightRole = rightRole;
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
    public void FlipCards(float time)
    {
        //if (isLeft) leftCanBeClick = false;
        //else rightCanBeClick = false;
        CanBeClick = false;
        StartCoroutine(flipCards(time));
    }

    IEnumerator flipCards(float time)
    {
        yield return new WaitForSeconds(time);
        // List<GameObject> cards2Flip = isLeft ? leftFlippedCards : rightFlippedCards;
        List<GameObject> cards2Flip = this.flippedCards;
        for (int i = 0; i<cards2Flip.Count; ++i)
        {
            if (cards2Flip[i].GetComponent<CardSingle>().isFlip)
                cards2Flip[i].GetComponent<CardSingle>().FlipCard();
        }
        flippedCards.Clear();
        remainStepNum = levelInfo.stepNum;
        CanBeClick = true;
        //if (isLeft) leftFlippedCards.Clear();
        //else rightFlippedCards.Clear();
        //if (isLeft)
        //{
        //    leftCanBeClick = true;
        //    remainLeftStep = levelInfo.leftStep;
        //}
        //else
        //{
        //    rightCanBeClick = true;
        //    remainRightStep = levelInfo.rightStep;
        //}     
    }

    public void ResetSteps()
    {
        //this.remainLeftStep = this.levelInfo.leftStep;
        //this.remainRightStep = this.levelInfo.rightStep;
        //this.leftCanBeClick = true;
        //this.rightCanBeClick = true;
        this.remainStepNum = this.levelInfo.stepNum;
        this.CanBeClick = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
