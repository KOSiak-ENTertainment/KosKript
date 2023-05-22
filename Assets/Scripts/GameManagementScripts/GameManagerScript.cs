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
        public Text solvedOrdersCounter;
        public OrdersManager ordersManager;
        public DialogManagerScript dialogManager;
        public GameStates gameState = GameStates.NothingToDo;
        public OrderLoading ordersState = OrderLoading.WithoutOrders;
        public int countOfSolvedOrders = 0;
        public List<Button> orderButtons;

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

        public enum OrderLoading
        {
            FirstOrderLoaded,
            SecondOrderLoaded,
            WithoutOrders
        }

        public void Start()
        {
            orderButtons[0].gameObject.SetActive(true);
            dialogManager.Start();
            dialogManager.ShowFirstCustomerDialogs();
        }

        public void Update()
        {
            ManageOrderState();
            
            if (gameState == GameStates.SecondOrder)
            {
                orderButtons[1].gameObject.SetActive(true);
                UpdateMaxPossibleShift();
                dialogManager.ShowSecondCustomerDialogs();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.ThirdOrder)
            {
                Debug.Log("FUSKDJFLSKDJFSLDKFJSDLFK!!!!!");
                gameState = GameStates.NothingToDo;
            }
        }

        private void ManageOrderState()
        {
            if (ordersState == OrderLoading.FirstOrderLoaded)
            {
                ordersManager.SolveFirstOrder();
                ordersState = OrderLoading.WithoutOrders;
            }
            
            if (ordersState == OrderLoading.SecondOrderLoaded)
            {
                ordersManager.SolveSecondOrder();
                ordersState = OrderLoading.WithoutOrders;
            }
        }
        
        private void UpdateMaxPossibleShift() => ordersManager.maxPossibleShift += 2;
        
        private IEnumerator WaitForSeconds(int countOfSeconds)
        {
            yield return new WaitForSeconds(countOfSeconds);
        }
    }
}