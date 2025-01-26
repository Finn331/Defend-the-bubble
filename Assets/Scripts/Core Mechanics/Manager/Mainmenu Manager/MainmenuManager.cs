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

    [Header("Animation Header Settings")]
    [SerializeField] GameObject imageHeader;
    [SerializeField] private float scaleDuration = 1f; // Durasi animasi
    [SerializeField] private Vector3 startScale = new Vector3(1f, 1f, 1f); // Skala awal
    [SerializeField] private Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f); // Skala target (lebih besar)

    // Start is called before the first frame update
    void Start()
    {
        PlayScaleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayScaleAnimation()
    {
        // Animasi scale membesar
        LeanTween.scale(imageHeader, targetScale, scaleDuration)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(() =>
            {
                // Animasi scale mengecil setelah selesai membesar
                LeanTween.scale(imageHeader, startScale, scaleDuration)
                    .setEase(LeanTweenType.easeInOutSine)
                    .setOnComplete(PlayScaleAnimation); // Loop animasi
            });
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

        SaveManager.instance.health = 100;
        SaveManager.instance.money = 0;
        SaveManager.instance.attackSpeedClick = 2;
        SaveManager.instance.attackDamage = 5;
        SaveManager.instance.regenRate = 0.1f;
        SaveManager.instance.autoFireRate = 0.5f;
        SaveManager.instance.Save();

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
