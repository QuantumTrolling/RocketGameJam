using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (PauseGame){
                Resume();
            }
            else{
                Freez();
            }
        }
    }

    public void Resume(){
        pauseGameMenu.SetActive(false);
        Time.timeScale= 1f;
        PauseGame = false;
    }
    public void Freez(){
        pauseGameMenu.SetActive(true);
        Time.timeScale= 0f;
        PauseGame = true;
    }
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}

