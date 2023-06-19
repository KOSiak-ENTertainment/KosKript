using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class NumberInput : MonoBehaviour
{
    public InputField inputField;
    public List<int> numbersList;
    public GameObject objToAct;
    public bool isInputChecked;
    public List<GameObject> objForDeact;

    private void Update()
    {
        if (!isInputChecked)
        {
            // Получение ввода игрока и удаление лишних пробелов
            string userInput = inputField.text.Trim();
            userInput = string.Join(" ", userInput.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            // Разделение введенных чисел на список
            string[] inputNumbers = userInput.Split(' ');
            List<int> userNumbers = new List<int>();

            foreach (string number in inputNumbers)
            {
                int parsedNumber;
                if (int.TryParse(number, out parsedNumber))
                {
                    userNumbers.Add(parsedNumber);
                }
                else
                {
                    Debug.Log("Ошибка: Введите только числа.");
                    return;
                }
            }

            // Проверка количества чисел
            if (userNumbers.Count != 8)
            {
                Debug.Log("Неверное количество чисел. Попробуйте еще раз.");
                return;
            }

            // Проверка чисел
            bool allNumbersCorrect = true;
            for (int i = 0; i < 8; i++)
            {
                if (userNumbers[i] != numbersList[i])
                {
                    allNumbersCorrect = false;
                    break;
                }
            }

            if (allNumbersCorrect)
            {
                Debug.Log("Все числа верны!");
                objToAct.SetActive(true);
                objToAct.GetComponent<SubmitOrderButtonScript>().objectsForDeactivate = objForDeact;
                isInputChecked = true; // Устанавливаем флаг, чтобы проверка была выполнена только один раз
            }
            else
            {
                Debug.Log("Одно или несколько чисел неверны. Попробуйте еще раз.");
            }
        }
    }
}