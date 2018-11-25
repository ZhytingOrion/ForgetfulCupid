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

[System.Serializable]
public class RoleInfo
{
    public int roleID;  //人物ID
    public string roleName;  //人物名字
    public string roleDesAddr;  //人物性格等描述图片地址
    public string roleHeadPicAddr;  //人物头像图片地址
    public string rolePicAddr;  //人物头像图片地址
}

[System.Serializable]
public class RoleInfoDic
{
    public Dictionary<int, RoleInfo> roleInfoDic = new Dictionary<int, RoleInfo>();
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
    public string[] ContentTypeTexsAddrs;
    public string contentTexsAddrsLeft;
    public string contentTexsAddrsRight;
    public string backTexsAddrsLeft;
    public string backTexsAddrsRight;
}

[System.Serializable]
public class CardManagerInfoDic
{
    public Dictionary<int, CardManagerInfo> cardManagerInfoDic = new Dictionary<int, CardManagerInfo>();
}

[System.Serializable]
public class CardResultInfo
{
    public int leftCardID;
    public int rightCardID;
    public int Score;
    public string SpecialEndName;
    public int SpecialLevel;
}


