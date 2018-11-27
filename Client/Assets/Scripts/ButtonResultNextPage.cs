using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResultNextPage : ButtonClass {

    public int nextPage = 1;

    private void OnMouseDown()
    {
        GameObject.Find("_resultManager").GetComponent<LevelResultManager>().nextPage(nextPage);
    }
}
