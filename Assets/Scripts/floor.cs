using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor{
    public int x;
    public int y; 
    public GameObject theFloor;
    public CharacterBody charontop;
	public floor(int x,int y,GameObject f)
    {
        this.x = x;
        this.y = y;
        theFloor = GameObject.Instantiate<GameObject>(f);
        theFloor.transform.localPosition = new Vector3(x,0,y);
    }
    
}
