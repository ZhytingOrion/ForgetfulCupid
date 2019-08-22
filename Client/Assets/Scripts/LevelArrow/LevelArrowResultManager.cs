using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArrowResultManager : MonoBehaviour {
    
    public List<CardResultInfo> cardResultInfo = new List<CardResultInfo>();
    public LevelResultInfo levelResultInfo = new LevelResultInfo();
    public RoleInfo leftRole;
    public RoleInfo rightRole;
    private bool isFail = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
