using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessSaver : ScriptableObject{
    public GameState gameState;
    public int gameLevel;
    public int gameResultID;
    public int gameMessageID;
    public int timeAttr;
    public List<CPpair> cpPairs;
    public List<messageMap> messMap;
}
