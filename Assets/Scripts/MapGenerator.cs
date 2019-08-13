using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [Serializable]
    public class TileWithWeight
    {
        public GameObject Tile;

        [Range(0, 100)]
        public int Weight;
    }


    [Range(1, 20)]
    public int Width;

    [Range(1, 20)]
    public int Heigth;

    public GameObject MapSide;

    public GameObject MapCorner;

    [Header("Tiles")]
    public List<TileWithWeight> Tiles;

    [HideInInspector]
    public GameObject[][] Map;

    public event Action<Bounds> OnMapGenerated;

    void Start()
    {
        Map = new GameObject[Heigth + 2][];

        (int from, int to, int index)[] weightIndexes = new (int, int, int)[Tiles.Count];

        int total = 0;

        for (int i = 0; i < Tiles.Count; i++)
        {
            weightIndexes[i] = (total, total + Tiles[i].Weight, i);
            total += Tiles[i].Weight;
        }

        float offsetX = 0f;
        float offsetZ = 0f;

        Renderer renderer = null;

        float maxHeight = float.MinValue;

        for (int i = 0; i < Heigth; i++)
        {
            Map[i] = new GameObject[Width];

            offsetX = 0f;

            for (int j = 0; j < Width; j++)
            {
                int rand = UnityEngine.Random.Range(0, total);

                Map[i][j] = Instantiate(Tiles[weightIndexes.First(x => rand >= x.from && rand < x.to).index].Tile, new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z + offsetZ), new Quaternion(0, 0, 0, 0), this.transform);

                renderer = Map[i][j].GetComponentInChildren<Renderer>();

                if (renderer.bounds.extents.y > maxHeight)
                    maxHeight = renderer.bounds.extents.y;

                Map[i][j].transform.RotateAround(renderer.bounds.center, Vector3.up, UnityEngine.Random.Range(0, 3) * 90);

                offsetX += renderer.bounds.size.x;
            }

            offsetZ += renderer.bounds.size.z;
        }

        Vector3 center = new Vector3(offsetX / 2 + transform.position.x, transform.position.y + maxHeight, transform.position.z + offsetZ / 2);

        Vector3 size = new Vector3(offsetX, maxHeight * 2, offsetZ);

        Bounds Bounds = new Bounds(center, size);

        if (OnMapGenerated != null)
        {
            OnMapGenerated.Invoke(Bounds);
        }
    }
}
