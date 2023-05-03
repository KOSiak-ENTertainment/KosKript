using UnityEngine;
using UnityEngine.UI;
using MachinesScripts;

namespace GameManagementScripts
{
    public class GameManagerScript : MonoBehaviour
    {
        public GameObject bugSolver;
        public Text textUI;
        public Text customerNameUI;
        public Text encryptedText;
        public InputField inputField;
        public Text warningText;

        private TextTyperScript _textTyper;
        private string _rightEncryptedText;

        private void Start()
        {
            InitTextTyper();
            GameLogic();
        }

        private void InitTextTyper()
        {
            var textTyperObject = new GameObject("TextTyper");
            _textTyper = textTyperObject.AddComponent<TextTyperScript>();
        }

        private void GameLogic()
        {
            var paragraphs = _textTyper.GetParagraphsFromFile("Dialogs/ArchieRochesterDialog.txt");
            customerNameUI.text = paragraphs[0];
            textUI.text = paragraphs[1]; //TODO сделать посимвольный вывод
            var text = _textTyper.GetParagraphsFromFile("Orders/ArchieRochesterOrder.txt");
            var cesMash = new CaesarMachine(text[0]);
            var encodedFile = cesMash.UncodedFile;
            var bag1 = new SymbolAutoRegistrationError(cesMash);
            encryptedText.text = encodedFile.Substring(0, bag1.IntervalBegin);
            bugSolver.SetActive(true);
            warningText.text = bag1.UnencryptedPieceText + " " + bag1.EncryptedPieceText;
            _rightEncryptedText = bag1.EncryptedPieceText;
            inputField.text = bag1.EncryptedPieceText;
            // Добавляем обработчик события EndEdit для InputField
            inputField.onEndEdit.AddListener(delegate { OnInputEndEdit(encodedFile, bag1); });
        }

        private void OnInputEndEdit(string encodedFile, SymbolAutoRegistrationError bag1)
        {
            // Получаем введенный текст
            string userInput = inputField.text;
            
            if (userInput.Equals(_rightEncryptedText))
            {
                // Добавляем его к зашифрованному тексту
                encryptedText.text += " " + userInput + " " +
                                      encodedFile.Substring(bag1.IntervalBegin + bag1.CountCipherSymbol);
                // Удаляем обработчик события EndEdit, чтобы избежать повторной обработки
                inputField.onEndEdit.RemoveAllListeners();
                
                bugSolver.SetActive(false);
            }
        }
    }
}