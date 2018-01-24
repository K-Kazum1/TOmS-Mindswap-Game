using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledCharacter: AI {
    public virtual void SetAI(World w, ControlledCharacter c)
    {
        ((AI)this).SetAI(w, c);
        GetComponentInParent<CharacterBody>().obj.GetComponent<Renderer>().material.color = Color.black;
    }
    public override int Decision()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))    return 0;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))  return 2;
        if (Input.GetKeyDown(KeyCode.LeftArrow))  return 3;
        if (Input.GetKeyDown(KeyCode.Space))      return 4;
        return -1;
    }

}
