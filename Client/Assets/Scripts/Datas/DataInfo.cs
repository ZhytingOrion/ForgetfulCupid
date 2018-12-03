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
    public int bindInfoID;
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
    //行动点数
    public int leftStep;
    public int rightStep;
    //槽
    public string[] slotTypeTexsAddrs;
    public int[] slotTypes;
}

[System.Serializable]
public class CardManagerInfoDic
{
    public Dictionary<int, CardManagerInfo> cardManagerInfoDic = new Dictionary<int, CardManagerInfo>();
}

[System.Serializable]
public class CardResultInfo
{
    public int levelID;
    public int leftCardID;
    public int rightCardID;
    public bool rightFirst;
    public int Score;
    public string resultString;
    public int SpecialEndID;
    public string SpecialEndName;
    public string EndPic;
}

[System.Serializable]
public class LevelResultInfo
{
    public int levelID;
    public int passScore;
    public int maxScore;
    public int endID;
    public string endName;
    public string endPic;
}

public class LevelResultInfoDic
{
    public Dictionary<int, LevelResultInfo> dic = new Dictionary<int, LevelResultInfo>();
}

[System.Serializable]
public class SelectInfo
{
    public int messageID;
    public int levelID;
    public int timeAttr;
    public int leftRoleID;
    public int rightRoleID;
    public string message;
}

public class SelectInfoDic
{
    public Dictionary<int, SelectInfo> dic = new Dictionary<int, SelectInfo>();
}

public class DataInfo
{
    private static DataInfo instance;
    public static DataInfo Instance
    {
        get
        {
            if (instance == null)
                instance = new DataInfo();
            return instance;
        }
    }

    public string[] CardTypeName = { "时间", "对话", "行动" };
}



