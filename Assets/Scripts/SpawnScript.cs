using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject Player;

    public MapGenerator Map { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Map = GetComponentInChildren<MapGenerator>();
        Map.OnMapGenerated += Map_OnMapGenerated;        
    }

    private void Map_OnMapGenerated(Bounds obj)
    {
        var player = Instantiate(Player, Vector3.zero, new Quaternion(), this.transform);

        var renderer = Player.GetComponentInChildren<Renderer>();

        player.transform.position = new Vector3(obj.center.x - renderer.bounds.size.x / 2, GameObject.Find("SpawnPoint").transform.position.y, obj.center.z - renderer.bounds.size.z / 2);
        
    }
}
