using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resources : MonoBehaviour
{
    //���������� ����������
    public static int resource_fishs;
    public static int resource_planks;
    public static int resource_pearls;
    public static int resource_time;
    public Text text_planks;
    public Text text_fishs;
    public Text text_pearls;
    public Text text_time;
    // Start is called before the first frame update
    void Start()
    {
        resource_fishs = 0;
        resource_planks = 0;
        resource_pearls = 0;
        resource_time = 4;
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
        text_pearls.text = "" + resource_fishs;
        text_time.text = "" + resource_time;
    }
}
