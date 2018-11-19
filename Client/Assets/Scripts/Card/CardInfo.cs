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

    public LocType locType;

    public bool AlwaysShowCard;

    public bool AlwaysShowType;

    public float stayTime = 3.0f;
}
