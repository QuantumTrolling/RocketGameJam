using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CostDebiting : MonoBehaviour
{
    public int fish_cost;
    public int plank_cost;
    public int shell_cost;
    public int action_cost;
    public int Level;
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Scene3"){
        resources.resource_fishs -= fish_cost;
        resources.resource_planks -= plank_cost;
        resources.resource_pearls -= shell_cost;
        resources.resource_time -= action_cost;
        }
    }

}
