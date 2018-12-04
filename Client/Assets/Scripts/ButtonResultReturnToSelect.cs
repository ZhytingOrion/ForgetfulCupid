using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonResultReturnToSelect : ButtonClass {

    private void OnMouseDown()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
