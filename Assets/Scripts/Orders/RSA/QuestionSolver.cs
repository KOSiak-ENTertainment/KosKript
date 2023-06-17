using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Orders.RSA
{
    public class QuestionSolver : MonoBehaviour
    {
        public Text questionText;
        public InputField answerInput;
        public GameObject objectToActivate;

        private Dictionary<string, int> questions;

        private string currentQuestion;
        private int currentAnswer;
        
        private bool _waitingForInput = true;

        private void Start()
        {
            questions = new Dictionary<string, int>
            {
                {
                    "Задача про грузовики: У вас есть грузовик, который может перевозить 10 ящиков. Вам нужно перевезти 100 ящиков из одного города в другой. Какое наименьшее количество грузовиков вам потребуется для этого?",
                    10
                },
                {
                    "Задача про фрукты: У вас есть 4 яблока и 5 апельсинов. Сколько всего фруктов у вас есть, если сложить яблоки и апельсины вместе?",
                    9
                },
                {
                    "Задача про числовой ряд: В каком порядке следующие числа должны идти в ряду: 2, 4, 6, 8, 10, __,",
                    12
                },
                {
                    "Задача про перекресток: На перекрестке стоят три машины. Они должны пересечь перекресток, но могут двигаться только прямо или направо. Каждая машина может пересечь перекресток за 1 минуту. Сколько минут потребуется, чтобы все машины пересекли перекресток?",
                    3
                },
                {
                    "Задача про раскраску забора: Забор состоит из 10 досок, которые нужно раскрасить в красный или зеленый цвет. Сколько различных вариантов раскраски забора возможно, если каждую доску можно покрасить только в один цвет?",
                    1024
                },
                {
                    "Задача про ключи: У вас есть три ключа и три замка. Каждый ключ подходит только к одному замку. Каким минимальным количеством попыток вы сможете открыть все замки?",
                    3
                },
                { "Задача про цифры: Какая цифра пропущена в последовательности: 1, 2, 4, 7, 11, __?", 16 },
                { "Задача про логический ряд: Закончите логический ряд: 3, 6, 9, 12, __?", 15 }
            };
            ShowRandomQuestion();
        }

        private void ShowRandomQuestion()
        {
            // Выбираем случайный вопрос из словаря
            int randomIndex = Random.Range(0, questions.Count);
            currentQuestion = questions.Keys.ElementAt(randomIndex);
            currentAnswer = questions.Values.ElementAt(randomIndex);

            // Выводим вопрос на Text UI
            questionText.text = currentQuestion;

            // Очищаем поле ввода ответа
            answerInput.text = string.Empty;
        }
        
        private void Update()
        {
            if (_waitingForInput && Input.GetKeyDown(KeyCode.Return))
            {
                CheckAnswer();
            }
        }

        public void CheckAnswer()
        {
            // Получаем ответ, введенный игроком
            string inputText = answerInput.text;

            // Пытаемся преобразовать введенный текст в число
            bool isNumeric = int.TryParse(inputText, out int inputAnswer);

            if (isNumeric && inputAnswer == currentAnswer)
            {
                // Если ответ верный, активируем объект
                objectToActivate.SetActive(true);
                Debug.Log("Правильный ответ!");
            }
            else
            {
                Debug.Log("Неправильный ответ!");
            }

            // Показываем новый случайный вопрос
            ShowRandomQuestion();
        }
    }
}
