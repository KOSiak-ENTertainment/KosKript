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
            var path = Path.Combine(Application.streamingAssetsPath, filePath);
            var fileText = File.ReadAllText(path);
            var paragraphs = fileText.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
    
            return paragraphs;
        }
        
        public string ReadTextFile(string filePath)
        {
            var path = Path.Combine(Application.streamingAssetsPath, filePath);
            if (File.Exists(path)) 
                return File.ReadAllText(path);

            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }
}