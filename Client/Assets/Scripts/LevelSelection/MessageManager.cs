using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public List<SelectInfo> messageList = new List<SelectInfo>();
    public List<GameObject> messageInstance = new List<GameObject>();
    public List<int> newMessageList = new List<int>();
    public Sprite overtimedTex;
    public Sprite passTex;
    public float startY = 1.15f;
    public float spaceY = 3.3f;
    public int currentIndex = 0;

    private SelectInfoDic selectInfoDic;
    private SelectInfoArray selectInfoArray;

    // Use this for initialization
    void Start () {
        selectInfoDic = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().selectInfoDic;
        selectInfoArray = (SelectInfoArray)Resources.Load("DataAssets/SelectInfo");
        ResetMessages();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetMessages()
    {
        for(int i = 0; i<messageInstance.Count; ++i)
        {
            Destroy(messageInstance[i]);
        }

        messageList.Clear();
        messageInstance.Clear();

        Debug.Log("时间属性：" + Game.Instance.timeAttr);
        for (int i = 0; i < selectInfoArray.dataArray.Length; ++i)
        {
            SelectInfo selectInfo = selectInfoArray.dataArray[i];
            if (selectInfo.timeAttr <= Game.Instance.timeAttr)
            {
                if (Game.Instance.messageShowMap.ContainsKey(selectInfo.messageID))   //这关之前已经存在：
                {
                    if (Game.Instance.hasCP(selectInfo.leftRoleID) || Game.Instance.hasCP(selectInfo.rightRoleID))    //有CP
                    {
                        if (Game.Instance.messageShowMap[selectInfo.messageID] != MessageState.Pass)
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
                        selectInfo.isNew = true;
                        messageList.Add(selectInfo);                        
                    }
                }
            }
        }
        messageList.Sort((x, y) => { return -x.timeAttr.CompareTo(y.timeAttr); });   //按时间顺序排序        
        for (int i = 0; i < messageList.Count; ++i)
        {
            GameObject message = Instantiate((GameObject)Resources.Load("Prefabs/AMessage"));
            if (messageList[i].isNew)
            {
                message.transform.position = new Vector3(15.27f, startY - spaceY * i, -0.1f);
                newMessageList.Add(i);
            }
            else
            {
                message.transform.position = new Vector3(0.27f, startY - spaceY * i, -0.1f);
            }
            message.transform.parent = GameObject.Find("Messages").transform;
            message.GetComponent<MessageSingle>().selectInfo = messageList[i];
            message.GetComponent<MessageSingle>().state = Game.Instance.messageShowMap[messageList[i].messageID];
            message.GetComponent<MessageSingle>().passTex = this.passTex;
            message.GetComponent<MessageSingle>().overtimedTex = this.overtimedTex;
            messageInstance.Add(message);
        }

        if(newMessageList.Count>0)
        {
            float moveY = (Mathf.Min(newMessageList[newMessageList.Count - 1] + 1, messageInstance.Count - 1) - this.currentIndex) * spaceY;
            for (int i = 0; i<messageInstance.Count; ++i)
            {
                messageInstance[i].transform.position += new Vector3(0, moveY, 0);
            }
            this.currentIndex = Mathf.Min(newMessageList[newMessageList.Count - 1] + 1, messageInstance.Count - 1);
        }
        StartCoroutine(messagesBound());
    }

    private IEnumerator messagesBound()
    {
        for(int j = newMessageList.Count-1; j>=0; j--)
        {
            Debug.Log("翻牌计划：" + newMessageList[j]);
            int bottomMessageIndex = Mathf.Min(newMessageList[j] + 1, messageInstance.Count - 1);

            Debug.Log("下面一张牌：" + bottomMessageIndex);
            float moveY = (bottomMessageIndex-this.currentIndex) * spaceY;
            Debug.Log("头牌：" + this.currentIndex);
            Debug.Log("移动：" + moveY);
            if (Mathf.Abs(moveY)>=0.01f)
            {
                yield return StartCoroutine(messagesSlide(moveY, 0.5f));
            }
            if (newMessageList[j] + 1 <= messageInstance.Count - 1)
            {
                yield return new WaitForSeconds(0.2f);
                yield return StartCoroutine(messagesSlide(-spaceY));
                yield return new WaitForSeconds(0.2f);
            }
            yield return StartCoroutine(messageInstance[newMessageList[j]].GetComponent<MessageSingle>().Bound());
            currentIndex = newMessageList[j];
        }
    }

    private IEnumerator messagesSlide(float moveY, float time = 0.5f)
    {
        for(float t = 0;t<time; t+=Time.deltaTime)
        {
            for(int i = 0; i<messageInstance.Count; ++i)
            {
                messageInstance[i].transform.position += new Vector3(0, moveY / time * Time.deltaTime,0);
            }
            yield return null;
        }
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
