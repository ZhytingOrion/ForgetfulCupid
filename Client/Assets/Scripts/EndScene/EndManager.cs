﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour {

    public EndInfo endInfo;

	// Use this for initialization
	void Start () {
        endInfo = GameObject.Find("_dataAssets").GetComponent<ReadAssets>().endInfoDic.dic[Game.Instance.gameResultID];
        GameObject.Find("EndPic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(endInfo.endPic);
        GameObject.Find("EndName").GetComponent<TextMesh>().text = "达成  结局" + endInfo.endID;
        GameObject.Find("EndString").GetComponent<TextMesh>().text = "“" + endInfo.endName + "”";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
