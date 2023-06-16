using System;
using UnityEngine;
using System.IO;
using GameManagementScripts;
using MachinesScripts;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Orders
{
    public class CaesarOrder : Order
    {
        public CaesarMachine CaesarMachine { private set; get; }
        
        public GameObject firstBugSolver;
        public GameObject secondBugSolver;

        public BugCreator Bug { get; private set; }

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