using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelResultManager : MonoBehaviour {

    public List<GameObject> slots = new List<GameObject>();
    public int resultNum = 0;
    public int lineNum = 4;

    public string[] CardTypeName = { "时间", "对话", "行动" };
    public List<CardResultInfo> cardResultInfo = new List<CardResultInfo>();
    public RoleInfo leftRole;
    public RoleInfo rightRole;
    private bool isFail = false;
    private GameObject canvas;

    [Header("布局")]
    public float cardOffsetY = 3;
    public Vector3 resultCardSize = new Vector3(1.5f, 1.5f, 1.5f);

    private bool[] isMarked;
    private int HeartValue = 0;

    // Use this for initialization
    void Start() {
        leftRole = GameObject.Find("_levelManager").GetComponent<LevelInstance>().leftRole;
        GameObject.Find("ResultUI/LeftRolePic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(leftRole.rolePicAddr);
        rightRole = GameObject.Find("_levelManager").GetComponent<LevelInstance>().rightRole;
        GameObject.Find("ResultUI/RightRolePic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rightRole.rolePicAddr);
        GameObject.Find("ResultUI").SetActive(false);

        CardTypeName = DataInfo.Instance.CardTypeName;
    }

    // Update is called once per frame
    void Update() {

    }

    public void ResultSceneInit()
    {
        isFail = false;
        GameObject.Find("Result").transform.Find("ResultUI").gameObject.SetActive(true);
        if (slots.Count == 0) slots = GameObject.Find("_slotManager").GetComponent<SlotManager>().slots;
        canvas = GameObject.Find("Canvas");
        isMarked = new bool[slots.Count];
        for(int i = 0; i< slots.Count; ++i)
        {
            isMarked[i] = false;
        }
        resetUI();
        //动画在这里设置

        nextLine();
    }

    public void ShowCardResult()
    {
        if (isFail) return;
        Debug.Log("我在结算界面" + this.resultNum);

        if (this.resultNum >= slots.Count) return;
        resetPage(this.resultNum - 1);
        resetPage(this.resultNum + 1);

        GameObject slot = slots[resultNum];

        GameObject leftCard = slot.transform.Find("LeftCard").GetComponent<SlotCardInstance>().thisCard;
        GameObject rightCard = slot.transform.Find("RightCard").GetComponent<SlotCardInstance>().thisCard;
        leftCard.transform.parent = slot.transform;
        rightCard.transform.parent = slot.transform;

        Vector3 slotLoc = slot.transform.position;
        slotLoc.z = -5;
        slotLoc.y = this.cardOffsetY;    //向上的偏移量
        slot.transform.position = slotLoc;
        slot.transform.localScale = this.resultCardSize;

        //设置卡片位置：为了翻牌时也在顶层，重设z
        leftCard.transform.position = slot.transform.Find("LeftCard").transform.position - new Vector3(0, 0, 3);
        rightCard.transform.position = slot.transform.Find("RightCard").transform.position - new Vector3(0, 0, 3);
        slot.transform.Find("Type").gameObject.SetActive(false);    //去掉属性信息   NOTICE:如果之后加上复原关卡的话，要把Type先Active！
        //leftCard.transform.localScale = leftCard.GetComponent<CardSingle>().

        if (!leftCard.GetComponent<CardSingle>().isFlip)
        {
            Debug.Log("卡片信息：" + leftCard.GetComponent<CardSingle>().cardInfo.cardID);
            leftCard.GetComponent<CardSingle>().FlipCard();
        }
        if (!rightCard.GetComponent<CardSingle>().isFlip) rightCard.GetComponent<CardSingle>().FlipCard();
    }

    public void resetPage(int page)
    {
        if (page < 0 || page >= this.slots.Count) return;

        GameObject slot = slots[page];

        GameObject leftCard = slot.transform.Find("LeftCard").GetComponent<SlotCardInstance>().thisCard;
        GameObject rightCard = slot.transform.Find("RightCard").GetComponent<SlotCardInstance>().thisCard;
        leftCard.transform.parent = slot.transform;
        rightCard.transform.parent = slot.transform;

        Vector3 slotLoc = slot.transform.position;
        slotLoc.z = 0;
        slotLoc.y = 0;    //向上的偏移量
        slot.transform.position = slotLoc;
        slot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //设置卡片位置：为了翻牌时也在顶层，重设z
        leftCard.transform.position = slot.transform.Find("LeftCard").transform.position;
        rightCard.transform.position = slot.transform.Find("RightCard").transform.position;
        slot.transform.Find("Type").gameObject.SetActive(true);    //去掉属性信息   NOTICE:如果之后加上复原关卡的话，要把Type先Active！
    }

    public void nextPage(int pageOffsetNum)
    {
        if (isFail) return;
        this.resultNum += pageOffsetNum;
        Debug.Log("ResultNum:" + this.resultNum + "/" + this.slots.Count);
        if (this.resultNum < this.slots.Count && this.resultNum >= 0)
        {
            Debug.Log("显示画面" + resultNum);
            resetUI();
            this.lineNum = 4;
            //ShowCardResult();
            //nextLine();
        }
        else
        {
            if (resultNum >= this.slots.Count) this.resultNum = this.slots.Count - 1;
            else this.resultNum = 0;
            Debug.Log("打印完啦");
            //返回选关界面
        }

        //在这里切换按钮状态
    }

    public void resetUI()
    {
        //按钮状态

        //文字状态
        canvas.transform.Find("TextCardType").GetComponent<Text>().text = "";
        canvas.transform.Find("TextCardType/Click").gameObject.SetActive(false);
        canvas.transform.Find("TextCardContext").GetComponent<Text>().text = "";
        canvas.transform.Find("TextCardContext/Click").gameObject.SetActive(false);
        canvas.transform.Find("TextResult").GetComponent<Text>().text = "";
        canvas.transform.Find("TextResult/Click").gameObject.SetActive(false);
        canvas.transform.Find("TextHeartValue").GetComponent<Text>().text = "";

        //小箭头状态
    }

    public void nextLine()
    {
        if (this.isFail) return;
        this.lineNum -= 1;
        if (this.lineNum < 0 || this.lineNum > 3) return;
        if (this.resultNum < 0 || this.resultNum >= this.slots.Count) return;

        string line = "";
        GameObject slot = slots[resultNum];
        CardInfo leftCard = slot.transform.Find("LeftCard").GetComponent<SlotCardInstance>().thisCard.GetComponent<CardSingle>().cardInfo;
        CardInfo rightCard = slot.transform.Find("RightCard").GetComponent<SlotCardInstance>().thisCard.GetComponent<CardSingle>().cardInfo;
        CardResultInfo cardResult = cardResultInfo.Find(x => x.leftCardID == leftCard.cardID && x.rightCardID == rightCard.cardID);

        if(cardResult == null)
        {
            cardResult = new CardResultInfo();
            cardResult.Score = 0;
            cardResult.resultString = "什么都没有发生……";
            cardResult.SpecialEndName = null;
            cardResult.rightFirst = false;
            cardResult.SpecialEndID = -1;
            cardResult.EndPic = null;
        }

        if(!this.isMarked[resultNum])
        {
            this.isMarked[resultNum] = true;
            this.HeartValue += cardResult.Score;
        }
        
        switch (this.lineNum)
        {
            case 3:
                ShowCardResult();
                line = "卡牌属性为“" + CardTypeName[(int)leftCard.type] + "”和“" + CardTypeName[(int)rightCard.type] + "”；\n";
                if (leftCard.type == rightCard.type)
                {
                    line += "匹配成功；\n";
                    canvas.transform.Find("TextCardType/Click").gameObject.SetActive(true);
                }
                else
                {
                    line += "匹配失败；\n";
                    this.lineNum = 0;
                    isFail = true;
                }
                canvas.transform.Find("TextCardType").GetComponent<Text>().text = line;
                break;
            case 2:
                line = leftRole.roleName + "：" + leftCard.context.Replace('-',' ') + "\n";
                line += rightRole.roleName + "：" + rightCard.context.Replace('-', ' ') + "\n";
                line += "\n";
                canvas.transform.Find("TextCardContext").GetComponent<Text>().text = line;
                canvas.transform.Find("TextCardContext/Click").gameObject.SetActive(true);
                break;
            case 1:
                line = cardResult.resultString.Replace('-', '\n');
                line += "\n";
                canvas.transform.Find("TextResult").GetComponent<Text>().text = line;
                canvas.transform.Find("TextResult/Click").gameObject.SetActive(true);
                break;
            case 0:
                line = "心动值增加：" + cardResult.Score + "\n";
                canvas.transform.Find("TextHeartValue").GetComponent<Text>().text = line;
                break;
            default: break;
        }
        Debug.Log(line);
    }
}
