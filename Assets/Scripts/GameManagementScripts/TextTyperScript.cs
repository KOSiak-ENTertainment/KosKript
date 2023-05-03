using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagementScripts
{
    public class TextTyperScript : MonoBehaviour
    {
        /*private Text _orderTextUI;
        private Text _customerNameUI;
        private string _fileName;
        private string[] _paragraphs;
        private int _currentParagraphIndex;

        public void Initialize(Text orderTextUI, Text customerNameUI, string fileName)
        {
            _orderTextUI = orderTextUI;
            _customerNameUI = customerNameUI;
            _fileName = fileName;
        }

        public void WriteTextFromFile()
        {
            var path = Path.Combine(Application.dataPath, _fileName);

            if (File.Exists(path))
            {
                _paragraphs = File.ReadAllText(path)
                    .Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

                _customerNameUI.text = _paragraphs[0];

                _orderTextUI.text = _paragraphs[1];
                _currentParagraphIndex = 1;
            }
            else
            {
                Debug.LogError("File not found: " + path);
            }
        }
        
        public void NextParagraph()
        {
            if (_currentParagraphIndex >= _paragraphs.Length - 1) 
                return;
            
            _currentParagraphIndex++;
            _orderTextUI.text = _paragraphs[_currentParagraphIndex] + "\n\n";
        }
        */

        public string[] GetParagraphsFromFile(string fileName)
        {
            var path = Path.Combine(Application.dataPath, fileName);

            return File.ReadAllText(path)
                .Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void WriteParagraph(Text textUI, string paragraph)
        {
            textUI.text = paragraph;
        }
    }
}