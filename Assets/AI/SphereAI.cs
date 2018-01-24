using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAI : AI {
    public virtual void SetAI(World w, ControlledCharacter c)
    {
        ((AI)this).SetAI(w, c);
        GetComponentInParent<CharacterBody>().obj.GetComponent<Renderer>().material.color = Color.blue;
    }
    public override int Decision()
    {

        return (GetComponentInParent<CharacterBody>().direction+1) % 4;
    }
}
