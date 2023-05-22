using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagementScripts
{
    public class TextTyperScript : MonoBehaviour
    { 
        public string[] GetTextParagraphs(string filePath)
        {
            string[] paragraphs = {};
            var path = Path.Combine(Application.dataPath, filePath);

            if (File.Exists(path))
            {
                paragraphs = File.ReadAllText(path)
                    .Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            return paragraphs;
        }
        
        public string ReadTextFile(string filePath)
        {
            var path = Path.Combine(Application.dataPath, filePath);
            if (File.Exists(path)) 
                return File.ReadAllText(path);

            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }
}