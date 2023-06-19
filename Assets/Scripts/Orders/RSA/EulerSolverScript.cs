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
        public bool successDisplayed; // Флаг, указывающий, был ли уже выведен успешный результат
        public bool isItNeedNewQuestion = false;

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
                return; // If success has already been displayed, stop the validation
            }

            bool allCorrect = true;

            for (int i = 0; i < inputFields.Count; i++)
            {
                string userInput = inputFields[i].text.Trim(); // Remove leading and trailing spaces from user input
                string correctValue = correctValues[i].Trim(); // Remove leading and trailing spaces from the correct value

                if (userInput != correctValue)
                {
                    allCorrect = false;
                    break;
                }
            }

            if (allCorrect)
            {
                Debug.Log("Success");
                taskSolver.SetActive(true);
                if (isItNeedNewQuestion)
                {
                    taskSolver.GetComponent<QuestionSolver>().ShowRandomQuestion();
                }

                isItNeedNewQuestion = true;
                successDisplayed = true; // Set the success display flag
            }
        }
    }
}