using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevelSelectionResetCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //重置时间

        this.transform.parent.gameObject.SetActive(false);
        Game.Instance.GameReset();
        GameObject.Find("_MessageManager").GetComponent<MessageManager>().ResetMessages();
    }
}
