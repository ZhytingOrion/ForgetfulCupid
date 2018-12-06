using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSlide : MonoBehaviour {

    public float topY = 1.85f;
    public float bottomY = -2.7f;
    private Vector2 originMousePos;
    private float height;

	// Use this for initialization
	void Start () {
        height = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        originMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector2 currentMousPos = Input.mousePosition;
        float diffY = (currentMousPos.y - originMousePos.y) / height;
        Vector3 buttonPos = this.transform.position;
        buttonPos.y = buttonPos.y + diffY;
        if (buttonPos.y  <= topY && buttonPos.y >= bottomY)
        {
            //移动滑动条
            this.transform.position = buttonPos;
            //移动卡牌
            GameObject.Find("_MessageManager").GetComponent<MessageManager>().SlideMessages(diffY/(topY - bottomY));
        }
    }
}
