using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCheck : ButtonClass {
        
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnMouseUp()
    {
        if (Game.Instance.gameState != GameState.Play) return;
        //变色

        //行程未被排满，提示需先将行程排满
        if (!GameObject.Find("_slotManager").GetComponent<SlotManager>().isSlotsFull())
        {
            //跳出提示
            Debug.Log("行程未满");

            return;
        }
        //进入结算界面
        else
        {
            Game.Instance.gameState = GameState.Result;

            //GameObject.Find("Result").SetActive(true);

            Vector3 pos = GameObject.Find("Cards").transform.position;
            pos.z = 10;
            GameObject.Find("Cards").transform.position = pos;
            GameObject.Find("_resultManager").GetComponent<LevelResultManager>().ResultSceneInit();
        }
    }

    
}
