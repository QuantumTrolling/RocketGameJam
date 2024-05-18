using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;

public class CrabWalk : MonoBehaviour
{
    public GameObject crab;
    private GameObject[] newcrab = new GameObject[100];
    private int cnt = 0;
    private bool[] isMoving = new bool[100];
    private Vector3[] SpawnPoint = new Vector3[100];
    private bool Spawn;
    private bool[] isMovingBack = new bool[100];
    private Vector3[] TargetPoint = new Vector3[100];
    public Vector3[] positions;
    public int CrabCount;
    private int diff = 1;
    private Animator animator;

    void Start(){
        animator = crab.GetComponent<Animator>();
        StartCoroutine(WaitSpawner());
        StartCoroutine(IncreaseDiff());
        resources.resource_pearls = CrabCount;
    }

    private IEnumerator IncreaseDiff(){
        yield return new WaitForSeconds(30);
        diff++;
    }

    private IEnumerator WaitSpawner(){
        if (CrabCount>0 && diff == 1){
            yield return new WaitForSeconds(4);
            cnt++;
            CrabSpawn(0);
            CrabCount--;
            yield return new WaitForSeconds(1.5f);
            cnt++;
            CrabSpawn(1);
            CrabCount--;
            StartCoroutine(WaitSpawner());
        } else if (CrabCount>0 && diff == 2){
            yield return new WaitForSeconds(4);
            cnt++;
            CrabSpawn(0);
            CrabCount--;
            yield return new WaitForSeconds(1);
            cnt++;
            CrabSpawn(1);
            CrabCount--;
            yield return new WaitForSeconds(1);
            cnt++;
            CrabSpawn(1);
            CrabCount--;
            yield return new WaitForSeconds(1.5f);
            cnt++;
            CrabSpawn(2);
            CrabCount--;
            StartCoroutine(WaitSpawner());
        }
    }

    void Update()
    {
        for(int i=1;i<cnt + 1;i++){
            if (isMoving[i]){
                MoveCrab(i);
            }
            else if (isMovingBack[i]){
                MoveBackCrab(i);
            }
        }
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out RaycastHit hit) && hit.collider.gameObject.tag == "Crab"){
                StartCoroutine(CrabDeath(hit.collider.gameObject));
                resources.resource_planks++;
                for (int i=0;i<=cnt;i++){
                    if (newcrab[i]==hit.collider.gameObject){
                        isMoving[i] = false;
                        isMovingBack[i] = false;
                    }
                }
            }
        }
        
    }

    private IEnumerator CrabDeath(GameObject crab){
        animator.Play("CrabDeath");
        yield return new WaitForSeconds(1);
        Destroy(crab);
    }
    

    private void CrabSpawn( int i){
        var random = new System.Random();
        switch(random.Next(1,4 - i) + i){
        case 1:
        SpawnPoint[cnt] = positions[0];
        break;
        case 2:
        SpawnPoint[cnt] = positions[1];
        break;
        case 3:
        SpawnPoint[cnt] = positions[2];
        break;
        case 4:
        SpawnPoint[cnt] = positions[3];
        break;
        }
        switch(random.Next(1,4 - i) + i){
        case 1:
        TargetPoint[cnt] = positions[4];
        break;
        case 2:
        TargetPoint[cnt] = positions[5];
        break;
        case 3:
        TargetPoint[cnt] = positions[6];
        break;
        case 4:
        TargetPoint[cnt] = positions[7];
        break;
        }
        newcrab[cnt] = Instantiate(crab, SpawnPoint[cnt], Quaternion.Euler(0,0,0)) as GameObject;
        isMoving[cnt] = true;
    }

    private void MoveCrab( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[i].transform.position, TargetPoint[i], 4f*Time.deltaTime);
        if(newcrab[i].transform.position == TargetPoint[i]){
            isMoving[i] = false;
            isMovingBack[i]=true;
            animator.Play("CrabThief");
        }
    }

    private void MoveBackCrab( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[i].transform.position, SpawnPoint[i], 4f*Time.deltaTime);
        if(newcrab[i].transform.position == SpawnPoint[i]){
            isMovingBack[i] = false;
            resources.resource_fishs++;
            Destroy(newcrab[i]);
        }
    }

}
