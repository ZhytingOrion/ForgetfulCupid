﻿using System.Collections;
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
        timeAttr = 0;
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
}
