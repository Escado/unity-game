using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{

    private MapGenerator Map;

    // Start is called before the first frame update
    void Start()
    {
        Map = GetComponentInChildren<MapGenerator>();
        Map.OnMapGenerated += Map_OnMapGenerated;
    }

    private void Map_OnMapGenerated(Bounds obj)
    {
        Debug.Log(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
