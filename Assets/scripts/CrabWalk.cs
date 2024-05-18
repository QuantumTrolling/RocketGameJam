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
    private Vector3 SpawnPoint;
    private bool Spawn;
    private bool[] isMovingBack = new bool[100];
    public Vector3[] positions;
    public int CrabCount;
    private Animator animator;

    void Start(){
        animator = crab.GetComponent<Animator>();
        StartCoroutine(WaitSpawner());
    }

    private IEnumerator WaitSpawner(){
        if (CrabCount>0){
            yield return new WaitForSeconds(4);
            cnt++;
            CrabSpawn();
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
                isMoving[cnt] = false;
                isMovingBack[cnt] = false;
            }
        }
        
    }

    private IEnumerator CrabDeath(GameObject crab){
        animator.Play("CrabDeath");
        yield return new WaitForSeconds(1);
        Destroy(crab);
    }
    

    private void CrabSpawn(){
        var random = new System.Random();
        switch(random.Next(1,4)){
        case 1:
        SpawnPoint = positions[0];
        break;
        case 2:
        SpawnPoint = positions[2];
        break;
        case 3:
        SpawnPoint = positions[3];
        break;
        }
        newcrab[cnt] = Instantiate(crab, SpawnPoint, Quaternion.Euler(0,0,0)) as GameObject;
        isMoving[cnt] = true;
    }

    private void MoveCrab( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[cnt].transform.position, positions[1], 4f*Time.deltaTime);
        if(newcrab[i].transform.position == positions[1]){
            isMoving[i] = false;
            isMovingBack[i]=true;
            animator.Play("CrabThief");
        }
    }

    private void MoveBackCrab( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[cnt].transform.position, SpawnPoint, 4f*Time.deltaTime);
        if(newcrab[i].transform.position == SpawnPoint){
            isMovingBack[i] = false;
            Destroy(newcrab[i]);
        }
    }

}
