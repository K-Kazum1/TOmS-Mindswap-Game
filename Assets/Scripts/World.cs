using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World: MonoBehaviour{
    public int height;
    public int width;
    floor[,] floors;
    int tick;
    public List<CharacterBody> chars;
    public List<CharacterBody> executionlist;
    bool playermoved;
    ControlledCharacter player;
    public Camera camera;
    bool playeralive;
    public GameObject orange;
    public GameObject red;
    public void Start()
    {
        MakeWorld();
        SpawnBody(0);
        for(int i = 0; i < 40; i++)
        {
            SpawnBody((int)Random.Range(1,4));
        }
    }
    public void MakeWorld()
    {
        floors = new floor[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    floors[i, j] = new floor(i, j, orange);
                }
                else
                {
                    floors[i, j] = new floor(i, j, red);
                }
            }
        }
    }
    public void Update()
    {

        //Debug.Log(input);
        if (playeralive)
        {
            int input = player.Decision();
            if (input != -1)
            {
                if (input < 4)
                {
                    playermoved = player.GetComponentInParent<CharacterBody>().Move(input);

                }
                else
                {
                    CharacterBody toswap=null;
                    foreach(floor f in GetAdjacent(GetFloor(
                        player.GetComponentInParent<CharacterBody>().posx,
                        player.GetComponentInParent<CharacterBody>().posy))
                       ){
                            if (f.charontop != null) toswap = f.charontop;
                        }
                    if (toswap != null)
                    {
                        if (toswap.GetComponent<AI>().GetType().Name == "CylinderAI")
                        {
                            Destroy(toswap.GetComponentInParent<AI>());
                            CylinderAI ai=player.GetComponentInParent<CharacterBody>().obj.AddComponent<CylinderAI>();
                            Destroy(player);
                            player=toswap.obj.AddComponent<ControlledCharacter>();
                            ai.SetAI(this, player);
                            
                        }
                        else if (toswap.GetComponent<AI>().GetType().Name == "SphereAI")
                        {
                            Destroy(toswap.GetComponentInParent<AI>());
                            SphereAI ai=player.GetComponentInParent<CharacterBody>().obj.AddComponent<SphereAI>();
                            Destroy(player);
                            player = toswap.obj.AddComponent<ControlledCharacter>();
                            ai.SetAI(this, player);
                        }
                        else if (toswap.GetComponent<AI>().GetType().Name == "CubeAI")
                        {
                            Destroy(toswap.GetComponentInParent<AI>());
                            CubeAI ai=player.GetComponentInParent<CharacterBody>().obj.AddComponent<CubeAI>();
                            Destroy(player);
                            player = toswap.obj.AddComponent<ControlledCharacter>();
                            ai.SetAI(this, player);
                        }
                        player.SetAI(this,player);
                        foreach(CharacterBody body in chars)
                        {
                            body.GetComponentInParent<AI>().SetAI(this,player);
                        }
                        


                    }
                }
                CameraMove(
                    player.GetComponentInParent<CharacterBody>().posx,
                    player.GetComponentInParent<CharacterBody>().posy
                    );
            }
            else if (playermoved)
            {
                for (int i = 0; i < player.GetComponentInParent<CharacterBody>().movespeed; i++)
                {
                    Tick();
                    tick++;
                }
                playermoved = false;
            }
        }
    }
    public void Tick()
    {
        foreach(CharacterBody c in chars)
        {
            if (c!=player.GetComponentInParent<CharacterBody>())c.Turn();
        }
        foreach(CharacterBody c in executionlist)
        {
            chars.Remove(c);
            Destroy(c.obj);
            SpawnBody((int)Random.Range(1, 4));
        }
        executionlist = new List<CharacterBody>();
    }
    public void SpawnBody(int type)
    {
        floor spawnpoint=null;
        GameObject body;
        CharacterBody character=new CharacterBody();
        for(int i =0;i<50;i++)
        {
            spawnpoint = GetFloor((int)(Random.value * width), 
                                  (int)(Random.value * height));
            if (!spawnpoint.charontop) break;
        }
        switch (type)
        {
            case 0:
                body=GameObject.CreatePrimitive(PrimitiveType.Capsule);
                body.transform.position = new Vector3(spawnpoint.x, 1.5f, spawnpoint.y);
                character = body.AddComponent<CharacterBody>();
                player=body.AddComponent<ControlledCharacter>();
                character.CharacterSet(this, spawnpoint, 2, 10, 1, 2, body);
                player.SetAI(this, player);
                camera.transform.position = new Vector3(spawnpoint.x,10,spawnpoint.y-5);
                playeralive = true;
                break;
            case 1:
                body = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                body.transform.position = new Vector3(spawnpoint.x, 1.5f, spawnpoint.y);
                character = body.AddComponent<CharacterBody>();
                character.CharacterSet(this, spawnpoint, 1, 3, 10, 4, body);
                body.AddComponent<CylinderAI>().SetAI(this, player);
                character.direction = (int)(Random.Range(0,4));
                break;
            case 2:
                body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                body.transform.position = new Vector3(spawnpoint.x, 1.5f, spawnpoint.y);
                character = body.AddComponent<CharacterBody>();
                character.CharacterSet(this, spawnpoint, 4, 30, 1, 6, body);
                body.AddComponent<SphereAI>().SetAI(this, player);
                break;
            case 3:
                body = GameObject.CreatePrimitive(PrimitiveType.Cube);
                body.transform.position = new Vector3(spawnpoint.x, 1.5f, spawnpoint.y);
                character = body.AddComponent<CharacterBody>();
                character.CharacterSet(this, spawnpoint, 20, 30, 1, 8, body);
                body.AddComponent<CubeAI>().SetAI(this, player);
                break;
        }
        spawnpoint.charontop = character;
        chars.Add(character);
    }
    public void Kill(CharacterBody body)
    {
        if (chars.Contains(body))
        {
            if (body == player.GetComponentInParent<CharacterBody>()) playeralive = false;
            executionlist.Add(body);
        }
    }
    public void CameraMove(int x,int y)
    {
        camera.transform.position = new Vector3(x,10,y-5);
    }
    public floor GetFloor(int x,int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return null;
        return floors[x, y];
    }

    public List<floor> GetAdjacent(floor f)
    {
        List<floor> list=new List<floor>();
        int x = f.x;
        int y = f.y;
        if (x > 0)          list.Add(GetFloor(x - 1, y));
        if (y > 0)          list.Add(GetFloor(x, y - 1));
        if (x < width - 1)  list.Add(GetFloor(x + 1, y));
        if (y < height - 1) list.Add(GetFloor(x, y + 1));
        return list;
    }
}
