﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Preview : MonoBehaviour
{
    [SerializeField] public Placable Placable;

    public Vector2Int Size;
    public Vector2Int[] Ignore;
    private Vector2Int currentGridPose;

    private bool isPlacingAvailable;
    private bool isMoving;

    protected SpriteRenderer MainRenderer;
    private Color green;
    private Color red;


    private void Awake()
    {
        MainRenderer = GetComponentInChildren<SpriteRenderer>();
        green = new Color(0, 1f, .3f, .8f);
        red = new Color(1, .2f, .2f, .8f);
    }

    public void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        isMoving =true;
    }
    public void OnMouseUp()
    {
        isMoving = false;
    }

    public void SetCurrentMousePosition(Vector2 position, Vector2Int GridPose, Func<Boolean> isBuildAvailable)
    {
        if (isMoving)
        {
            if (Size.x%2==0){
                position.x += Size.x/2 - 0.5f;
            }else{
                position.x += (Size.x -1)/2;
            }
            if (Size.y%2==0){
                position.y += Size.y/2 - 0.5f;
            }else{
                position.y += (Size.y -1)/2;
            }
            transform.position = position;
            currentGridPose = GridPose;
            SetBuildAvailable(isBuildAvailable());
        }
    }

    public void SetSpawnPosition(Vector2Int GridPose)
    {
        currentGridPose = GridPose;
    }

    private bool IgnoreCheck(int x, int y)
    {
        for (int i = 0; i < Ignore.Length; i++){
            if (Ignore[i].x == x && Ignore[i].y == y)
            {
                return true;
            }
        }
        return false;
    }

    public Placable InstantiateHere()
    {
        if (isPlacingAvailable)
        {
            Vector2Int size = GetSize();

            Cell[] placeInGrid = new Cell[size.x * size.y];
            int index = 0;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (!IgnoreCheck(x,y)){
                        placeInGrid[index++] = new Cell(currentGridPose.x + x , currentGridPose.y + y);
                    }
                }
            }

            Placable placable = InitPlacable(placeInGrid);
            Destroy(gameObject);
            return placable;
        }

        return null;
    }

    private Placable InitPlacable(Cell[] placeInGrid)
    {
        Placable placable = Instantiate(Placable, transform.position, Quaternion.identity);
        placable.GridPlace = new GridPlace(placeInGrid);
        return placable;
    }

    public void SetBuildAvailable(bool available)
    {
        isPlacingAvailable = available;
        MainRenderer.material.color = available ? green : red;
    }

    public bool IsBuildAvailable()
    {
        return isPlacingAvailable;
    }

    public virtual Vector2Int GetSize()
    {
        return Size;
    }
}
