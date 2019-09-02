using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private IEnumerator coroutine;
    private Image fade;

    void Start()
    {
        fade = gameObject.GetComponent<Image>();
        fade.enabled = true;
        FadeOut(); 
    }
    IEnumerator FadeIn(string scene)
    {
        if(fade != null)
        {
            print("Fading In...");
            fade.CrossFadeAlpha(1f, 1f, true);
            yield return new WaitForSecondsRealtime(1);
            SceneManager.LoadScene(scene);
        }
    }
    public void FadeOut()
    {
        if (fade != null)
        {
            print("Fading Out...");
            fade.CrossFadeAlpha(0f, 1f, true);
        }
    }
    public void LoadScene(string scene)
    {
        coroutine = FadeIn(scene);
        StartCoroutine(coroutine);
    }
}
