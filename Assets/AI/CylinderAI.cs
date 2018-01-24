using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderAI : AI {

    public void SetAI(World w, ControlledCharacter c)
    {
        ((AI)this).SetAI(w, c);
        GetComponentInParent<CharacterBody>().obj.GetComponent<Renderer>().material.color = Color.red;
    }
    public override int Decision()
    {

        return (GetComponentInParent<CharacterBody>().direction)%4;
    }
}
