using UnityEngine;
using UnityEngine.UI;

namespace Orders.RSA
{
    public class RsaCustomerCheckerScript : MonoBehaviour
    {
        public GameObject firstBugSolver;
        public GameObject secondBugSolver;
        
        private Button _button;
        private Text _textUI;

        private readonly string[] _uniqueTexts = { "Заказчик в базе", "Заказчик не в базе" };

        private void Start()
        {
            _button = GameObject.Find("CheckCustomer").GetComponent<Button>();
            _textUI = GameObject.Find("BaseStatus").GetComponent<Text>();
            
            _button.onClick.AddListener(ActivateRandomObject);
        }

        private void ActivateRandomObject()
        {
            // Генерируем случайное число 0 или 1 для выбора объекта
            var randomIndex = Random.Range(0, 1);

            // Активируем выбранный объект и деактивируем другой
            if (randomIndex == 0)
            {
                firstBugSolver.SetActive(true);
                secondBugSolver.SetActive(false);
            }
            else
            {
                firstBugSolver.SetActive(false);
                secondBugSolver.SetActive(true);
            }

            // Устанавливаем уникальный текст на Text UI
            _textUI.text = _uniqueTexts[randomIndex];
        }
    }
}