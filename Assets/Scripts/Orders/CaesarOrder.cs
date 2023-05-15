using System;
using UnityEngine;
using System.IO;
using MachinesScripts;

namespace Orders
{
    public class CaesarOrder : MonoBehaviour
    {
        public string orderFilePath; //TODO Использовать orderFilePath
        public GameObject bugSolver;
        private string[] _orderText;
        public GameObject submitOrder;

        public BugCreator Bug1 { get; private set; }

        public string[] GetOrderText() => _orderText; //TODO Использовать этот метод
        
        public void InitBug(CaesarMachine caesarMachine)
        {
            Bug1 = new BugCreator(caesarMachine);
        }
    }
}