﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapHolder : MonoBehaviour
{
    public Vector2Int Size;
    public GameObject wave;
    public GameObject water;
    public GameObject[] sand;

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
        for (int x = 0; x < map.size.x; x++)
        {
            for (int y = 0; y < map.size.y; y++)
            {
                coordinate.x = x; coordinate.y = y;
                tilePosition = map.CellToWorld(coordinate);
                if (x==0 || y==0 || x == (map.size.x - 1) || y == (map.size.y - 1 )){
                if(rand.Next(100)<50){
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, false);
                    SandRender(x,y);
                    WavesRender(x,y);
                } else{
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, true);
                    WaterdRender(x,y);
                }
                }else{
                    grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, false);
                    SandRender(x,y);
                }
            }
        }
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

    private void WavesRender(int x, int y){
        if (x==0 && y!=0 && y != (map.size.y - 1)){
            Instantiate(wave, new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,0));
        }
        if (x!=0 && y==0 && x!=(map.size.x - 1)){
            Instantiate(wave, new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,90));
        }
        if (x==(map.size.x - 1) && y != (map.size.y - 1) && y!= 0){
            Instantiate(wave, new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,180));
        }
        if (x!=(map.size.x - 1) && y == (map.size.y - 1) && x!= 0){
            Instantiate(wave, new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), Quaternion.Euler(0,0,270));
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
        if (available && IsPlaceTaken(gridPose.x, gridPose.y, preview.GetSize())) { available = false; }

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
                        Gizmos.color = grid[x, y].IsOccupied ? new Color(1, 0.5f, 0.5f) : new Color(0, 1f, 0.5f);
                        Gizmos.DrawSphere(new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), 0.1f);
                    }
                }
            }
        }
    }
}
