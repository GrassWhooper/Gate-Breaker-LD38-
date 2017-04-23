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
    public GameObject lostUIEndGame;
    public Slider healthSlider;

    public GameObject[] keyTriggers;
    public GameObject[] keys;

    RawImage[] keysImages;
    Text[] keysTexts;

    int activatedKeyIndex = -1;
    Gate gate;
    LevelManager levelManager = null;
    Health playerHealth;
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
        levelManager.WonEvent += LevelManager_EndGameEvent;
        Time.timeScale = 0;
        GameObject player = GameObject.FindGameObjectWithTag(Constants.PlayerTag_KEY);
        playerHealth= player.GetComponent<Health>();
        if (playerHealth==null)
        {
            playerHealth = player.GetComponentInChildren<Health>();
        }
        if (playerHealth==null)
        {
            playerHealth=  player.transform.root.GetComponentInChildren<Health>();
        }
        playerHealth.OnDeathEvent += OnPlayerDeath_OnDeathEvent;
        levelManager.LostEvent += LevelManager_LostEvent;
        playerHealth.onDamageTakenEvent += H_onDamageTakenEvent;
        healthSlider.maxValue = playerHealth.CurrentHealth;
        healthSlider.minValue = 0;
        healthSlider.value = healthSlider.maxValue;

        if (keys.Length==keyTriggers.Length)
        {
            keysTexts = new Text[keys.Length];
            keysImages = new RawImage[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                keysTexts[i] = keys[i].GetComponentInChildren<Text>();
                keysImages[i] = keys[i].GetComponentInChildren<RawImage>();
                keyTriggers[i].GetComponent<Health>().OnDeathEvent += ActivateNextKeyPart;
                keysImages[i].enabled = false;
            }
        }
    }

    void ActivateNextKeyPart()
    {
        activatedKeyIndex++;
        if (activatedKeyIndex>=keys.Length)
        {
            return;
        }
        keysTexts[activatedKeyIndex].enabled = false;
        keysImages[activatedKeyIndex].enabled = true;
    }

    private void H_onDamageTakenEvent()
    {
        healthSlider.value = playerHealth.CurrentHealth;
    }

    private void LevelManager_LostEvent()
    {
        lostUIEndGame.SetActive(true);
    }

    private void OnPlayerDeath_OnDeathEvent()
    {
        FindObjectOfType<LevelManager>().GameOver(false);
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
            FindObjectOfType<LevelManager>().GameOver(true);
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