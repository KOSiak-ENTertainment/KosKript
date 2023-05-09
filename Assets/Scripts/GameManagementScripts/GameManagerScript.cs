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
        public GameStates gameState = GameStates.FirstOrder;

        public enum GameStates
        {
            NothingToDo,
            FirstOrder,
            FirstOrderCompleted,
            SecondOrder,
            SecondOrderCompleted
        }

        public void Start()
        {
            dialogManager.Start();
            dialogManager.ShowFirstDayDialogs();
            ordersManager.SolveFirstOrder();
        }

        public void Update()
        {
            if (gameState == GameStates.SecondOrder)
            {
                Debug.Log("LOOOOOOOOOOOOOOOOOOL!");
                gameState = GameStates.SecondOrderCompleted;
            }
        }
        
        private IEnumerator WaitForSeconds(int countOfSeconds)
        {
            yield return new WaitForSeconds(countOfSeconds);
        }
    }
}