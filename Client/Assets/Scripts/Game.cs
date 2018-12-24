using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
                Screen.SetResolution(1750, 900, false);
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
        GameProcessSaver saver = new GameProcessSaver();
        saver.gameState = this.gameState;
        saver.gameLevel = this.gameLevel;
        saver.gameResultID = this.gameResultID;
        saver.gameMessageID = this.gameMessageID;
        saver.timeAttr = this.timeAttr;
        foreach (KeyValuePair<int, int> pair in rolePairs)
        {
            CPpair cp = new CPpair();
            cp.roleIDL = pair.Key;
            cp.roleIDR = pair.Value;
            saver.cpPairs.Add(cp);
        }
        foreach (KeyValuePair<int, MessageState> pair in messageShowMap)
        {
            messageMap map = new messageMap();
            map.ID = pair.Key;
            map.State = pair.Value;
            saver.messMap.Add(map);
        }

        //还没有保存，考虑json
        ////json转string
        //JsonSerializer serializer = new JsonSerializer();
        //string text = JsonConvert.SerializeObject(saver, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });
        //////UnityEngine.Debug.Log(text);

        ////写入文件
        //string path = Application.dataPath + "/TexturePackerTest/TexturePackerSaver.txt";
        //StreamWriter sw = new StreamWriter(path);
        ////byte[] btext = System.Convert.FromBase64String(text);
        //sw.Write(text);

        ////UnityEngine.Debug.Log("Save Models Done.");
        //sw.Close();

        ////读取Json
        //string path = Application.dataPath + "/TexturePackerTest/TexturePackerSaver.txt";
        //string jsonString = TexturePackerAutoTool.LoadFromFile(path);
        //if (jsonString == null) return;
        //TexturePackerAutoToolSaver saver = JsonConvert.DeserializeObject<TexturePackerAutoToolSaver>(jsonString);
    }

    public void GameReset()
    {
        gameLevel = -1;
        gameResultID = -1;
        gameMessageID = -1;
        timeAttr = 0;
        gameState = GameState.Start;
        rolePairs.Clear();
        messageShowMap.Clear();
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
