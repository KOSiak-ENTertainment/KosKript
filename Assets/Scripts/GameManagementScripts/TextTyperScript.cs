using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagementScripts
{
    public class TextTyperScript : MonoBehaviour
    {
        private Text _textUI;
        private string _fileName;

        public TextTyperScript(Text textUI, string fileName)
        {
            _textUI = textUI;
            _fileName = fileName;
        }
        
        
    }
}