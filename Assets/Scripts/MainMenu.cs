using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    EventSystem events;
    
    void Start()
    {
        events = GetComponentInChildren<EventSystem>();
    }

    private void Update()
    {
        if(events.currentSelectedGameObject == null)
        {
            events.SetSelectedGameObject(events.firstSelectedGameObject);
        }
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("CreatureScene");
    }

    public void ControlsButtonPressed()
    {
        SceneManager.LoadScene("ControlsScreen");
    }

    public void BackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
