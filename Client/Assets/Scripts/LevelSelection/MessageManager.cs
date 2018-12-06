using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public SelectInfoDic selectInfoDic;
    public List<SelectInfo> messageList = new List<SelectInfo>();
    public List<GameObject> messageInstance = new List<GameObject>();
    public Sprite overtimedTex;
    public Sprite passTex;
    public float startY = 1.15f;
    public float spaceY = 3.3f;

	// Use this for initialization
	void Start () {
        selectInfoDic = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().selectInfoDic;
        SelectInfoArray selectInfoArray = (SelectInfoArray)Resources.Load("DataAssets/SelectInfo");
        Debug.Log("时间属性："+ Game.Instance.timeAttr);
        for(int i = 0; i<selectInfoArray.dataArray.Length; ++i)
        {
            SelectInfo selectInfo = selectInfoArray.dataArray[i];
            if(selectInfo.timeAttr<=Game.Instance.timeAttr)
            {
                if(Game.Instance.messageShowMap.ContainsKey(selectInfo.messageID))   //这关之前已经存在：
                {
                    if (Game.Instance.hasCP(selectInfo.leftRoleID) || Game.Instance.hasCP(selectInfo.rightRoleID))    //有CP
                    {
                        if(Game.Instance.messageShowMap[selectInfo.messageID] != MessageState.Pass)
                            Game.Instance.messageShowMap[selectInfo.messageID] = MessageState.Overtimed;    //已过期
                        messageList.Add(selectInfo);
                    }
                    else   //无CP
                    {
                        messageList.Add(selectInfo);
                    }
                }
                else    //这关之前并未显示：
                {
                    if (Game.Instance.hasCP(selectInfo.leftRoleID) || Game.Instance.hasCP(selectInfo.rightRoleID))    //有CP
                    {
                        //那就别显示了……空伤人心
                    }
                    else  //无CP
                    {
                        Game.Instance.addMessageInit(selectInfo.messageID);
                        messageList.Add(selectInfo);
                    }
                }
            }
        }
        messageList.Sort((x, y) => { return -x.timeAttr.CompareTo(y.timeAttr); });   //按时间顺序排序

        for(int i = 0; i<messageList.Count; ++i)
        {
            GameObject message = Instantiate((GameObject)Resources.Load("Prefabs/AMessage"));
            message.transform.position = new Vector3(0.0f, startY - spaceY * i, -0.1f);
            message.transform.parent = GameObject.Find("Messages").transform;
            message.GetComponent<MessageSingle>().selectInfo = messageList[i];
            message.GetComponent<MessageSingle>().state = Game.Instance.messageShowMap[messageList[i].messageID];
            message.GetComponent<MessageSingle>().passTex = this.passTex;
            message.GetComponent<MessageSingle>().overtimedTex = this.overtimedTex;
            messageInstance.Add(message);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SlideMessages(float diffY)
    {
        float MaxY = messageInstance[messageInstance.Count - 1].transform.position.y - messageInstance[0].transform.position.y;
        for(int i = 0; i<messageInstance.Count; ++i)
        {
            messageInstance[i].transform.position += new Vector3(0, diffY * MaxY, 0);
        }
    }
}
