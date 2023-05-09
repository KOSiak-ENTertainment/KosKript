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
        public GameObject submitOrder;

        public SymbolAutoRegistrationError Bug1 { get; private set; }

        public string[] GetOrderText() => _orderText;
        
        public void InitBug(CaesarMachine caesarMachine)
        {
            Bug1 = new SymbolAutoRegistrationError(caesarMachine);
        }
    }
}