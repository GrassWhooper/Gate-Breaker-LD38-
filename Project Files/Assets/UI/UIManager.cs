using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
