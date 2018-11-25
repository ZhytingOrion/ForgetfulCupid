using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum CardType
{
    //For YanTing:  填写卡片类型，默认从0开始，递增
    Time,
    Chat,
    Act,
}


[System.Serializable]
public enum LocType
{
    Left,
    Right,
}

[System.Serializable]
public class CardInfo {
    public int cardID;
    public CardType type;
    public string context;
    public LocType locType;   //左边还是右边
    public bool AlwaysShowCard;
    public bool AlwaysShowType;
    public float stayTime = 3.0f;
}

[System.Serializable]
public class CardInfoDic
{
    public Dictionary<int, CardInfo> cardInfoDic = new Dictionary<int, CardInfo>();
}

public class CardInfoArray : ScriptableObject
{
    public CardInfo[] dataArray;
}

[System.Serializable]
public class RoleInfo
{
    public int roleID;  //人物ID
    public string roleName;  //人物名字
    public string description;  //人物性格等描述
    public string rolePicAddr;  //人物头像图片地址
}

[System.Serializable]
public class RoleInfoDic
{
    public Dictionary<int, RoleInfo> roleInfoDic = new Dictionary<int, RoleInfo>();
}

public class RoleInfoArray : ScriptableObject
{
    public RoleInfo[] dataArray;
}

[System.Serializable]
public class CardManagerInfo
{
    public int levelID;
    public int roleLeftID;
    public int roleRightID;
    public int[] CardsLeftID;
    public int[] CardsRightID;
    public int[] CardsLeftLocs;
    public int[] CardsRightLocs;
    //如果每关的卡的图案都不同
    public string[] typeTexsAddrs;
    public string[] contentTexsAddrsLeft;
    public string[] contentTexsAddrsRight;
    public string backTexsAddrsLeft;
    public string backTexsAddrsRight;
}

[System.Serializable]
public class CardResults
{
    public Dictionary<int, int> cardsRightResults = new Dictionary<int, int>();
}


