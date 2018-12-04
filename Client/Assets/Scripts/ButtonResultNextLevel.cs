using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonResultNextLevel : ButtonClass {

    private void OnMouseDown()
    {
        Game.Instance.gameLevel += 1;
        SceneManager.LoadScene("tempScene");
    }
}
