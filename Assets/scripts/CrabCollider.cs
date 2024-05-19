using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabCollider : MonoBehaviour
{
    public bool isMoving;
    public bool isMovingBack;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Structure") ){
            Debug.Log("Nah,I'm back");
            isMovingBack=true;
        }
    }
}
