using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public CardType[] slotCardTypes;
    public List<GameObject> slots = new List<GameObject>();
    public Texture2D[] slotTypeTexs;
    public float spaceY = 1.5f;
    public float offsetY = 0.0f;

    //[System.Serializable]
    public List<CardResultInfo> answers = new List<CardResultInfo>();

	// Use this for initialization
	void Start () {
        float startY = (slotCardTypes.Length - 1) * 0.5f * this.spaceY + offsetY;
        for (int i = 0; i<slotCardTypes.Length; ++i)
        {
            GameObject slot = Instantiate((GameObject)Resources.Load("Prefabs/Slot"));
            Sprite sp = Sprite.Create(slotTypeTexs[(int)slotCardTypes[i]], slot.transform.Find("Type").GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f));
            slot.transform.Find("Type").GetComponent<SpriteRenderer>().sprite = sp;
            slot.transform.position = new Vector3(0, startY - i * this.spaceY, 0);
            slot.transform.parent = GameObject.Find("Slots").transform;
            slots.Add(slot);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isSlotsFull()
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            GameObject leftSlot = slots[i].transform.Find("LeftCard").gameObject;
            if (leftSlot.GetComponent<SlotCardInstance>().thisCard == null) return false;
            GameObject RightSlot = slots[i].transform.Find("RightCard").gameObject;
            if (RightSlot.GetComponent<SlotCardInstance>().thisCard == null) return false;
        }
        return true;
    }

    public void ResetSlots()
    {
        for(int i = 0; i<slots.Count; ++i)
        {
            GameObject leftSlot = slots[i].transform.Find("LeftCard").gameObject;
            leftSlot.GetComponent<SlotCardInstance>().resetSlot();
            GameObject RightSlot = slots[i].transform.Find("RightCard").gameObject;
            RightSlot.GetComponent<SlotCardInstance>().resetSlot();
        }
    }
}
