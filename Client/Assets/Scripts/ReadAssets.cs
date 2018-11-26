using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadAssets : MonoBehaviour {

    public static int Init = 0;

    public CardManagerInfoDic levelInfoDic;
    public CardInfoDic cardInfoDic;
    public RoleInfoDic roleInfoDic;
    public CardResultInfoArray cardResultInfoArray;

    // Use this for initialization
    void Awake () {
		if(Init == 0)
        {
            Init = 1;
            DontDestroyOnLoad(this.gameObject);
            //CardInfo
            CardInfoArray cardInfoArray = (CardInfoArray)Resources.Load("DataAssets/CardInfo");
            Debug.Log(cardInfoArray);
            for (int i = 0; i < cardInfoArray.dataArray.Length; ++i)
            {
                CardInfo cardInfo = cardInfoArray.dataArray[i];
                cardInfoDic.cardInfoDic.Add(cardInfo.cardID, cardInfo);
            }

            //RoleInfo
            RoleInfoArray roleInfoArray = (RoleInfoArray)Resources.Load("DataAssets/RoleInfo");
            Debug.Log(roleInfoArray);
            for (int i = 0; i < roleInfoArray.dataArray.Length; ++i)
            {
                RoleInfo roleInfo = roleInfoArray.dataArray[i];
                roleInfoDic.roleInfoDic.Add(roleInfo.roleID, roleInfo);
            }

            //CardManagerInfo
            CardManagerInfoArray cardManagerInfoArray = (CardManagerInfoArray)Resources.Load("DataAssets/LevelInfo");
            for (int i = 0; i < cardManagerInfoArray.dataArray.Length; ++i)
            {
                CardManagerInfo cardManagerInfo = cardManagerInfoArray.dataArray[i];
                levelInfoDic.cardManagerInfoDic.Add(cardManagerInfo.levelID, cardManagerInfo);
            }

            //CardResultInfo
            cardResultInfoArray = (CardResultInfoArray)Resources.Load("DataAssets/CardResultInfo");
        }
	}
}
