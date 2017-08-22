using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSlot : MonoBehaviour {

    private Hammer target;

	void Start () {
		
	}

    public void Spawn()
    {
        GameObject hammer = (GameObject.Instantiate(Resources.Load("Hammer")) as GameObject);
        target = hammer.GetComponent<Hammer>().Setup(this);
    }
    
}
