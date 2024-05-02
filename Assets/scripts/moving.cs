using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class moving : MonoBehaviour
{
    private bool ismoving = false;
    private bool CanMove = true;
    private Vector3 targetPosition;
    public float speed = 5f;

    public TileMapHolder grid;
    
    void Start()
    {
        
    }

    public void StopMoving(){
        bool check = CanMove;
        CanMove = !check;
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
        if (CanMove){
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if(grid.IsAreaBounded(((int)targetPosition.x), ((int)targetPosition.y), new Vector2Int(8,8)) && grid.IsPlaceTaken(((int)targetPosition.x), ((int)targetPosition.y), new Vector2Int(8,8)))
        {
            Debug.Log("Yep");
        }

        ismoving = true;
        
        }

    }

    private void Move(){
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            ismoving = false;
        }
    }
}
