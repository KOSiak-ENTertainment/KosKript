using UnityEngine;
using UnityEngine.UI;

namespace TaskbarScripts
{
    public class TaskbarManagerScript : MonoBehaviour
    {
        public GameObject[] canvasApps; // массив с объектами Canvas для каждого приложения
        public Button[] appButtons; // массив с кнопками для каждого приложения

        private void Start()
        {
            // Настройка привязки методов запуска и скрытия приложений к кнопкам
            for (int i = 0; i < appButtons.Length; i++)
            {
                int index = i;
                appButtons[i].onClick.AddListener(() => { ShowCanvas(index); });
            }
        }

        private void ShowCanvas(int indexToShow)
        {
            // Скрытие всех Canvas
            foreach (GameObject canvas in canvasApps)
            {
                canvas.SetActive(false);
            }

            // Отображение нужного Canvas
            canvasApps[indexToShow].SetActive(true);
        }
    }
}