using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup Canvas;
    public CircleWipeController circleWipeController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStart()
    {
        StartCoroutine(StartRoutine());
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator StartRoutine()
    {
        float Time_current = 0;

        float FadeTime = 0.5f;
        float WipeTime = 2f;

        while (Time_current <= FadeTime)
        {
            Canvas.alpha = (FadeTime - Time_current / FadeTime);
            Time_current += Time.deltaTime;
            yield return null;
        }
        Time_current = 0;


        circleWipeController.FadeOut();
        while (Time_current <= WipeTime)
        {
            Time_current += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("GameWithTiles");
        yield return null;
    }
}
