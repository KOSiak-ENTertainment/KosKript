using GameManagementScripts;
using UnityEngine;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour
{
    public Slider progressBar; // Ссылка на компонент полоски прогресса в Unity Editor
    public Text loadingText; // Ссылка на компонент текста в Unity Editor

    private bool _isLoading;

    public void OnButtonClick()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true); 

        if (gameManager.countOfSolvedOrders == 0)
            gameManager.ordersState = GameManagerScript.OrderLoading.FirstOrderLoaded;
        if (gameManager.countOfSolvedOrders == 1)
            gameManager.ordersState = GameManagerScript.OrderLoading.SecondOrderLoaded;
        if (gameManager.countOfSolvedOrders == 2)
            gameManager.ordersState = GameManagerScript.OrderLoading.ThirdOrderLoaded;
        if (_isLoading) 
            return;
        
        _isLoading = true;
        progressBar.value = 0; // Сбросить значение полоски прогресса в начало
        loadingText.text = "Заказ загружается..."; // Установить начальный текст

        StartCoroutine(LoadData()); // Запустить процесс загрузки данных в корутине
    }

    private System.Collections.IEnumerator LoadData()
    {
        // Имитация процесса загрузки данных
        for (float progress = 0; progress <= 1; progress += 0.01f)
        {
            progressBar.value = progress;
            yield return null;
        }

        loadingText.text = "Заказ загружен!"; // Установить текст по завершении загрузки
        _isLoading = false;
        progressBar.gameObject.SetActive(false); // Скрыть полоску прогресса
    }
}
