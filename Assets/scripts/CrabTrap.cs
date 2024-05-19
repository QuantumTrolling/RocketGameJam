using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabTrap : MonoBehaviour
{
    public Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private bool hasCrab = false;
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Crab") && !hasCrab){
            Destroy(other.gameObject);
            Debug.Log("Catch yo");
            spriteRenderer.sprite=sprite;
            hasCrab=true;
        }
    }
}
