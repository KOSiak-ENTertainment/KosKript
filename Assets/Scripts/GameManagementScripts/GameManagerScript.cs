using System;
using System.Collections;
using System.Collections.Generic;
using DialogManager;
using UnityEngine;
using UnityEngine.UI;
using MachinesScripts;
using Orders;
using UnityEngine.Serialization;

namespace GameManagementScripts
{
    public class GameManagerScript : MonoBehaviour
    { 
        public OrdersManager ordersManager;
        public DialogManagerScript dialogManager;
        public GameStates gameState = GameStates.NothingToDo;

        public enum GameStates
        {
            FirstOrder,
            SecondOrder,
            ThirdOrder,
            FirstOrderCompleted,
            SecondOrderCompleted,
            ThirdOrderCompleted,
            NothingToDo
        }

        public void Start()
        {
            dialogManager.Start();
            dialogManager.ShowFirstCustomerDialogs();
            ordersManager.SolveFirstOrder();
        }

        public void Update()
        {
            if (gameState == GameStates.SecondOrder)
            {
                dialogManager.ShowSecondCustomerDialogs();
                ordersManager.SolveSecondOrder();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.ThirdOrder)
            {
                Debug.Log("FUSKDJFLSKDJFSLDKFJSDLFK!!!!!");
                gameState = GameStates.NothingToDo;
            }
        }
        
        private IEnumerator WaitForSeconds(int countOfSeconds)
        {
            yield return new WaitForSeconds(countOfSeconds);
        }
    }
}