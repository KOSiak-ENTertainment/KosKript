using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

namespace Orders.RSA
{
    public class GetTaskButtonScript : MonoBehaviour
    {
        public GameObject questionSolver;
        public GameObject encryptor;
        public BigInteger ModN;
        private Text _questionText;
        private InputField _answerInput;
        private Text _resultText;

        private Dictionary<string, int> _dictionary;
        private string _currentQuestion;
        private int _currentAnswer;
        private bool _waitingForInput;

        public void OnClick()
        {
            questionSolver.SetActive(true);
            ShowRandomQuestion();
        }

        private void Start()
        {
            _questionText = questionSolver.transform.Find("Task").GetComponent<Text>();
            _answerInput = questionSolver.transform.Find("InputFieldForTaskAnswer").GetComponent<InputField>();
            _resultText = questionSolver.transform.Find("ModuleN").GetComponent<Text>();

            _dictionary = new Dictionary<string, int>
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
        }

        private void ShowRandomQuestion()
        {
            var randomIndex = Random.Range(0, _dictionary.Count);

            _currentQuestion = _dictionary.Keys.ElementAt(randomIndex);
            _currentAnswer = _dictionary[_currentQuestion];

            _questionText.text = _currentQuestion;

            _answerInput.text = string.Empty;

            _resultText.text = string.Empty;

            _waitingForInput = true;

            gameObject.GetComponent<Button>().interactable = false;
        }

        private void Update()
        {
            if (_waitingForInput && Input.GetKeyDown(KeyCode.Return))
            {
                CheckAnswer();
            }
        }

        private void CheckAnswer()
        {
            if (int.TryParse(_answerInput.text, out var answer) || _answerInput.text.ToLower() == "желтый")
            {
                if (answer == _currentAnswer || _answerInput.text.ToLower() == "желтый")
                {
                    _resultText.text = "N = " + ModN;
                    _waitingForInput = false;
                    encryptor.SetActive(true);
                }
                else
                {
                    _resultText.text = "Неправильно. Попробуйте еще раз.";
                    _waitingForInput = true;
                }
            }
        }
    }
}