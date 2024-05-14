using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class resources : MonoBehaviour
{
    //���������� ����������
    public static int resource_fishs;
    public static int resource_planks;
    public static int resource_pearls;
    public static int resource_time;
    public static int Raund = 1;
    public Text text_planks;
    public Text text_fishs;
    public Text text_pearls;
    public Text text_time;
    private int checkRound = 0;
    // Start is called before the first frame update
    void Start()
    {
        resource_fishs = 0;
        resource_planks = 0;
        resource_pearls = 0;
        resource_time = 100;
        text_planks.text = "" + resource_planks;
        text_fishs.text = "" + resource_fishs;
        text_pearls.text = "" + resource_fishs;
        text_time.text = "" + resource_time;
    }

    // Update is called once per frame
    void Update()
    {
        text_planks.text = "" + resource_planks;
        text_fishs.text = "" + resource_fishs;
        text_pearls.text = "" + resource_pearls;
        text_time.text = "" + resource_time;

        if (checkRound!=Raund){
        switch(Raund)
        {
            case 1:
                resource_time+=4;
                checkRound=Raund;
            break;
            case 2:
                resource_time+=8;
                checkRound=Raund;
            break;
            case 3:
                resource_time+=12;
                checkRound=Raund;
            break;
            case 4:
                resource_time+=16;
                checkRound=Raund;
            break;
        }
        }
    }
}
