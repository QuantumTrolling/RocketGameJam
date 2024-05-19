using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class CrabWalkTh : MonoBehaviour
{
    public GameObject crab;
    public TileMapHolder tileMap;
    public Pause pause;
    private GameObject[] newcrab = new GameObject[100];
    private int cnt = 0;
    private bool[] isMoving = new bool[100];
    private Vector3[] SpawnPoint = new Vector3[100];
    public GameObject Things;
    private bool[] isMovingBack = new bool[100];
    private Vector3[] TargetPoint = new Vector3[100];
    public Vector3[] positions;
    public int CrabCount;
    private float speed;
    private int sink = 3;
    private Animator[] animator = new Animator[100];

    void Start(){
        StartCoroutine(WaitSpawner());
        //StartCoroutine(Sinking());
        //Instantiate(Things, positions[5], Quaternion.Euler(0,0,0));

    }

    private IEnumerator Sinking(){
        if(sink>0){
        yield return new WaitForSeconds(25);
        Debug.Log("Sink");
        tileMap.NextRound();
        positions[0].x++;
        positions[0].y++;
        positions[1].x--;
        positions[1].y++;
        positions[2].x--;
        positions[2].y--;
        positions[3].x++;
        positions[3].y--;
        sink--;
        StartCoroutine(Sinking());
        }
    }

    private IEnumerator WaitSpawner(){
        if (CrabCount>0){
            speed = 4f;
            yield return new WaitForSeconds(4);
            cnt++;
            CrabSpawnth(0);
            CrabCount--;
            yield return new WaitForSeconds(1.5f);
            cnt++;
            CrabSpawnth(1);
            CrabCount--;
            yield return new WaitForSeconds(1.5f);
            cnt++;
            CrabSpawnth(2);
            CrabCount--;
            StartCoroutine(WaitSpawner());
        }
    }

    void Update()
    {
        for(int i=1;i<cnt + 1;i++){
            if(newcrab[i].IsDestroyed()){
                isMoving[i]=false;
                isMovingBack[i]=false;
            }
            if (isMoving[i]){
                MoveCrabth(i);
            }
            else if (isMovingBack[i]){
                MoveBackCrabth(i);
            }
        }
        
    }

    private void CrabSpawnth( int i){
        var random = new System.Random();
        switch(random.Next(1,4 - i) + i){
        case 1:
        SpawnPoint[cnt] = positions[0];
        TargetPoint[cnt] = positions[4];
        break;
        case 2:
        SpawnPoint[cnt] = positions[1];
        TargetPoint[cnt] = positions[5];
        break;
        case 3:
        SpawnPoint[cnt] = positions[2];
        TargetPoint[cnt] = positions[6];
        break;
        case 4:
        SpawnPoint[cnt] = positions[3];
        TargetPoint[cnt] = positions[7];
        break;
        }
        newcrab[cnt] = Instantiate(crab, SpawnPoint[cnt], Quaternion.Euler(0,0,0)) as GameObject;
        animator[cnt]=newcrab[cnt].GetComponent<Animator>();
        isMoving[cnt] = true;
    }

    private void MoveCrabth( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[i].transform.position, TargetPoint[i], speed*Time.deltaTime);
        if(newcrab[i].transform.position == TargetPoint[i]){
            isMoving[i] = false;
            isMovingBack[i]=true;
            Destroy(newcrab[i]);
        }
    }

    private void MoveBackCrabth( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[i].transform.position, SpawnPoint[i], speed*Time.deltaTime);
        animator[i].Play("CrabThiefWalk");
        if(newcrab[i].transform.position == SpawnPoint[i]){
            isMovingBack[i] = false;
            resources.resource_fishs++;
            Destroy(newcrab[i]);
        }
    }

}
