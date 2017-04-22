using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject passWordEntry;
    public InputField pwInputField;
    public GameObject endGameTab;
    Gate gate;
    LevelManager levelManager = null;

    public void DisableUIElement(GameObject uiElement)
    {
        uiElement.SetActive(false);
        Time.timeScale = 1;
    }
    public void DisplayUIElement(GameObject uiElement)
    {
        uiElement.SetActive(true);
        Time.timeScale = 0;
    }
    public void Quit()
    {
        Application.Quit();
        Debug.LogWarning("Quitting Game");
    }
    // Use this for initialization
    void Start()
    {
        gate = FindObjectOfType<Gate>();
        gate.EnteredGateEvent += Gate_EnteredGateEvent;
        gate.LeftGateEvent += Gate_LeftGateEvent;
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.EndGameEvent += LevelManager_EndGameEvent;
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void LevelManager_EndGameEvent()
    {
        endGameTab.SetActive(true);
    }

    public void CheckAnswer()
    {
        string s = pwInputField.text;
        bool correctAnswer = false;
        foreach (string s0 in Constants.PossibleAnswers)
        {
            if (s0==s)
            {
                correctAnswer = true;
                break;
            }
        }
        gate.GotAnswer = correctAnswer;
        if (gate.GotAnswer==true)
        {
            passWordEntry.SetActive(false);
        }
    }
    private void Gate_LeftGateEvent()
    {
        passWordEntry.SetActive(false);
    }
    private void Gate_EnteredGateEvent()
    {
        passWordEntry.SetActive(true);
    }
}