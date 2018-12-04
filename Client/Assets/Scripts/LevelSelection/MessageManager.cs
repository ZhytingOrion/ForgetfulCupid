using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public SelectInfoDic selectInfoDic;

	// Use this for initialization
	void Start () {
        selectInfoDic = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().selectInfoDic;
        SelectInfoArray selectInfoArray = (SelectInfoArray)Resources.Load("DataAssets/SelectInfo");
        for(int i = 0; i<selectInfoArray.dataArray.Length; ++i)
        {
            SelectInfo selectInfo = selectInfoArray.dataArray[i];
            if(selectInfo.timeAttr<=Game.Instance.timeAttr)
            {
                if(!Game.Instance.hasCP(selectInfo.leftRoleID) && !Game.Instance.hasCP(selectInfo.rightRoleID))
                {
                    //放一条信息
                    //Game需要记录哪些关卡已经玩过了
                    //Game需要记录哪些关卡打开了
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
