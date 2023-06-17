using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Orders.RSA
{
    public class EulerSolverScript : MonoBehaviour
    {
        public GameObject taskSolver;
        public List<InputField> inputFields; // Ссылки на ваши InputField'ы
        public List<string> correctValues; // Правильные значения
        private List<bool> inputFieldCorrect;
        private bool successDisplayed; // Флаг, указывающий, был ли уже выведен успешный результат

        private void Start()
        {
            for (int i = 0; i < inputFields.Count; i++)
            {
                int index = i; // Захватываем значение i в локальную переменную, чтобы использовать внутри лямбда-выражения

                inputFields[i].onValueChanged.AddListener(text =>
                {
                    CheckInputFields();
                });
            }
        }

        private void CheckInputFields()
        {
            if (successDisplayed)
            {
                return; // Если успешный результат уже был выведен, прекращаем проверку
            }

            bool allCorrect = true;

            for (int i = 0; i < inputFields.Count; i++)
            {
                if (inputFields[i].text != correctValues[i])
                {
                    allCorrect = false;
                    break;
                }
            }

            if (allCorrect)
            {
                Debug.Log("Success");
                taskSolver.SetActive(true);
                successDisplayed = true; // Устанавливаем флаг успешного вывода
            }
        }
    }
}