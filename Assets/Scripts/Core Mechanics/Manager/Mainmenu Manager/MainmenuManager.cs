using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuManager : MonoBehaviour
{
    [Header("Button FX")]
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject quitButton;
    [SerializeField] float transitionDuration;

    [Header("Transition Effect")]
    [SerializeField] Image fadeIn;
    [SerializeField] GameObject fadeInGameObject;
    [SerializeField] float fadeInDuration;

    [Header("UI SFX")]
    [SerializeField] AudioClip uiClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        AudioManager.instance.PlaySound(uiClick);
        LeanTween.scale(startButton, new Vector3(3.6986f, 3.6986f, 3.6986f), transitionDuration).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            fadeInGameObject.SetActive(true);
            LeanTween.scale(startButton, new Vector3(3.5986f, 3.5986f, 3.5986f), transitionDuration).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => {
                LeanTween.alpha(fadeIn.rectTransform, 1, fadeInDuration).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Playground");
                });
            });
        });
        
    }

    public void Quit()
    {
        AudioManager.instance.PlaySound(uiClick);
        LeanTween.scale(quitButton, new Vector3(3.6986f, 3.6986f, 3.6986f), transitionDuration).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            fadeInGameObject.SetActive(true);
            LeanTween.scale(quitButton, new Vector3(3.5986f, 3.5986f, 3.5986f), transitionDuration).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => {
                LeanTween.alpha(fadeIn.rectTransform, 1, fadeInDuration).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                {
                    Application.Quit();
                });
            });
        });
        
    }
}
