using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGlobal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if(hit.collider != null)
            {
                Debug.Log("hit.collider.tag:" + hit.collider.gameObject.tag); 
                if (hit.collider.tag == "Card")
                {
                    Debug.Log("找到cardl了！");
                    GameObject card = hit.collider.gameObject;
                    if (card.GetComponent<CardSingle>().isInSlot) return;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = -1.0f;
                    card.transform.position = mousePos;
                    card.GetComponent<CardSingle>().isDrag = true;
                }
            }
        }
	}
}
