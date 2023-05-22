using System;
using System.Collections.Generic;
using System.Linq;
using GameManagementScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Documents
{
    public class DocumentsButtonsManager : MonoBehaviour
    {
        public Text documentText; 
        public Button[] appButtons;
        private List<string> _machinesDocs;

        public void Start()
        {
            var textTyperScript = gameObject.AddComponent<TextTyperScript>();
            
            _machinesDocs = new List<string>
            {
                textTyperScript.ReadTextFile("Documents/CaesarDocs.txt"),
                textTyperScript.ReadTextFile("Documents/VigenereDocs.txt"),
                "Вы ещё не начали ни одного заказа?"
            };
            
            for (var i = 0; i < appButtons.Length; i++)
            {
                var index = i;
                appButtons[i].onClick.AddListener(() => { ShowCanvas(index); });
            }
        }

        public void ChangeCurrentOrderText(string text) => _machinesDocs[2] = text;

        private void ShowCanvas(int indexToShow)
        {
            documentText.text = "";

            documentText.text = _machinesDocs[indexToShow];
        }
    }
}