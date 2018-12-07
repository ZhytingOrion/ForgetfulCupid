using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameProcessSaver : ScriptableObject{
    public GameState gameState;
    public int gameLevel;
    public int gameResultID;
    public int gameMessageID;
    public int timeAttr;
    public List<CPpair> cpPairs = new List<CPpair>();
    public List<messageMap> messMap = new List<messageMap>();
}
