using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    public float SpaceX = 1.5f;
    public float SpaceY = 2.0f;
    public float StartX = 8.0f;

    public float showTypeTime = 3.0f;

    public bool isInit = true;
    
    public List<CardInfo> cardsInfosLeft = new List<CardInfo>();
    private List<GameObject> cardsLeft = new List<GameObject>();
    public List<int> cardsLocsLeft = new List<int>();

    public List<CardInfo> cardsInfosRight = new List<CardInfo>();
    private List<GameObject> cardsRight = new List<GameObject>();
    public List<int> cardsLocsRight = new List<int>();

    [Header("Type Texs")]
    public Texture2D[] typeTexs;
    public Texture2D[] contentTexsLeft;
    public Texture2D[] contentTexsRight;
    public Texture2D leftBackTex;
    public Texture2D rightBackTex;

	// Use this for initialization
	void Start () {
        //初始化卡片
        InitialCards(cardsLocsLeft, out cardsLeft, cardsInfosLeft, true);
        InitialCards(cardsLocsRight, out cardsRight, cardsInfosRight, false);

        isInit = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitialCards(List<int> cardsLocs, out List<GameObject> cards, List<CardInfo> cardsInfos, bool isLeft)
    {
        Debug.Log("Start Initialize!");
        cards = new List<GameObject>();
        for (int i = 0; i < cardsInfos.Count; ++i)
        {
            GameObject card = Instantiate((GameObject)Resources.Load("Prefabs/Card"));

            card.transform.parent = GameObject.Find("Cards").transform;
            card.GetComponent<CardSingle>().cardInfo = cardsInfos[i];
            card.GetComponent<CardSingle>().showTypeTime = showTypeTime;
            card.GetComponent<CardSingle>().TypeTex = typeTexs[(int)cardsInfos[i].type];
            card.GetComponent<CardSingle>().ContentTex = isLeft ? contentTexsLeft[(int)cardsInfos[i].type] : contentTexsRight[(int)cardsInfos[i].type];
            card.GetComponent<CardSingle>().BackTex = isLeft ? leftBackTex : rightBackTex;
            card.GetComponent<CardSingle>().Init();

            cards.Add(card);
        }
        cards = ResetCards(cards);
        Debug.Log(cards.Count);
        setCardsLocs(cardsLocs, cards, isLeft);
    }

    public void setCardsLocs(List<int> cardsLocs, List<GameObject> cards, bool isLeft)
    {
        int count = 0;
        int ratio = 1;
        if (isLeft) ratio = -1;
        float startY = (cardsLocs.Count-1) * 0.5f * -this.SpaceY;
        float startX = this.StartX * ratio;
        Debug.Log(isLeft + " " + ratio + " " + startY + "," + startX);
        for(int i = 0;i<cardsLocs.Count; ++i)
        {
            float locY = startY + i * this.SpaceY;
            for(int j = 0; j<cardsLocs[i]; ++j)
            {
                float locX = startX - j * this.SpaceX * ratio;
                cards[count].transform.position = new Vector3(locX, locY, 0);
                if(isInit) cards[count].GetComponent<CardSingle>().setLoc(new Vector3(locX, locY, 0));
                else cards[count].GetComponent<CardSingle>().resetLoc(new Vector3(locX, locY, 0));
                count++;

                Debug.Log(cards[count-1].GetComponent<CardSingle>().cardInfo.cardID+ "," + cards[count-1].transform.position);
            }
        }
    }

    public void ResetCards()
    {
        cardsLeft = ResetCards(cardsLeft);
        setCardsLocs(cardsLocsLeft, cardsLeft, true);
        
        cardsRight = ResetCards(cardsRight);
        setCardsLocs(cardsLocsRight, cardsRight, false);

        GameObject.Find("_levelManager").GetComponent<LevelInstance>().ResetSteps();
        //弹出已重新洗牌的标识
    }

    private List<GameObject> ResetCards(List<GameObject> cards)
    {
        Debug.Log("Reset Cards ************");
        List<GameObject> resetCards = new List<GameObject>();
        List<int> numbers = new List<int>();
        for(int i = 0; i< cards.Count; i++)
        {
            numbers.Add(i);
            Debug.Log(i);
        }

        Debug.Log("Choose Cards ************");
        for (int i = 0; i < cards.Count; i++)
        {
            Debug.Log("numbers.Count" + numbers.Count);
            int random = Random.Range(0, numbers.Count);
            resetCards.Add(cards[numbers[random]]);
            numbers.RemoveAt(random);
            Debug.Log("random" + random);
        }
        return resetCards;
    }
}
