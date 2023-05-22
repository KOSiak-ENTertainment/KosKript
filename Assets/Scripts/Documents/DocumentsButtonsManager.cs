using GameManagementScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Documents
{
    public class DocumentsButtonsManager : MonoBehaviour
    {
        public Text documentText; 
        public Button[] appButtons;
        private string[] _machinesDocs;
        
        private void Start()
        {
            var textTyperScript = gameObject.AddComponent<TextTyperScript>();
            _machinesDocs = new string[3];

            _machinesDocs[0] = textTyperScript.ReadTextFile("Documents/CaesarDocs.txt");
            _machinesDocs[1] = textTyperScript.ReadTextFile("Documents/VigenereDocs.txt");
            
            for (var i = 0; i < appButtons.Length; i++)
            {
                var index = i;
                appButtons[i].onClick.AddListener(() => { ShowCanvas(index); });
            }
        }

        private void ShowCanvas(int indexToShow)
        {
            documentText.text = "";

            documentText.text = _machinesDocs[indexToShow];
        }
    }
}