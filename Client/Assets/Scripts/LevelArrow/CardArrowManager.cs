using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArrowManager : MonoBehaviour {

    //单例
    private static CardArrowManager _instance = null;
    public static CardArrowManager Instance
    {
        get
        {
            return _instance;
        }
    }
   

    //属性
    [Header("卡片内容属性")]
    public List<CardInfo> cardsInfosLeft = new List<CardInfo>();
    private Dictionary<CardType, List<GameObject>> cardsLeft = new Dictionary<CardType, List<GameObject>>();

    public List<CardInfo> cardsInfosRight = new List<CardInfo>();
    private Dictionary<CardType, List<GameObject>> cardsRight = new Dictionary<CardType, List<GameObject>>();

    private List<GameObject> activeCards = new List<GameObject>();

    [Header("卡牌排列属性")]
    public int MaxCardsShow = 4;
    private int leftIndex = 0;
    private int rightIndex = 0;

    [Header("左卡牌起始位置")]
    public Vector3 startLeftXYZ = new Vector3(-5, 2, 0);
    [Header("左卡牌间隔距离")]
    public Vector3 betweenLeftXYZ = Vector3.zero;

    [Header("右卡牌起始位置")]
    public Vector3 startRightXYZ = new Vector3(5, -2, 0);
    [Header("右卡牌间隔距离")]
    public Vector3 betweenRightXYZ = Vector3.zero;


    private GameObject leftRoot;  //左边卡牌的根
    private GameObject rightRoot;  //右边卡牌的根

    // Use this for initialization
    void Awake () {
        InitCards();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitCards()
    {
        _instance = this;
        leftRoot = this.transform.Find("Left").gameObject;
        rightRoot = this.transform.Find("Right").gameObject;
        classifyCards(cardsLeft, cardsInfosLeft, leftRoot);
        classifyCards(cardsRight, cardsInfosRight, rightRoot);
    }

    private void classifyCards(Dictionary<CardType, List<GameObject>> cards, List<CardInfo> cardinfos, GameObject root)
    { 
        //若cardlist取出后可以用则可用此方法，否则的话需要先确定好cardlist再插入
        for (int i = 0; i < cardinfos.Count; i++)
        {
            CardInfo cardinfo = cardinfos[i];
            //根据cardinfo生成Card对象

            List<GameObject> cardlist = null;
            if (cards.ContainsKey(cardinfo.type))
            {
                cards.TryGetValue(cardinfo.type, out cardlist);
                //加入Card对象
                GameObject card = InitCard(cardinfo);
                card.transform.parent = root.transform;
                cardlist.Add(card);
                cards.Remove(cardinfo.type);
                cards.Add(cardinfo.type, cardlist);
            }
            else
            {
                cardlist = new List<GameObject>();
                //加入Card对象
                GameObject card = InitCard(cardinfo);
                card.transform.parent = root.transform;
                cardlist.Add(card);
                cards.Add(cardinfo.type, cardlist);
            }
        }
    }

    private GameObject InitCard(CardInfo cardinfo)
    {
        GameObject card = (GameObject)Instantiate(Resources.Load("Prefabs/CardArrow"));
        card.GetComponent<CardArrowSingle>().Init(cardinfo);
        card.SetActive(false);
        return card;
    }

    public void SetActiveSlot(CardType slotType)
    {
        for(int i = 0; i<activeCards.Count; i++)
        {
            activeCards[i].SetActive(false);
        }
        activeCards.Clear();

        //LeftCards
        List<GameObject> leftcards = new List<GameObject>();
        cardsLeft.TryGetValue(slotType, out leftcards);
        leftIndex = 0;
        int index = 0;
        for(int i = 0; i<leftcards.Count && i<MaxCardsShow; i++)
        {
            GameObject card = leftcards[i];
            if (card.GetComponent<CardArrowSingle>().isInSlot) continue;
            card.transform.position = startLeftXYZ + index * betweenLeftXYZ;
            card.SetActive(true);
            activeCards.Add(card);
            index++;
        }

        //RightCards
        List<GameObject> rightcards = new List<GameObject>();
        cardsRight.TryGetValue(slotType, out rightcards);
        rightIndex = 0;
        index = 0;
        for (int i = 0; i < rightcards.Count && i < MaxCardsShow; i++)
        {
            GameObject card = rightcards[i];
            if (card.GetComponent<CardArrowSingle>().isInSlot) continue;
            card.transform.position = startRightXYZ + index * betweenRightXYZ;
            card.SetActive(true);
            activeCards.Add(card);
            index++;
        }
    }
}
