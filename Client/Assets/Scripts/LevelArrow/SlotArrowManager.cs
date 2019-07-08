using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotArrowManager : MonoBehaviour {

    //单例
    private static SlotArrowManager _instance = null;
    public static SlotArrowManager Instance
    {
        get
        {
            return _instance;
        }
    }

    //属性
    public CardType[] slotCardTypes;
    [Header("第一支箭的初始位置")]
    public Vector3 StartLocXYZ;    //初始位置
    [Header("箭之间的间隔")]
    public float BetweenX;    //间隔
    [Header("箭的图片（时间、对话、行动）")]
    public List<Sprite> SlotPics = new List<Sprite>();
    [Header("箭射完后的图片（时间、对话、行动）")]
    public List<Sprite> SlotFullPics = new List<Sprite>();
    [Header("属性按钮的图片（时间、对话、行动）")]
    public List<Sprite> SlotButtonPics = new List<Sprite>();
    [Header("左边第一个属性按钮的位置")]
    public Vector3 SlotButtonLeftStartXYZ;    //初始位置
    [Header("左边属性按钮之间的间隔")]
    public float BetweenXLeft;    //间隔
    [Header("右边第一个属性按钮的位置")]
    public Vector3 SlotButtonRightStartXYZ;    //初始位置
    [Header("右边属性按钮之间的间隔")]
    public float BetweenXRight;    //间隔

    //调试
    public List<GameObject> SlotArrows = new List<GameObject>();   //Arrows
    public List<GameObject> SlotArrowButtonsLeft = new List<GameObject>();   //ArrowButtons左边的
    public List<GameObject> SlotArrowButtonsRight = new List<GameObject>();   //ArrowButtons右边的

    [HideInInspector] public GameObject ActiveArrow = null;

    // Use this for initialization
    void Awake() {
        InitSlotArrows();
    }

    void Start()
    {
        SetActiveSlot(slotCardTypes[0]);
    }

    // Update is called once per frame
    void Update() {

    }

    public void InitSlotArrows()    //初始化
    {
        _instance = this;
        for (int i = 0; i < slotCardTypes.Length; ++i)
        {
            GameObject slot = Instantiate((GameObject)Resources.Load("Prefabs/SlotArrow"));
            slot.transform.position = new Vector3(StartLocXYZ.x + BetweenX * i, StartLocXYZ.y, StartLocXYZ.z);
            slot.GetComponent<SlotArrow>().Init(slotCardTypes[i], SlotPics[(int)slotCardTypes[i]], SlotFullPics[(int)slotCardTypes[i]]);
            slot.transform.parent = this.transform;
            SlotArrows.Add(slot);


            GameObject buttonL = Instantiate((GameObject)Resources.Load("Prefabs/SlotButton_Arrow"));
            buttonL.transform.position = new Vector3(SlotButtonLeftStartXYZ.x + BetweenXLeft * i, SlotButtonLeftStartXYZ.y, SlotButtonLeftStartXYZ.z);
            buttonL.GetComponent<SlotButtonArrow>().Init(slotCardTypes[i], SlotButtonPics[(int)slotCardTypes[i]], true);
            buttonL.transform.parent = this.transform;
            SlotArrowButtonsLeft.Add(buttonL);

            GameObject buttonR = Instantiate((GameObject)Resources.Load("Prefabs/SlotButton_Arrow"));
            buttonR.transform.position = new Vector3(SlotButtonRightStartXYZ.x + BetweenXRight * i, SlotButtonRightStartXYZ.y, SlotButtonRightStartXYZ.z);
            buttonR.GetComponent<SlotButtonArrow>().Init(slotCardTypes[i], SlotButtonPics[(int)slotCardTypes[i]], true);
            buttonR.transform.parent = this.transform;
            SlotArrowButtonsRight.Add(buttonR);
        }

    }

    public void ArrowEmpty(CardType slotType)
    {
        for(int i = 0; i<SlotArrows.Count; i++)
        {
            GameObject arrow = SlotArrows[i];
            if (arrow.GetComponent<SlotArrow>().slotType == slotType) continue;
            SlotArrow slot = arrow.GetComponent<SlotArrow>();
            if (slot.leftCard != null && slot.rightCard != null) continue;
            SetActiveSlot(slot.slotType);            
        }
    }

    public void SetActiveSlot(CardType slotType)
    {
        for (int i = 0; i < SlotArrows.Count; i++)
        {
            GameObject arrow = SlotArrows[i];
            if (arrow.GetComponent<SlotArrow>().slotType != slotType) arrow.GetComponent<SlotArrow>().SetActiveState(false);
            else
            {
                arrow.GetComponent<SlotArrow>().SetActiveState(true);
                this.ActiveArrow = arrow;
            }
        }
        for (int i = 0; i < SlotArrowButtonsLeft.Count; i++)
        {
            GameObject button = SlotArrowButtonsLeft[i];
            if (button.GetComponent<SlotButtonArrow>().slotType != slotType) button.GetComponent<SlotButtonArrow>().SetActiveState(false);
            else button.GetComponent<SlotButtonArrow>().SetActiveState(true);
        }
        for (int i = 0; i < SlotArrowButtonsRight.Count; i++)
        {
            GameObject button = SlotArrowButtonsRight[i];
            if (button.GetComponent<SlotButtonArrow>().slotType != slotType) button.GetComponent<SlotButtonArrow>().SetActiveState(false);
            else button.GetComponent<SlotButtonArrow>().SetActiveState(true);
        }
        CardArrowManager.Instance.SetActiveSlot(slotType);
    }
}
