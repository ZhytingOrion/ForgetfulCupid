using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToScene : ButtonClass {

    public string SceneName;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneName);

        //to delete:
        Game.Instance.gameLevel = 0;
    }
}
