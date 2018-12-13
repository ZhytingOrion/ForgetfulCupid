using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelResultManager : MonoBehaviour {

    public List<GameObject> slots = new List<GameObject>();
    public int resultNum = 0;
    public int lineNum = 4;

    public string[] CardTypeName = { "时间", "对话", "行动" };
    public List<CardResultInfo> cardResultInfo = new List<CardResultInfo>();
    public LevelResultInfo levelResultInfo = new LevelResultInfo();
    public RoleInfo leftRole;
    public RoleInfo rightRole;
    private bool isFail = false;
    private GameObject canvas;

    [Header("布局")]
    public float cardOffsetY = 3;
    public Vector3 resultCardSize = new Vector3(1.5f, 1.5f, 1.5f);

    private bool[] isMarked;
    private int HeartValue = 0;

    private int EndID = -1;

    private enum ButtonState
    {
        LastPage,
        NextPage,
        ReturnToSelect,
        NextLevel,
        None,
    }

    //Buttons
    private GameObject buttonLastPage;
    private GameObject buttonNextPage;
    private GameObject buttonNextLevel;
    private GameObject buttonReturnToSelect;

    // Use this for initialization
    void Start() {

        buttonLastPage = GameObject.Find("ResultUI/ButtonLastPage").gameObject;
        buttonNextPage = GameObject.Find("ResultUI/ButtonNextPage").gameObject;
        buttonNextLevel = GameObject.Find("ResultUI/ButtonNextLevel").gameObject;
        buttonReturnToSelect = GameObject.Find("ResultUI/ButtonReturnToSelect").gameObject;

        GameObject.Find("ResultUI/ResultLeftRolePic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(leftRole.rolePicAddr);
        GameObject.Find("ResultUI/ResultRightRolePic").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rightRole.rolePicAddr);
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

    public void ShowCardResult(bool rightFirst)
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

        if (rightFirst)
        {
            StartCoroutine(flipCards(rightCard, leftCard));
        }
        else StartCoroutine(flipCards(leftCard, rightCard));
    }

    private IEnumerator flipCards(GameObject firstCard, GameObject lastCard)
    {
        if(!firstCard.GetComponent<CardSingle>().isFlip)
        {
            firstCard.GetComponent<CardSingle>().FlipCard(3.0f);
            yield return new WaitForSeconds(1.0f);
        }        
        if (!lastCard.GetComponent<CardSingle>().isFlip) lastCard.GetComponent<CardSingle>().FlipCard(3.0f);
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
        setButtons(ButtonState.None, ButtonState.None, ButtonState.None);

        //文字状态 & 小箭头状态
        canvas.transform.Find("TextCardType").GetComponent<Text>().text = "";
        canvas.transform.Find("TextCardType/Click").gameObject.SetActive(false);
        canvas.transform.Find("TextCardContext").GetComponent<Text>().text = "";
        canvas.transform.Find("TextCardContext/Click").gameObject.SetActive(false);
        canvas.transform.Find("TextResult").GetComponent<Text>().text = "";
        canvas.transform.Find("TextResult/Click").gameObject.SetActive(false);
        canvas.transform.Find("TextHeartValue").GetComponent<Text>().text = "";
    }

    private void setButtons(ButtonState left, ButtonState middle, ButtonState right)
    {
        switch(left)
        {
            case ButtonState.LastPage:
                buttonLastPage.SetActive(true);
                break;
            default:
                buttonLastPage.SetActive(false);
                break;
        }
        switch(right)
        {
            case ButtonState.ReturnToSelect:
                buttonNextPage.SetActive(false);
                buttonNextLevel.SetActive(false);
                buttonReturnToSelect.SetActive(true);
                break;
            case ButtonState.NextLevel:
                buttonNextPage.SetActive(false);
                buttonNextLevel.SetActive(true);
                buttonReturnToSelect.SetActive(false);
                break;
            case ButtonState.NextPage:
                buttonNextPage.SetActive(true);
                buttonNextLevel.SetActive(false);
                buttonReturnToSelect.SetActive(false);
                break;
            default:
                buttonNextPage.SetActive(false);
                buttonNextLevel.SetActive(false);
                buttonReturnToSelect.SetActive(false);
                break;
        }
    }

    public void nextLine()
    {
        if (this.isFail) return;
        if(this.EndID != -1)   //进入结局界面
        {
            Game.Instance.gameResultID = this.EndID;
            SceneManager.LoadScene("ResultScene");
            return;
        }

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
        
        switch (this.lineNum)
        {
            case 3:
                ShowCardResult(cardResult.rightFirst);
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
                    setButtons(this.resultNum > 0 ? ButtonState.LastPage : ButtonState.None, ButtonState.None, ButtonState.ReturnToSelect);
                }
                canvas.transform.Find("TextCardType").GetComponent<Text>().text = line;
                if (isMarked[this.resultNum]) nextLine();
                break;
            case 2:
                if (cardResult.rightFirst)
                {
                    line = rightRole.roleName + "：" + rightCard.context.Replace("-", "") + "\n";
                    line += leftRole.roleName + "：" + leftCard.context.Replace("-", "") + "\n";
                }
                else
                {
                    line = leftRole.roleName + "：" + leftCard.context.Replace("-", "") + "\n";
                    line += rightRole.roleName + "：" + rightCard.context.Replace("-", "") + "\n";
                }
                line += "\n";
                canvas.transform.Find("TextCardContext").GetComponent<Text>().text = line;
                canvas.transform.Find("TextCardContext/Click").GetComponent<RectTransform>().anchoredPosition = new Vector3((line.Split('\n')[1].Length + 1) * 25 - 485 ,0,0);
                canvas.transform.Find("TextCardContext/Click").gameObject.SetActive(true);
                if (cardResult.Score == -1)
                    this.EndID = cardResult.SpecialEndID;
                if (isMarked[this.resultNum]) nextLine();
                break;
            case 1:
                line = cardResult.resultString.Replace('-', '\n');
                line += "\n";
                canvas.transform.Find("TextResult").GetComponent<Text>().text = line;
                canvas.transform.Find("TextResult/Click").GetComponent<RectTransform>().anchoredPosition = new Vector3((line.Split('\n')[line.Split('\n').Length-2].Length + 1) * 25 - 485, 32 - (line.Split('\n').Length-2) * 32, 0);
                canvas.transform.Find("TextResult/Click").gameObject.SetActive(true);
                if (isMarked[this.resultNum]) nextLine();
                break;
            case 0:
                line = "心动值增加：" + cardResult.Score + "\n";

                if (!this.isMarked[resultNum])
                {
                    this.isMarked[resultNum] = true;
                    //触发特殊结局
                    if (cardResult.Score != -1) HeartValue += cardResult.Score;
                    else
                    {
                        this.EndID = cardResult.SpecialEndID;
                    }
                }

                //显示Button
                Debug.Log("关卡信息：" + levelResultInfo.levelID + "/" + levelResultInfo.successEndID);
                if (this.resultNum < this.slots.Count - 1) {
                    setButtons(this.resultNum > 0 ? ButtonState.LastPage : ButtonState.None, ButtonState.None, ButtonState.NextPage);
                }
                else if(levelResultInfo.successEndID == -1)
                {
                    if(this.HeartValue >= levelResultInfo.passScore)
                    {
                        setButtons(this.resultNum > 0 ? ButtonState.LastPage : ButtonState.None, ButtonState.None, ButtonState.NextLevel);
                        line += "心动值达标，解锁下一阶段。\n";
                    }
                    else
                    {
                        setButtons(this.resultNum > 0 ? ButtonState.LastPage : ButtonState.None, ButtonState.None, ButtonState.ReturnToSelect);
                        line += "心动值未达标，配对失败。\n";
                    }
                }
                else
                {
                    if (this.HeartValue >= levelResultInfo.passScore)
                    {
                        this.EndID = levelResultInfo.successEndID;
                        Game.Instance.addCP(leftRole.roleID, rightRole.roleID);
                    }
                    else
                    { 
                        line += "心动值未达标，配对失败。\n";
                        this.EndID = levelResultInfo.failEndID;
                    }
                }

                canvas.transform.Find("TextHeartValue").GetComponent<Text>().text = line;

                break;
            default: break;
        }
        Debug.Log(line);
    }
}
