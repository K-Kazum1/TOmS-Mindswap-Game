using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : MonoBehaviour {
    World world;
    public GameObject obj;
    public int movetick=0;
    public int posx;
    public int posy;
    public int eyesight;
    public int health;
    public int attack;
    public int movespeed;
    public int direction;
    public void CharacterSet(World w, floor f, int e, int h, int a, int s,GameObject g)
    {
        world     = w;
        posx      = f.x;
        posy      = f.y;
        eyesight  = e;
        health    = h;
        attack    = a;
        movespeed = s;
        obj       = g;        
    }
    public void Turn()
    {
        movetick += 1;
        if (movetick == movespeed)
        {
            movetick = 0;            
            Move(obj.GetComponent<AI>().Decision());
        }
    }
    public bool Move(int direction)
    {
        int movedposx=0;
        int movedposy=0;
        this.direction = direction;
        switch (direction) {
            case 0:
                movedposx = 0;
                movedposy = 1;
                break;
            case 1:
                movedposx = 1;
                movedposy = 0;
                break;
            case 2:
                movedposx = 0;
                movedposy = -1;
                break;
            case 3:
                movedposx = -1;
                movedposy = 0;
                break;
        }
        floor f = world.GetFloor(posx + movedposx, posy + movedposy);
        if (f != null)
        {
            if (f.charontop == null)
            {
                world.GetFloor(posx, posy).charontop = null;
                posx += movedposx;
                posy += movedposy;
                world.GetFloor(posx, posy).charontop = this;
                obj.transform.Translate(movedposx, 0, movedposy);   
            }
            else
            {
                f.charontop.health -= attack;
                if (f.charontop.health < 0)
                {
                    world.Kill(f.charontop);
                }
            }
            return true;
        }
        return false;
    }
}
