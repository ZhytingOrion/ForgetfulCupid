using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEndSceneBackToSelect : ButtonClass {

    private void OnMouseDown()
    {
        //该结局已达成的成就
        
        SceneManager.LoadScene("LevelSelect");
    }
}
