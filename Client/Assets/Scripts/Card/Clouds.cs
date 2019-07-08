using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0);
        Vector3 pos = this.transform.position;
        if (pos.y < -4) pos.y = -4;
        if (pos.y > 4) pos.y = 4;
        if (pos.x < -7) pos.x = -7;
        if (pos.x > 7) pos.x = 7;
    }

    private void OnMouseOver()
    {
        this.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
    }
}
