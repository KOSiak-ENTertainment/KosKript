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
        public int countOfSolvedOrders;
        public List<Button> orderButtons;
        public List<Button> taskbarButtons;

        public enum GameStates
        {
            FirstOrder,
            SecondOrder,
            ThirdOrder,
            FifthOrder,
            FirstOrderCompleted,
            SecondOrderCompleted,
            ThirdOrderCompleted,
            FifthOrderCompleted,
            NothingToDo
        }

        public enum OrderLoading
        {
            FirstOrderLoaded,
            SecondOrderLoaded,
            ThirdOrderLoaded,
            FifthOrderLoaded,
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
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.ThirdOrder)
            { 
                orderButtons[2].gameObject.SetActive(true);
                
                UpdateMaxPossibleShift();
                dialogManager.ShowThirdCustomerDialogs();
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.FifthOrder)
            { 
                orderButtons[3].gameObject.SetActive(true);
                
                UpdateMaxPossibleShift();
                dialogManager.ShowFifthCustomerDialogs();
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
        }

        private int GetCurrentOrderNumber() => countOfSolvedOrders + 1;

        private void SolveOrder() => ordersManager.SolveOrder(GetCurrentOrderNumber());

        private void ManageOrderState()
        {
            if (ordersState == OrderLoading.FirstOrderLoaded)
            {
                SolveOrder();
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.SecondOrderLoaded)
            {
                SolveOrder();
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.ThirdOrderLoaded)
            {
                SolveOrder();
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.FifthOrderLoaded)
            {
                SolveOrder();
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
        }
        
        private void UpdateMaxPossibleShift() => ordersManager.maxPossibleShift += 2;
        
        private IEnumerator WaitForSeconds(int countOfSeconds)
        {
            yield return new WaitForSeconds(countOfSeconds);
        }

        private void MakeSolveOrderHints()
        {
            HighlightEncryptionMachinesButton();
            HighlightDocumentsButton();
        }
        
        public void HighlightOrdersButton() => taskbarButtons[1].GetComponent<HighlightButton>().ChangeButtonColor();

        private void HighlightEncryptionMachinesButton() => taskbarButtons[2].GetComponent<HighlightButton>().ChangeButtonColor();
        
        private void HighlightDocumentsButton() => taskbarButtons[0].GetComponent<HighlightButton>().ChangeButtonColor();
    }
}