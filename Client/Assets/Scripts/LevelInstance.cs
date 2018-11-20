using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelInfo
{
    public int levelIndex;
    public int leftStep;
    public int rightStep;
}

public class LevelInstance : MonoBehaviour {

    public LevelInfo levelInfo;
    public int remainLeftStep
    {
        get;set;
    }
    public int remainRightStep
    {
        get;set;
    }

    public List<GameObject> leftFlippedCards;
    public bool leftCanBeClick = true;

    public List<GameObject> rightFlippedCards;
    public bool rightCanBeClick = true;
    
	// Use this for initialization
	void Start () {
        this.remainLeftStep = levelInfo.leftStep;
        this.remainRightStep = levelInfo.rightStep;
	}

    private void OnEnable()
    {
        
    }
    
    /// <summary>
    /// 将卡片都翻回来
    /// </summary>
    /// <param name="isLeft"> 需要翻回来的卡片是否是左边的 </param>
    /// <param name="time"> 翻回来前停留的时间 </param>
    public void FlipCards(bool isLeft, float time)
    {
        if (isLeft) leftCanBeClick = false;
        else rightCanBeClick = false;
        StartCoroutine(flipCards(isLeft, time));
    }

    IEnumerator flipCards(bool isLeft, float time)
    {
        yield return new WaitForSeconds(time);
        List<GameObject> cards2Flip = isLeft ? leftFlippedCards : rightFlippedCards;
        for(int i = 0; i<cards2Flip.Count; ++i)
        {
            if (cards2Flip[i].GetComponent<CardSingle>().isFlip)
                cards2Flip[i].GetComponent<CardSingle>().FlipCard();
        }
        if (isLeft) leftFlippedCards.Clear();
        else rightFlippedCards.Clear();
        if (isLeft)
        {
            leftCanBeClick = true;
            remainLeftStep = levelInfo.leftStep;
        }
        else
        {
            rightCanBeClick = true;
            remainRightStep = levelInfo.rightStep;
        }
    }

    public void ResetSteps()
    {
        this.remainLeftStep = this.levelInfo.leftStep;
        this.remainRightStep = this.levelInfo.rightStep;
        this.leftCanBeClick = true;
        this.rightCanBeClick = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
