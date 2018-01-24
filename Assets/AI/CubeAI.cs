using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAI : AI {

    public virtual void SetAI(World w, ControlledCharacter c)
    {
        ((AI)this).SetAI(w, c);
        GetComponentInParent<CharacterBody>().obj.GetComponent<Renderer>().material.color = Color.yellow;
    }
    public override int Decision()
    {
        int x = 0;
        int y = 0;
        foreach(floor f in SearchSurronding())
        {
            if (f.charontop == charpos.GetComponentInParent<CharacterBody>())
            {
                x = charpos.GetComponentInParent<CharacterBody>().posx -
                       this.GetComponentInParent<CharacterBody>().posx;
                y = charpos.GetComponentInParent<CharacterBody>().posy -
                       this.GetComponentInParent<CharacterBody>().posy;
                if (y >= x && x >= 0)
                {
                    return 0;
                }
                else if (x > y && y >= 0)
                {
                    return 1;
                }
                else if (x >= y)
                {
                    return 2;
                }
                return 3;
            }
        }
        return (int)Random.Range(0, 4);
    }
}
