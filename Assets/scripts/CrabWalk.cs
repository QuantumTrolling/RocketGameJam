using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CrabWalk : MonoBehaviour
{
    public GameObject crab;
    private GameObject[] newcrab = new GameObject[100];
    private int cnt = 0;
    private bool[] isMoving = new bool[100];
    public Vector3[] positions;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            cnt++;
            CrabSpawn();
        }
        for(int i=1;i<cnt + 1;i++){
            if (isMoving[i]){
                MoveCrab(i);
            }
        }
        
    }

    private void CrabSpawn(){
        newcrab[cnt] = Instantiate(crab, positions[0], Quaternion.Euler(0,0,0)) as GameObject;
        isMoving[cnt] = true;
    }

    private void MoveCrab( int i){
        newcrab[i].transform.position = Vector3.MoveTowards(newcrab[cnt].transform.position, positions[1], 4f*Time.deltaTime);
        if(newcrab[i].transform.position == positions[1]){
            isMoving[i] = false;
            Destroy(newcrab[i]);
        }
    }
}
