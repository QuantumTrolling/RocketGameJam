using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostDebiting : MonoBehaviour
{
    public resources resources;

    public CostChecker cost;
    void Start()
    {
        resources.resource_fishs -= cost.fish_cost;
        resources.resource_planks -= cost.plank_cost;
        resources.resource_pearls -= cost.shell_cost;
        resources.resource_time -= cost.action_cost;
    }

}
