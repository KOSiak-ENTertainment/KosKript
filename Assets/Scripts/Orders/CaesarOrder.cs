using System;
using UnityEngine;
using System.IO;
using MachinesScripts;

namespace Orders
{
    public class CaesarOrder : MonoBehaviour
    {
        public string orderFilePath;
        public GameObject bugSolver;
        private string[] _orderText;
        public SymbolAutoRegistrationError Bug1 { get; private set; }

        public string[] GetOrderText() => _orderText;
        
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

        public void InitFirstBug(CaesarMachine caesarMachine)
        {
            Bug1 = new SymbolAutoRegistrationError(caesarMachine);
        }
    }
}