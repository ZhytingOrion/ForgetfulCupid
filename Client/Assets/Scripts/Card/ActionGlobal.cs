﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGlobal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) && Game.Instance.gameState == GameState.Play)
        {
            Debug.Log("我被点击了妈欸！！！！我还在打游戏" + Time.time);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if(hit.collider != null)
            {
                if (hit.collider.tag == "Card")
                {
                    GameObject card = hit.collider.gameObject;
                    if (card.GetComponent<CardSingle>().isInSlot) return;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = -1.0f;
                    card.transform.position = mousePos;
                    card.GetComponent<CardSingle>().isDrag = true;
                    card.GetComponent<BoxCollider2D>().size = new Vector2(2.0f, 0.5f);
                }
            }
        }
        else if(Input.GetMouseButtonDown(0) && Game.Instance.gameState == GameState.Result)  //结算按钮点击结算
        {
            Debug.Log("我们去下一页吧");
            GameObject.Find("_resultManager").GetComponent<LevelResultManager>().nextPage();
        }
	}
}
