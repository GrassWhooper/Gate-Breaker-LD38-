using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : MonoBehaviour
{
    public delegate void EndGameDelegate();
    public event EndGameDelegate EndGameEvent;

    [SerializeField] float fadingOutTime = 0.5f;
    [SerializeField] float fadingInTime = 0.250f;
    [SerializeField] float timeInTotalBlack = 0.05f;

    Transform toGoTo;
    MeshRenderer hidingScreen;
    bool teleporting = false;
    bool fadingOut = false;
    Coroutine teleportingRotine;
    float alphaCounter;
    float timeCounter;
    Transform player;

    public void GameOver()
    {
        if (teleportingRotine!=null)
        {
            StopCoroutine(teleportingRotine);
            teleportingRotine = null;
        }
        teleporting = true;
        fadingOut = true;
        teleportingRotine = StartCoroutine(Fade(fadingOut,EndGame));
    }

    void EndGame()
    {
        Time.timeScale = 0;
        if (EndGameEvent!=null)
        {
            EndGameEvent();
        }
    }

    public void SendPlayerTo(Transform toGoTo)
    {
        if (teleportingRotine!=null)
        {
            StopCoroutine(teleportingRotine);
            teleportingRotine = null;
        }
        this.toGoTo = toGoTo;
        teleporting = true;
        fadingOut = true;
        teleportingRotine = StartCoroutine(Fade(fadingOut,FadeIn));
    }
    void FadeIn()
    {
        if (teleportingRotine != null)
        {
            StopCoroutine(teleportingRotine);
            teleportingRotine = null;
        }
        fadingOut = false;
        teleportingRotine = StartCoroutine(Transition());
    }
	// Use this for initialization
	void Awake()
    {
        player = FindObjectOfType<PlayerInputManager>().transform;
        hidingScreen = GameObject.FindGameObjectWithTag(Constants.HidingScreenQuad_KEY).GetComponent<MeshRenderer>();
	}
    IEnumerator Transition()
    {
        yield return 0;
        player.position = Camera.main.transform.position = toGoTo.position;
        yield return new WaitForSeconds(timeInTotalBlack);

        teleportingRotine = StartCoroutine(Fade(fadingOut, null));
    }
    IEnumerator Fade(bool fadesOut,Action callBack)
    {
        yield return 0;
        bool fading=true;
        while (fading)
        {
            yield return 0;
            if (fadesOut == true)
            {
                alphaCounter += Time.deltaTime / fadingOutTime;
            }
            else
            {
                alphaCounter -= Time.deltaTime / fadingInTime;
            }
            Material mat = hidingScreen.material;
            Color c = mat.color;
            c.a = alphaCounter;
            mat.color = c;
            if ((fadesOut&&alphaCounter>=1)
                ||(fadesOut == false && alphaCounter < 0.1f))
            {
                fading = false;
                yield return new WaitForSeconds(timeInTotalBlack);
            }
        }
        if (callBack!=null)
        {
            callBack();
        }
    }
    void StartFading()
    {

    }
}