using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public World world;
    public ControlledCharacter charpos;
    public virtual void SetAI(World w, ControlledCharacter c)
    {
        world = w;
        charpos = c;
    }
    public virtual int Decision()
    {
        return (int)(Random.value*4);
    }
    public List<floor> SearchSurronding()
    {
        List<floor> resultlist = new List<floor>();
        List<floor> searchlist = new List<floor>();
        List<floor> nextlist;
        CharacterBody body = GetComponentInParent<CharacterBody>();
        searchlist.Add(world.GetFloor(body.posx, body.posy));
        for(int i = 0; i < body.eyesight; i++)
        {
            nextlist = new List<floor>();
            foreach (floor flooor in searchlist)
            {
                foreach (floor floooor in world.GetAdjacent(flooor))
                {
                    if (!resultlist.Contains(floooor))
                    {
                        resultlist.Add(floooor);
                        nextlist.Add(floooor);
                    }
                }
            }
            searchlist = nextlist;
        }
        return resultlist;
    }
}
