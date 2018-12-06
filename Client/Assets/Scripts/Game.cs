using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    ChooseLevel,
    Play,
    Result,
}

public class Game{
    
    private static Game _instance = null;
    public static Game Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Game();
            }
            return _instance;
        }
    }

    private Game()
    {
        gameState = GameState.Start;
        gameLevel = -1;
        gameResultID = -1;
        gameMessageID = -1;
        timeAttr = 0;
        //Read CP Files and Message Files
    }

    ~Game()
    {
        //Wrtie CP Files and Message Files
    }

    public void GameReset()
    {
        gameLevel = -1;
        gameResultID = -1;
        gameMessageID = -1;
        timeAttr = 0;
        gameState = GameState.Start;
        rolePairs.Clear();
    }

    public GameState gameState;

    public int gameLevel   //游戏关卡
    {
        get; set;
    }

    public int timeAttr
    {
        get; set;
    }

    public int gameResultID  //进入的结局关卡
    {
        get; set;
    }

    public int gameMessageID
    {
        get; set;
    }

    public Dictionary<int, int> rolePairs = new Dictionary<int, int>();
    public Dictionary<int, MessageState> messageShowMap = new Dictionary<int, MessageState>();

    public void addCP(int leftRoleID, int rightRoleID)
    {
        rolePairs.Add(leftRoleID, rightRoleID);
        rolePairs.Add(rightRoleID, leftRoleID);
    }

    public bool hasCP(int roleID)
    {
        if (rolePairs.ContainsKey(roleID)) return true;
        return false;
    }

    public void finishMessage(int messageID)
    {
        messageShowMap[messageID] = MessageState.Pass;
    }

    public void addMessageInit(int messageID)
    {
        if(messageShowMap.ContainsKey(messageID))
        {
            Debug.Log("This Message has been added into map!");
        }
        else
        {
            messageShowMap.Add(messageID, MessageState.Normal);
        }
    }
}
