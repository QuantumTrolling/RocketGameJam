using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public bool Win = false;

    public int Level;

    public GameObject win;

    public GameObject lose;

    void Start(){
        if (Level == 2){
            StartCoroutine(Checker());
        }
    }

   public IEnumerator Checker(){
        yield return new WaitForSeconds(45);
        if (resources.resource_stars >=70){
            StartCoroutine(Nextlevel());
        }else{
            StartCoroutine(Replay());
        }
        
    }


    public IEnumerator Nextlevel(){
        win.SetActive(true);
        yield return new WaitForSeconds(5);
        if (Level == 2){
        SceneManager.LoadScene("Menu");
        }
        if (Level == 3){
            SceneManager.LoadScene("Menu");
        }
        
    }

    public IEnumerator Replay(){
        lose.SetActive(true);
        yield return new WaitForSeconds(5);
        if (Level == 2){
        SceneManager.LoadScene("Scene2");
        }
        if (Level == 3){
            SceneManager.LoadScene("Scene3");
        }
        
    }
}
