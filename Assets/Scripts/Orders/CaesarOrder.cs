using System;
using UnityEngine;
using System.IO;
using GameManagementScripts;
using MachinesScripts;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Orders
{
    public class CaesarOrder : MonoBehaviour
    {
        public string orderFilePath; //TODO Использовать orderFilePath
        public GameObject firstBugSolver;
        public GameObject secondBugSolver;
        private string[] _orderText;
        public GameObject submitOrder;

        public void LoadOrderText() =>
            _orderText = gameObject.AddComponent<TextTyperScript>().GetTextParagraphs(orderFilePath);

        public BugCreator Bug { get; private set; }

        public string[] GetOrderText() => _orderText;
        
        public void InitBug(CaesarMachine caesarMachine)
        {
            Bug = new BugCreator(caesarMachine);
        }
        
        public GameObject RandomlySelectBugSolver()
        {
            var selectedBugSolver = 
                Random.Range(1, 2) == 0 
                    ? firstBugSolver 
                    : secondBugSolver;
            
            Debug.Log("Selected Bug Solver: " + selectedBugSolver.name);

            return selectedBugSolver;
        }
    }
}