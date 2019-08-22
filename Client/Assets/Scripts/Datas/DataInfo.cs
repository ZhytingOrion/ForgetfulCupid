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
public enum MessageState
{
    Pass,
    Overtimed,
    Normal,
}

[System.Serializable]
public class CardInfo {
    public int cardID;   //卡片编号
    public CardType type;     //卡片属性
    public string context;    //内容
    public LocType locType;   //左边还是右边
    public bool AlwaysShowCard;  //翻牌版本用
    public bool AlwaysShowType;  //翻牌版本，射箭版本已取消
    public int levelID;  //对应关卡号
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
    public int levelID;         //关卡id
    public int roleLeftID;      //左角色id
    public int roleRightID;      //右角色id
    public int[] CardsLeftID;   //左卡牌
    public int[] CardsRightID;  //右卡牌
    public int[] slotTypes;     //槽
    public string levelName;    //关卡名
    public string roleLeftName;  //左角色名
    public string roleLeftDesPic;  //左角色对话框图
    public string roleRightName;   //右角色名
    public string roleRightDesPic;  //右角色对话框图
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
}

[System.Serializable]
public class LevelResultInfo
{
    public int levelID;
    public int passScore;
    public int maxScore;
    public int successEndID;
    public string successEndName;
    public int failEndID;
    public string failEndName;
}

[System.Serializable]
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
    public bool isNew = false;
}

[System.Serializable]
public class SelectInfoDic
{
    public Dictionary<int, SelectInfo> dic = new Dictionary<int, SelectInfo>();
}

[System.Serializable]
public class EndInfo
{
    public int endID;
    public string endName;
    public string endPic;
}

[System.Serializable]
public class EndInfoDic
{
    public Dictionary<int, EndInfo> dic = new Dictionary<int, EndInfo>();
}

[System.Serializable]
public class CPpair
{
    public int roleIDL;
    public int roleIDR;
}

[System.Serializable]
public class messageMap
{
    public int ID;
    public MessageState State;
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



