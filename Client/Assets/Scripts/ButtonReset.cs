using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : ButtonClass {

    private void OnMouseDown()
    {
        //变色

        //重置
        GameObject.Find("_cardManager").GetComponent<CardManager>().ResetCards();
        GameObject.Find("_slotManager").GetComponent<SlotManager>().ResetSlots();
    }

}
