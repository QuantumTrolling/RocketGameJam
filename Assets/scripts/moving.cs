using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    private bool ismoving = false;
    private Vector3 targetPosition;
    public float speed = 5f;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetMouseButton(0)){
            SetTargetPosition();
        }
        if(ismoving)
        {
            Move();
        }   
    }
    private void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        ismoving = true;
    }

    private void Move(){
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            ismoving = false;
        }
    }
}
