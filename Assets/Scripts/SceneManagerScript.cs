using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Button button;
    public AudioClip audioClip;
    public Text textUi;
    public GameObject hintText;
    public string text;

    private void Start()
    {
        hintText.SetActive(false);
        audioSource.clip = audioClip;
        textUi.text = text;
        button.interactable = false;
        // Включаем аудиофайл
        audioSource.Play();

        // Вызываем метод ActivateButton после окончания проигрывания аудиофайла
        Invoke("ActivateButton", audioSource.clip.length);
    }

    private void ActivateButton()
    {
        // Включаем кнопку
        button.interactable = true;
        hintText.SetActive(true);
        hintText.transform.Find("HintText").GetComponent<Text>().text =
            "Запустить компьютер вы можете нажав на красную кнопку.";
    }
}
