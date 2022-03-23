using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioClip[] soundtrack;
    public Slider volumeSlider;

    private AudioSource audioSource;

    public void BackButton()
    {
        SceneManager.UnloadSceneAsync("UISettings");
    }

    // Use this for initialization
    private void Start()
    {
        audioSource = GameManager.Instance.GetComponent<AudioSource>();

        if (!audioSource.playOnAwake)
        {
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
        }

        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
        }
    }

    private void OnEnable()
    {
        //Register Slider Events
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
    }

    //Called when Slider is moved
    private void changeVolume(float sliderValue)
    {
        audioSource.volume = sliderValue;
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    private void OnDisable()
    {
        //Un-Register Slider Events
        volumeSlider.onValueChanged.RemoveAllListeners();
    }
}
