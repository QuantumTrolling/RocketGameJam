using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Drawing;

public class TileMapHolder : MonoBehaviour
{
    public Vector2Int Size;
    public int LevelNumber;
    public GameObject[] waves;
    public GameObject water;
    private GameObject[] PlacedWaves = new GameObject[100];
    public GameObject[] sand;
    public Placer placer;
    private int raund = 0;
    private int wavecnt;

    private Tilemap map;
    private GridCell[,] grid;


    private void Awake()
    {
        map = GetComponentInChildren<Tilemap>();

        grid = new GridCell[Size.x, Size.y];
        map.size = new Vector3Int(Size.x, Size.y, 0);
        var rand = new System.Random();

        Vector3 tilePosition;
        Vector3Int coordinate = new Vector3Int(0, 0, 0);
        if (LevelNumber == 1){
        for (int x = 0; x < map.size.x; x++)
        {
            for (int y = 0; y < map.size.y; y++)
            {
                coordinate.x = x; coordinate.y = y;
                tilePosition = map.CellToWorld(coordinate);
                if (x==0 || y==0 || x == (map.size.x - 1) || y == (map.size.y - 1 )){
                if ((x==0 && y==0) || (x==0 && y==(map.size.y - 1 )) || (x== (map.size.x - 1 ) && y == (map.size.y - 1 )) || (x== (map.size.x - 1 ) && y==0)){
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, true, true);
                    WaterdRender(x,y);
                } else{
                if(rand.Next(100)<60){
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, false, false);
                    SandRender(x,y);
                } else{
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, true, true);
                    WaterdRender(x,y);
                }
                }
                }else{
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, false, false);
                    SandRender(x,y);
                }
            }
        }
        WavesRender(0);
        }
        if (LevelNumber == 2){
            for (int x = 0; x < map.size.x; x++)
        {
            for (int y = 0; y < map.size.y; y++)
            {
                coordinate.x = x; coordinate.y = y;
                tilePosition = map.CellToWorld(coordinate);
                if ((x==0 && y==0) || (x==0 && y==(map.size.y - 1 )) || (x== (map.size.x - 1 ) && y == (map.size.y - 1 )) || (x== (map.size.x - 1 ) && y==0)){
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, true, true);
                    WaterdRender(x,y);
                } else{
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, false, false);
                    SandRender(x,y);
                }
            }
        }
        WavesRender(0);
        }
    }
    public void NextRound() {
        Sinking(raund);
        raund++;
        resources.Raund++;
    }

    private void Sinking(int raund){
        var rand = new System.Random();
        for (int x = raund; x < map.size.x - raund; x++){
            for (int y = raund; y < map.size.y - raund; y++){
                if(( x == raund && y == raund) || (x==raund && y==(map.size.y - 1 - raund)) || (y==raund && x == (map.size.x - 1 - raund)) || (x == (map.size.x - 1 -raund) && y == (map.size.y - 1 -raund))){
                    grid[x,y].isSinking = true;
                    grid[x,y].IsOccupied = true;
                    Destroying(x,y);
                    WaterdRender(x,y);
                }
                if ( x == raund){
                    if(grid[x,y].isSinking && grid[x,y].IsOccupied && rand.Next(100)<70){
                        grid[x + 1 ,y].isSinking = true;
                        grid[x + 1, y].IsOccupied = true;
                        Destroying(x+1,y);
                        WaterdRender(x+1,y);
                    }else if (!grid[x,y].isSinking){
                        grid[x,y].isSinking = true;
                        grid[x,y].IsOccupied = true;
                        Destroying(x,y);
                        WaterdRender(x,y);
                    }
                }
                if ( y == raund){
                    if(grid[x,y].isSinking &&  grid[x,y].IsOccupied && rand.Next(100)<60){
                        grid[x,y + 1].isSinking = true;
                        grid[x,y + 1].IsOccupied = true;
                        Destroying(x,y + 1);
                        WaterdRender(x,y + 1);
                    }else if (!grid[x,y].isSinking){
                        grid[x,y].isSinking = true;
                        grid[x,y].IsOccupied = true;
                        Destroying(x,y);
                        WaterdRender(x,y);
                    }
                }
                if ( x == map.size.x - 1 - raund){
                    if(grid[x,y].isSinking && grid[x,y].IsOccupied && rand.Next(100)<60){
                        grid[x - 1,y].isSinking = true;
                        grid[x - 1,y].IsOccupied = true;
                        Destroying(x - 1,y);
                        WaterdRender(x - 1,y);
                    }else if (!grid[x,y].isSinking){
                        grid[x,y].isSinking = true;
                        grid[x,y].IsOccupied = true;
                        Destroying(x,y);
                        WaterdRender(x,y);
                    }
                }
                if ( y == map.size.y - 1 - raund){
                    if(grid[x,y].isSinking && grid[x,y].IsOccupied && rand.Next(100)<60){
                        grid[x,y - 1].isSinking = true;
                        grid[x,y - 1].IsOccupied = true;
                        Destroying(x,y - 1);
                        WaterdRender(x,y - 1);
                    }else if (!grid[x,y].isSinking){
                        grid[x,y].isSinking = true;
                        grid[x,y].IsOccupied = true;
                        Destroying(x,y);
                        WaterdRender(x,y);
                    }
                }
            }
        }
        PlaceClean();
        for (int i=0;i<wavecnt;i++){
            Destroy(PlacedWaves[i]);
        }
        SinWavesRender(raund);
    }

    private void Destroying(int x, int y){
        Ray ray = new Ray(new Vector3(grid[x,y].centerX, grid[x,y].centerY, 10), new Vector3(0,0,-10));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Structure"){
            Destroy(hit.collider.gameObject);
        }
    }

    private bool StructureCheck(int x, int y){
        Ray ray = new Ray(new Vector3(grid[x,y].centerX, grid[x,y].centerY, 10), new Vector3(0,0,-10));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Structure"){
            return true;
        }
        return false;
    } 

    private void WaterdRender(int x, int y){
        Instantiate(water, new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0));
    }

    private void SandRender(int x, int y){

        var rnd = new System.Random();
        if (rnd.Next(100)<=30){
            Instantiate(sand[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0));
        } else if(rnd.Next(100)<=60){
            Instantiate(sand[1], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0));
        }
        else{
            Instantiate(sand[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0));
        }
    }

    private void WavesRender(int round){
        wavecnt = 0;
        for (int x = round; x< map.size.x - round; x++){
            for (int y = round; y< map.size.y - round; y++){
                if(( x == raund && y == raund) || (x==raund && y==(map.size.y - 1 - raund)) || (y==raund && x == (map.size.x - 1 - raund)) || (x == (map.size.x - 1 -raund) && y == (map.size.y - 1 -raund))){
                    continue;
                }
        if (x==round && !grid[x,y].isSinking){
            if(grid[x,y + 1].isSinking && !grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] =  Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x + 1, y].centerX, grid[x + 1, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
            else if (!grid[x,y + 1].isSinking && grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x + 1, y].centerX, grid[x + 1, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            else if (!grid[x,y + 1].isSinking && !grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
            else {
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x + 1, y].centerX, grid[x + 1, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
        }
        if (y==round && !grid[x,y].isSinking){
            if(grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y + 1].centerX, grid[x, y + 1].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            else if (!grid[x + 1,y].isSinking && grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y + 1].centerX, grid[x, y + 1].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            else if (!grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            else {
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x, y + 1].centerX, grid[x, y + 1].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
        }
        if (x == map.size.x - 1 - round && !grid[x,y].isSinking){
            if(grid[x,y + 1].isSinking && !grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x - 1, y].centerX, grid[x - 1, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
            else if (!grid[x,y + 1].isSinking && grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x - 1, y].centerX, grid[x - 1, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            else if (!grid[x,y + 1].isSinking && !grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            else {
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x - 1, y].centerX, grid[x - 1, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
        }
        if(y == map.size.y - 1 - round && !grid[x,y].isSinking){
            if(grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y - 1].centerX, grid[x, y - 1].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
            else if (!grid[x + 1,y].isSinking && grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y - 1].centerX, grid[x, y - 1].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
            else if (!grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
            else {
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x, y - 1].centerX, grid[x, y - 1].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
        }

        if (grid[x,y].isSinking){
            if (x==round){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x + 1, y].centerX, grid[x + 1, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
            if (y == round){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y + 1].centerX, grid[x, y + 1].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            if (x == map.size.x - 1 - round){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x - 1, y].centerX, grid[x - 1, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            if (y== map.size.y - 1 - round){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y - 1].centerX, grid[x, y - 1].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
        }

            }
        }
    }


    private void PlaceClean(){
        for (int x = 0; x<map.size.x;x++){
            for (int y = 0; y<map.size.y;y++){
                if (!grid[x,y].isSinking && grid[x,y].IsOccupied && !StructureCheck(x,y)){
                    grid[x,y].IsOccupied = false;
                }
            }
        }
    }


    private void SinWavesRender(int round){
        wavecnt = 0;
        for (int x = round; x< map.size.x - round; x++){
            for (int y = round; y< map.size.y - round; y++){
                if(( x == raund && y == raund) || (x==raund && y==(map.size.y - 1 - raund)) || (y==raund && x == (map.size.x - 1 - raund)) || (x == (map.size.x - 1 -raund) && y == (map.size.y - 1 -raund))){
                    continue;
                }
        if (!grid[x,y].isSinking){
            if(grid[x - 1,y].isSinking && !grid[x,y - 1].isSinking && grid[x,y + 1].isSinking){
                PlacedWaves[wavecnt++] =  Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x + 1, y].centerX, grid[x + 1, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
            else if (grid[x - 1,y].isSinking && !grid[x,y + 1].isSinking && grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x + 1, y].centerX, grid[x + 1, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            else if (grid[x - 1,y].isSinking && !grid[x,y + 1].isSinking && !grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
            else if (grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x + 1, y].centerX, grid[x + 1, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            if( grid[x,y - 1].isSinking && grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y + 1].centerX, grid[x, y + 1].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            else if (grid[x,y - 1].isSinking && !grid[x + 1,y].isSinking && grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y + 1].centerX, grid[x, y + 1].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            else if (grid[x,y - 1].isSinking && !grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90)) as GameObject;
            }
            else if (grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x, y + 1].centerX, grid[x, y + 1].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            if(grid[x + 1,y].isSinking && grid[x,y + 1].isSinking && !grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x - 1, y].centerX, grid[x - 1, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
            else if (grid[x + 1,y].isSinking && !grid[x,y + 1].isSinking && grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x - 1, y].centerX, grid[x - 1, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            else if (grid[x + 1,y].isSinking && !grid[x,y + 1].isSinking && !grid[x,y - 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180)) as GameObject;
            }
            else if (grid[x + 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x - 1, y].centerX, grid[x - 1, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
            if(grid[x,y + 1].isSinking && grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y - 1].centerX, grid[x, y - 1].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
            else if (grid[x,y + 1].isSinking && !grid[x + 1,y].isSinking && grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[0], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[1], new Vector3(grid[x, y - 1].centerX, grid[x, y - 1].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
            else if (grid[x,y + 1].isSinking && !grid[x + 1,y].isSinking && !grid[x - 1,y].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[2], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270)) as GameObject;
            }
            else if (grid[x,y + 1].isSinking){
                PlacedWaves[wavecnt++] = Instantiate(waves[3], new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
                PlacedWaves[wavecnt++] = Instantiate(waves[4], new Vector3(grid[x, y - 1].centerX, grid[x, y - 1].centerY, 0), Quaternion.Euler(0,0,0)) as GameObject;
            }
        }

            }
        }
    }

    public void SetGridPlaceStatus(GridPlace place, bool isOccupied)
    {
        foreach (var cell in place.Place)
        {
            grid[cell.x, cell.y].IsOccupied = isOccupied;
        }
    }

    public Vector2Int GetGridPosHere(Vector3 mousePos)
    {
        Vector3Int cellIndex = map.WorldToCell(mousePos);
        return new Vector2Int(cellIndex.x, cellIndex.y);
    }

    public Vector2 GetGridCellPosition(Vector2Int indecies)
    {
        if (IsAreaBounded(indecies.x, indecies.y, Vector2Int.one))
        {
            GridCell gridCell = grid[indecies.x, indecies.y];
            return new Vector2(gridCell.centerX, gridCell.centerY);
        }

        return new Vector2(indecies.x, indecies.y);

    }

    public bool IsAreaBounded(int x, int y, Vector2Int size)
    {
        bool available = x >= 0 && x <= grid.GetLength(0) - size.x;
        if (available) { return y >= 0 && y <= grid.GetLength(1) - size.y; }
        return available;
    }

    public bool IsBuildAvailable(Vector2Int gridPose, Preview preview)
    {
        bool available = IsAreaBounded(gridPose.x, gridPose.y, preview.GetSize());
        if (available && IsPlaceTaken(gridPose.x , gridPose.y , preview.GetSize())) { available = false; }

        return available;
    }

    public bool IsPlaceTaken(int placeX, int placeY, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (grid[placeX + x, placeY + y].IsOccupied) return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] != null)
                    {
                        Gizmos.color = grid[x, y].IsOccupied ? new UnityEngine.Color(1, 0.5f, 0.5f) : new UnityEngine.Color(0, 1f, 0.5f);
                        Gizmos.DrawSphere(new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), 0.1f);
                    }
                }
            }
        }
    }
}
