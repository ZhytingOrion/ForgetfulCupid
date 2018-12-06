﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageSingle : MonoBehaviour {

    public SelectInfo selectInfo = new SelectInfo();
    public MessageState state = MessageState.Normal;
    public Sprite overtimedTex;
    public Sprite passTex;

	// Use this for initialization
	void Start () {
        if (selectInfo.timeAttr > Game.Instance.timeAttr) return;
        this.transform.Find("Message").GetComponent<TextMesh>().text = selectInfo.message.Replace('-', '\n');
        if (this.state == MessageState.Overtimed) this.transform.Find("State").GetComponent<SpriteRenderer>().sprite = overtimedTex;
        if (this.state == MessageState.Pass) this.transform.Find("State").GetComponent<SpriteRenderer>().sprite = passTex;
        if (this.state == MessageState.Normal) this.transform.Find("State").GetComponent<SpriteRenderer>().sprite = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        this.transform.Find("HighLight").gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        this.transform.Find("HighLight").gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        Game.Instance.gameLevel = this.selectInfo.levelID;
        Game.Instance.gameMessageID = this.selectInfo.messageID;
        SceneManager.LoadScene("LevelGame");
    }
}
