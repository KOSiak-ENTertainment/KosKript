using UnityEngine;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour
{
    public Slider progressBar; // Ссылка на компонент полоски прогресса в Unity Editor
    public Text loadingText; // Ссылка на компонент текста в Unity Editor

    private bool isLoading = false;

    private void Start()
    {
        progressBar.gameObject.SetActive(false); // Скрыть полоску прогресса при старте
    }

    public void OnButtonClick()
    {
        if (!isLoading)
        {
            isLoading = true;
            progressBar.value = 0; // Сбросить значение полоски прогресса в начало
            loadingText.text = "Loading..."; // Установить начальный текст

            StartCoroutine(LoadData()); // Запустить процесс загрузки данных в корутине
        }
    }

    private System.Collections.IEnumerator LoadData()
    {
        // Имитация процесса загрузки данных
        for (float progress = 0; progress <= 1; progress += 0.01f)
        {
            progressBar.value = progress;
            yield return null;
        }

        loadingText.text = "Loading Complete"; // Установить текст по завершении загрузки
        isLoading = false;
        progressBar.gameObject.SetActive(false); // Скрыть полоску прогресса
    }
}
