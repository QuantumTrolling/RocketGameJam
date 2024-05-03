using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostChecker : MonoBehaviour
{
    public int fish_cost;
    public int plank_cost;
    public int shell_cost;
    public int action_cost;

    public Button button;
    public resources resources;


    void Update(){

        if (resources.resource_fishs >= fish_cost && resources.resource_planks >= plank_cost && resources.resource_pearls >= shell_cost && resources.resource_time >= action_cost){
            button.enabled = true;
        }
        else{
            button.enabled = false;
        }
    }

    public void CostDebiting(){
        resources.resource_fishs -= fish_cost;
        resources.resource_planks -= plank_cost;
        resources.resource_pearls -= shell_cost;
        resources.resource_time -= action_cost;
    }

}
