using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Orders.RSA
{
    public class EncryptorScript : MonoBehaviour
    {
        public List<BigInteger> CodSymbol;
        public List<BigInteger> CompleteSymbol;
        public GameObject submitOrderButton;
        public bool isInputEnabled = true;
        public List<GameObject> objForOff;
        public Text nums;
        public InputField inputForNums;
        public Text input;

        public void Start()
        {
            // Добавляем обработчик события для поля ввода текста
            inputForNums.onEndEdit.AddListener(OnInputEndEdit);
        }

        private void OnInputEndEdit(string input)
        {
            if (!isInputEnabled)
                return;

            // Разделяем введенный текст на числа с помощью пробелов
            string[] numbers = input.Trim().Split(' ');

            // Создаем новый список чисел для введенных данных
            List<BigInteger> inputNumbers = new List<BigInteger>();

            // Перебираем числа и добавляем их в список, игнорируя недопустимые значения
            foreach (string number in numbers)
            {
                if (BigInteger.TryParse(number, out BigInteger parsedNumber))
                {
                    inputNumbers.Add(parsedNumber);
                }
            }

            // Вызываем функцию для сравнения списков чисел
            CompareLists(inputNumbers);
        }

        private void CompareLists(List<BigInteger> inputNumbers)
        {
            // Проверяем, если длины списков различаются, то они не равны
            if (inputNumbers.Count != CompleteSymbol.Count)
            {
                Debug.Log("Списки чисел не равны.");
                return;
            }

            // Проверяем каждый элемент списка
            for (int i = 0; i < inputNumbers.Count; i++)
            {
                if (inputNumbers[i] != CompleteSymbol[i])
                {
                    Debug.Log("Списки чисел не равны.");
                    return;
                }
            }

            // Введенные числа совпадают, отключаем возможность ввода
            isInputEnabled = false;
            Debug.Log("Введенные числа совпадают.");
            submitOrderButton.GetComponent<SubmitOrderButtonScript>().objectsForDeactivate = objForOff;
            submitOrderButton.SetActive(true);
        }

        public string GetCodSymbol()
        {
            return CodSymbol.Aggregate("", (current, num) => current + (num + " "));
        }
    }
}