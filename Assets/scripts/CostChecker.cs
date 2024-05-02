using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostChecker : MonoBehaviour
{
    public int fish_cost;
    public int plank_cost;
    public int shell_cost;

    public Button button;
    public resources resources;

    void Update(){

        if (resources.resource_fishs >= fish_cost && resources.resource_planks >= plank_cost && resources.resource_pearls >= shell_cost){
            button.gameObject.SetActive(true);
        }
        else{
            button.gameObject.SetActive(false);
        }
    }

}
