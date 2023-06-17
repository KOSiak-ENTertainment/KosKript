using UnityEngine;
using UnityEngine.UI;

namespace Orders.RSA
{
    public class CharacterSolverScript : MonoBehaviour
    {
        private InputField inputField1;
        private InputField inputField2;
        private Button button;

        private bool isInputValid1;
        private bool isInputValid2;

        private void Start()
        {
            inputField1 = GameObject.Find("PCharacterInputField").GetComponent<InputField>();
            inputField2 = GameObject.Find("QCharacterInputField").GetComponent<InputField>();
            button = GameObject.Find("GenButton").GetComponent<Button>();

            // Установка обработчиков событий для полей ввода
            inputField1.onValueChanged.AddListener(OnInputField1ValueChanged);
            inputField2.onValueChanged.AddListener(OnInputField2ValueChanged);

            // Установка обработчика события для кнопки
            button.onClick.AddListener(OnButtonClick);

            // Деактивация кнопки в начале игры
            button.interactable = false;
        }

        private void OnInputField1ValueChanged(string value)
        {
            isInputValid1 = ValidateInput(value);
            UpdateButtonInteractivity();
        }

        private void OnInputField2ValueChanged(string value)
        {
            isInputValid2 = ValidateInput(value);
            UpdateButtonInteractivity();
        }

        private bool ValidateInput(string value)
        {
            string[] numbers = value.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (numbers.Length != 4)
                return false;

            foreach (string number in numbers)
            {
                if (!int.TryParse(number, out int parsedNumber) || !IsPrime(parsedNumber))
                    return false;
            }

            return true;
        }

        private bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i <= Mathf.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private void UpdateButtonInteractivity()
        {
            button.interactable = isInputValid1 && isInputValid2;
        }

        private void OnButtonClick()
        {
            inputField1.text = "1 2";
            inputField2.text = "1 2";
        }
    }
}