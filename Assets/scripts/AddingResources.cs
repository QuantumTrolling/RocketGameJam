using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddingResources : MonoBehaviour
{
    public int add_fish = 0;
    public int add_plank = 0;
    public int add_shell = 0;
    public int add_action = 0;

    private int checkRound;

    public void Start(){
        checkRound = resources.Raund;
    }

    public void Update(){
        if(checkRound!=resources.Raund){
            Adding();
            checkRound=resources.Raund;
        }
    }

    private void Adding(){
        resources.resource_fishs+=add_fish;
        resources.resource_pearls+=add_shell;
        resources.resource_planks+=add_plank;
        resources.resource_time+=add_action;
    }
}
