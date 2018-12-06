using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevelSelectionReset : MonoBehaviour {

    Transform parent;
    float rotateAngle = 0.0f;

    private void Start()
    {
        this.parent = this.transform.parent;   
    }

    private void OnMouseDown()
    {
        this.parent.Find("Warning").gameObject.SetActive(true);
    }

    private void OnMouseEnter()
    {
        StartCoroutine(rotateItself());
    }

    private void OnMouseExit()
    {
        StopAllCoroutines();
        this.transform.Rotate(new Vector3(0, 0, 1), -rotateAngle);
        rotateAngle = 0.0f;
    }

    private IEnumerator rotateItself()
    {
        while(true)
        {
            this.transform.Rotate(new Vector3(0, 0, 1), -5.0f);
            rotateAngle -= 5;
            yield return null;
        }
    }
}
