using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagementScripts
{
    public class TextTyperScript : MonoBehaviour
    { 
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