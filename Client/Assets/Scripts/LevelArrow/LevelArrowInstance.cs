using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArrowInstance : MonoBehaviour {

    public int levelIndex;
    private CardManagerInfo cardManagerInfo;

    public RoleInfo leftRole;
    public RoleInfo rightRole;

    void Awake()
    {

        ///
        /// levelInstance的信息：
        /// 
        int level = Game.Instance.gameLevel;
        cardManagerInfo = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().levelInfoDic.cardManagerInfoDic[level];

        levelIndex = level;

        ///
        /// CardManager信息
        ///
        GameObject cardManager = GameObject.Find("Cards");
        CardInfoDic cardInfoDic = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().cardInfoDic;

        //卡片信息：
        List<CardInfo> leftCardInfo = new List<CardInfo>();
        for (int i = 0; i < cardManagerInfo.CardsLeftID.Length; ++i)
        {
            leftCardInfo.Add(cardInfoDic.cardInfoDic[cardManagerInfo.CardsLeftID[i]]);
        }
        List<CardInfo> rightCardInfo = new List<CardInfo>();
        for (int i = 0; i < cardManagerInfo.CardsRightID.Length; ++i)
        {
            rightCardInfo.Add(cardInfoDic.cardInfoDic[cardManagerInfo.CardsRightID[i]]);
        }
        cardManager.GetComponent<CardArrowManager>().InitCards(leftCardInfo, rightCardInfo);

        //卡面图案信息：
        //cardManager.GetComponent<CardManager>().contentTexsLeft = (Texture2D)Resources.Load(cardManagerInfo.contentTexsAddrsLeft);
        //cardManager.GetComponent<CardManager>().contentTexsRight = (Texture2D)Resources.Load(cardManagerInfo.contentTexsAddrsRight);
        //cardManager.GetComponent<CardManager>().leftBackTex = (Texture2D)Resources.Load(cardManagerInfo.backTexsAddrsLeft);
        //cardManager.GetComponent<CardManager>().rightBackTex = (Texture2D)Resources.Load(cardManagerInfo.backTexsAddrsRight);
        //Texture2D[] typeFrontTexs = new Texture2D[cardManagerInfo.ContentTypeTexsAddrs.Length];
        //for (int i = 0; i < cardManagerInfo.ContentTypeTexsAddrs.Length; ++i)
        //{
        //    typeFrontTexs[i] = (Texture2D)Resources.Load(cardManagerInfo.ContentTypeTexsAddrs[i]);
        //}
        //cardManager.GetComponent<CardManager>().typeFrontTexs = typeFrontTexs;
        //Texture2D[] typeTexs = new Texture2D[cardManagerInfo.typeTexsAddrs.Length];
        //for (int i = 0; i < cardManagerInfo.typeTexsAddrs.Length; ++i)
        //{
        //    typeTexs[i] = (Texture2D)Resources.Load(cardManagerInfo.typeTexsAddrs[i]);
        //}
        //cardManager.GetComponent<CardManager>().typeTexs = typeTexs;


        ///
        /// RoleInfo信息
        ///
        leftRole = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().roleInfoDic.roleInfoDic[cardManagerInfo.roleLeftID];
        rightRole = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().roleInfoDic.roleInfoDic[cardManagerInfo.roleRightID];

        //角色信息：
        GameObject.Find("LeftRolePic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(leftRole.roleHeadPicAddr);
        GameObject.Find("RightRolePic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rightRole.roleHeadPicAddr);
        GameObject.Find("LeftDesPic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardManagerInfo.roleLeftDesPic);
        GameObject.Find("RightDesPic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardManagerInfo.roleRightDesPic);
        GameObject.Find("LeftRoleName").GetComponent<TextMesh>().text = cardManagerInfo.roleLeftName;
        GameObject.Find("RightRoleName").GetComponent<TextMesh>().text = cardManagerInfo.roleRightName;
        GameObject.Find("LevelName").GetComponent<TextMesh>().text = cardManagerInfo.levelName.Replace('-', '\n');

        ///
        /// SlotManager的信息
        /// 
        GameObject slotManager = GameObject.Find("ArrowManagers");
        List<CardResultInfo> cardResultInfoArray = new List<CardResultInfo>(GameObject.Find("_dataAssets").GetComponent<ReadAssets>().cardResultInfoArray.dataArray).FindAll(x => x.levelID == level);
        slotManager.GetComponent<SlotArrowManager>().answers = cardResultInfoArray;

        //槽图案信息
        //Texture2D[] slotTypeTexs = new Texture2D[cardManagerInfo.slotTypeTexsAddrs.Length];
        //for (int i = 0; i < cardManagerInfo.slotTypeTexsAddrs.Length; ++i)
        //{
        //    slotTypeTexs[i] = (Texture2D)Resources.Load(cardManagerInfo.slotTypeTexsAddrs[i]);
        //}
        //slotManager.GetComponent<SlotManager>().slotTypeTexs = slotTypeTexs;
        CardType[] slotCardTypes = new CardType[cardManagerInfo.slotTypes.Length];
        for (int i = 0; i < cardManagerInfo.slotTypes.Length; ++i)
        {
            slotCardTypes[i] = (CardType)cardManagerInfo.slotTypes[i];
        }
        slotManager.GetComponent<SlotArrowManager>().slotCardTypes = slotCardTypes;


        ///
        /// CardResult信息
        /// 
        List<CardResultInfo> cardResultInfos = new List<CardResultInfo>(GameObject.Find("_dataAssets").GetComponent<ReadAssets>().cardResultInfoArray.dataArray);
        LevelResultInfoDic levelresultInfoDic = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().levelResultInfoDic;
        GameObject.Find("_resultManager").GetComponent<LevelArrowResultManager>().cardResultInfo = cardResultInfos.FindAll(x => x.levelID == level);

        ///
        /// LevelResult信息
        /// 
        GameObject.Find("_resultManager").GetComponent<LevelArrowResultManager>().levelResultInfo = levelresultInfoDic.dic[level];
        GameObject.Find("_resultManager").GetComponent<LevelArrowResultManager>().leftRole = leftRole;
        GameObject.Find("_resultManager").GetComponent<LevelArrowResultManager>().rightRole = rightRole;
        Debug.Log("LevelInstance获取到的关卡信息：" + levelresultInfoDic.dic[level].levelID);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
