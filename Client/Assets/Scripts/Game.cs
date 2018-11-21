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
    }

    public GameState gameState;



}
