using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class CrabWalk : MonoBehaviour
{
    public GameObject crab;
    public GameObject win;
    public GameObject lose;
    public TileMapHolder tileMap;
    public Pause pause;
    private GameObject[] newcrab = new GameObject[100];
    private int cnt = 0;
    private bool[] isMoving = new bool[100];
    private Vector3[] SpawnPoint = new Vector3[100];
    public GameObject Things;
    private bool Spawn;
    private bool[] isMovingBack = new bool[100];
    private Vector3[] TargetPoint = new Vector3[100];
    public Vector3[] positions;
    public int CrabCount;
    private int diff = 1;
    private float speed;
    private int sink = 3;
    private Animator[] animator = new Animator[100];

    void Start(){
        StartCoroutine(WaitSpawner());
        StartCoroutine(IncreaseDiff());
        StartCoroutine(Sinking());
        resources.resource_pearls = CrabCount;
        Instantiate(Things, positions[5], Quaternion.Euler(0,0,0));

    }

    private IEnumerator IncreaseDiff(){
        yield return new WaitForSeconds(25);
        diff++;
    }

    private IEnumerator Sinking(){
        if(sink>0){
        yield return new WaitForSeconds(15);
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
        if (CrabCount>0 && diff == 1){
            speed = 4f;
            yield return new WaitForSeconds(4);
            cnt++;
            CrabSpawn(0);
            CrabCount--;
            yield return new WaitForSeconds(1.5f);
            cnt++;
            CrabSpawn(1);
            CrabCount--;
            yield return new WaitForSeconds(1.5f);
            cnt++;
            CrabSpawn(0);
            CrabCount--;
            StartCoroutine(WaitSpawner());
        } else if (CrabCount>0 && diff == 2){
            speed = 5f;
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
        if(Input.GetMouseButtonDown(0) && !pause.PauseGame){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out RaycastHit hit) && hit.collider.gameObject.tag == "Crab"){
                StartCoroutine(CrabDeath(hit.collider.gameObject));
                for (int i=0;i<=cnt;i++){
                    if (newcrab[i]==hit.collider.gameObject){
                        isMoving[i] = false;
                        isMovingBack[i] = false;
                    }
                }
            }
        }
        if(CrabCount == 0){
            if (resources.resource_planks >= resources.resource_pearls/2){
                StartCoroutine(NextLevel());
            }
            else{
                StartCoroutine(RePlay());
            }
        }
        
    }

    private IEnumerator NextLevel(){
        win.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scene2");
    }

    private IEnumerator RePlay(){
        lose.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scene1");
    }

    private IEnumerator CrabDeath(GameObject crab){
        resources.resource_planks++;
        crab.GetComponent<Animator>().Play("CrabDeath");
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
        newcrab[cnt] = Instantiate(crab, SpawnPoint[cnt], Quaternion.Euler(0,0,0)) as GameObject;
        animator[cnt]=newcrab[cnt].GetComponent<Animator>();
        isMoving[cnt] = true;
    }

    private void MoveCrab( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[i].transform.position, positions[5], speed*Time.deltaTime);
        if(newcrab[i].transform.position == positions[5]){
            isMoving[i] = false;
            isMovingBack[i]=true;
            animator[i].Play("crabWalk");
        }
    }

    private void MoveBackCrab( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[i].transform.position, SpawnPoint[i], speed*Time.deltaTime);
        if(newcrab[i].transform.position == SpawnPoint[i]){
            isMovingBack[i] = false;
            resources.resource_fishs++;
            animator[i].Play("CrabThiefWalk");
            Destroy(newcrab[i]);
        }
    }

}
