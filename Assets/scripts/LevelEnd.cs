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

    public IEnumerator Nextlevel(){
        win.SetActive(true);
        yield return new WaitForSeconds(5);
        if (Level == 2){
        SceneManager.LoadScene("Scene3");
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
