using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class generationnoise : MonoBehaviour
{
    public Vector2Int size;
    public float zoom;
    public Vector2 offset;
    public Tilemap tm;
    public Tile[] tile;
    public float intensivity;
    public float cutPlane;


    public void genTileCave() 
    {
        tm.ClearAllTiles();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var p = Mathf.PerlinNoise(((x + offset.x) / zoom)* intensivity, (  (y + offset.y) / zoom) * intensivity );
                var gr = p;

                var t = tile[Random.Range(1, 3)];
                t.color = getColorBy(gr);
                if (gr > cutPlane) 
                {
                    tm.SetTile(new Vector3Int(x, y, 0), t);
                }   
            }
        }
    }
    public Color getColorBy(float inp)
    {
        var output_color = new Color(0, 0, 0, 1);
        if (inp < 0.3f)
        {
            output_color = new Color(17f/255f, 21f/255f, 150f/255f, 1);
        }
        else { output_color = new Color(245f/255f, 239f/ 255f, 73f/255f, 1); }
        return output_color;
    }
    void GetTex() 
    {
        var tex = new Texture2D(size.x, size.y);
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var gr = Mathf.PerlinNoise((x + offset.x) / zoom, (y + offset.y) / zoom);
                tex.SetPixel(x, y, getColorBy(gr));

            }
        }
        tex.Apply();
        var spr = Sprite.Create(tex, new Rect(0, 0, size.x, size.y), new Vector2(0, 0));
        GetComponent<SpriteRenderer>().sprite = spr;
    }
    void Update()
    {
        genTileCave();
    }
}
