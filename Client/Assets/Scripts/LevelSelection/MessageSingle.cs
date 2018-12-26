using System.Collections;
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

    public IEnumerator Bound()
    {
        
        float x0 = 5.57f, x1 = -2.53f, x2 = 0.27f, time = 0;
        float t0 = 0.13f, t1 = 0.19f, t2 = 0.25f;
        Vector3 pos0 = this.transform.position;
        pos0.x = x0;
        this.transform.position = pos0;
        for (time = 0; time < t0; time += Time.deltaTime)
        {
            this.transform.position += new Vector3((x2-x0) /t0 * Time.deltaTime, 0, 0);
            this.transform.localScale -= new Vector3(0.22f /t0 * Time.deltaTime, 0, 0);
            yield return null;
        }
        for (; time < t1; time += Time.deltaTime)
        {
            this.transform.position += new Vector3((x1-x2) / (t1-t0) * Time.deltaTime, 0, 0);
            this.transform.localScale -= new Vector3(0.06f / (t1 - t0) * Time.deltaTime, 0, 0);
            yield return null;
        }
        for (; time < t2; time += Time.deltaTime)
        {
            this.transform.position += new Vector3((x2-x1) / (t2-t1) * Time.deltaTime, 0, 0);
            this.transform.localScale += new Vector3(0.28f / (t2 - t1) * Time.deltaTime, 0, 0);
            yield return null;
        }
        Vector3 pos = this.transform.position;
        pos.x = 0.27f;
        this.transform.position = pos;
        Vector3 scale = this.transform.localScale;
        scale.x = scale.y;
        this.transform.localScale = scale;
    }

    private void OnMouseEnter()
    {
        if (this.state != MessageState.Normal) return;
        this.transform.Find("HighLight").gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        this.transform.Find("HighLight").gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (this.state != MessageState.Normal) return;
        Game.Instance.gameLevel = this.selectInfo.levelID;
        Game.Instance.gameMessageID = this.selectInfo.messageID;
        SceneManager.LoadScene("LevelGame");
    }
}
