using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResultManager : MonoBehaviour {

    public List<GameObject> slots = new List<GameObject>();
    public int resultNum = 0;
    public int lineNum = 4;

    public string[] CardTypeName = { "时间", "对话", "行动" };
    public List<CardResultInfo> cardResultInfo = new List<CardResultInfo>();
    public RoleInfo leftRole;
    public RoleInfo rightRole;
    private bool isFail = false;

    // Use this for initialization
    void Start () {
        leftRole = GameObject.Find("_levelManager").GetComponent<LevelInstance>().leftRole;
        GameObject.Find("ResultUI/LeftRolePic").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(leftRole.rolePicAddr);
        rightRole = GameObject.Find("_levelManager").GetComponent<LevelInstance>().rightRole;
        GameObject.Find("ResultUI/RightRolePic").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(rightRole.rolePicAddr);
        GameObject.Find("ResultUI").SetActive(false);

        CardTypeName = DataInfo.Instance.CardTypeName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResultSceneInit()
    {
        isFail = false;
        GameObject.Find("Result").SetActive(true);
        if (slots.Count == 0) slots = GameObject.Find("_slotManager").GetComponent<SlotManager>().slots;
        //动画在这里设置

        nextLine();
    }

    public void ShowCardResult()
    {
        if (isFail) return;
        Debug.Log("我在结算界面" + this.resultNum);
        
        if (this.resultNum >= slots.Count) return;
        if (this.resultNum > 0) slots[resultNum - 1].transform.position = new Vector3(0, 0, 0);
        GameObject slot = slots[resultNum];

        GameObject leftCard = slot.transform.Find("LeftCard").GetComponent<SlotCardInstance>().thisCard;
        GameObject rightCard = slot.transform.Find("RightCard").GetComponent<SlotCardInstance>().thisCard;
        leftCard.transform.parent = slot.transform;
        rightCard.transform.parent = slot.transform;

        Vector3 slotLoc = slot.transform.position;
        slotLoc.z = -5;
        slotLoc.y = 0;
        slot.transform.position = slotLoc;
        slot.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        leftCard.transform.position = slot.transform.Find("LeftCard").transform.position - new Vector3(0,0,3);
        rightCard.transform.position = slot.transform.Find("RightCard").transform.position - new Vector3(0, 0, 3);
        //leftCard.transform.localScale = leftCard.GetComponent<CardSingle>().

        if (!leftCard.GetComponent<CardSingle>().cardInfo.AlwaysShowCard)
        {
            Debug.Log("卡片信息：" + leftCard.GetComponent<CardSingle>().cardInfo.cardID);
            leftCard.GetComponent<CardSingle>().FlipCard();
        }
        if (!rightCard.GetComponent<CardSingle>().cardInfo.AlwaysShowCard) rightCard.GetComponent<CardSingle>().FlipCard();
    }

    public void nextPage(int pageOffsetNum)
    {
        if (isFail) return;
        this.resultNum += pageOffsetNum;
        Debug.Log("ResultNum:" + this.resultNum + "/" + this.slots.Count);
        if (this.resultNum < this.slots.Count && this.resultNum >= 0)
        {
            Debug.Log("显示画面" + resultNum);
            this.lineNum = 4;
            ShowCardResult();
        }
        else
        {
            Debug.Log("打印完啦");
            //返回选关界面
        }

        //在这里切换按钮状态
    }    

    public void nextLine()
    {
        if (this.isFail) return;
        this.lineNum -= 1;
        string line = "";
        GameObject slot = slots[resultNum];
        CardInfo leftCard = slot.transform.Find("LeftCard").GetComponent<SlotCardInstance>().thisCard.GetComponent<CardSingle>().cardInfo;
        CardInfo rightCard = slot.transform.Find("RightCard").GetComponent<SlotCardInstance>().thisCard.GetComponent<CardSingle>().cardInfo;
        CardResultInfo cardResult = cardResultInfo.Find(x => x.leftCardID == leftCard.cardID && x.rightCardID == rightCard.cardID);
        if(cardResult == null) cardResult = cardResultInfo.Find(x => x.leftCardID == -1 && x.rightCardID == -1);
        switch (this.lineNum)
        {
            case 3:
                ShowCardResult();
                line = "卡牌属性为“" + CardTypeName[(int)leftCard.type] + "”和“" + CardTypeName[(int)rightCard.type] + "”；\n";
                if (cardResult.Score > 0) line += "匹配成功；\n";
                else
                {
                    line += "匹配失败；\n";
                    this.lineNum = 0;
                    line += "\n" + cardResult.resultString + "\n";
                    isFail = true;
                }
                break;
            case 2:
                line = leftRole.roleName + "：" + leftCard.context + "\n";
                line += rightRole.roleName + "：" + rightCard.context + "\n";
                line += "\n";
                break;
            case 1:
                line = cardResult.resultString.Replace('-', '\n');
                line += "\n";
                break;
            case 0:
                line = "心动值增加：" + cardResult.Score + "\n";
                break;
            default: break;
        }
        Debug.Log(line);
    }
}
