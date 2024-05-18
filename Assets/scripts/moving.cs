using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    private bool ismoving = false;
    private bool CanMove = true;
    public Pause pause;
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
        if(Input.GetMouseButton(0) && !pause.PauseGame){
            SetTargetPosition();
        }
        if(ismoving)
        {
            Move();
        }   
    }
   
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Water")){
            Debug.Log("Water");
            ismoving=false;
        }
    }

    private void SetTargetPosition()
    {
        if (CanMove){
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        bool available = grid.IsAreaBounded(grid.GetGridPosHere(targetPosition).x, grid.GetGridPosHere(targetPosition).y, new Vector2Int(1,1));
        if(!grid.IsPlaceTaken(grid.GetGridPosHere(targetPosition).x, grid.GetGridPosHere(targetPosition).y, new Vector2Int(1,1)) && available){ ismoving = true; }
        
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
