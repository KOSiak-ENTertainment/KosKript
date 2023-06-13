using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    public AudioSource audioSource;
    public Button button;

    private void Start()
    {
        button.interactable = false; // Начально делаем кнопку неактивной
        PlayAudio();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            button.interactable = true; // После окончания аудиофайла активируем кнопку
        }
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }
}
