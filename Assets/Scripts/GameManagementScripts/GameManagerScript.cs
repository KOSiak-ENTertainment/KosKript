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
        public int countOfSolvedOrdersForUi;
        public int countOfSolvedOrders;
        public List<Button> orderButtons;
        public List<Button> taskbarButtons;

        public enum GameStates
        {
            FirstOrder,
            SecondOrder,
            ThirdOrder,
            FourthOrder,
            FifthOrder,
            SixthOrder,
            SeventhOrder,
            FirstOrderCompleted,
            SecondOrderCompleted,
            ThirdOrderCompleted,
            FourthOrderCompleted,
            FifthOrderCompleted,
            SixthOrderCompleted,
            SeventhOrderCompleted,
            NothingToDo
        }

        public enum OrderLoading
        {
            FirstOrderLoaded,
            SecondOrderLoaded,
            ThirdOrderLoaded,
            FourthOrderLoaded,
            FifthOrderLoaded,
            SixthOrderLoaded,
            SeventhOrderLoaded,
            WithoutOrders
        }

        public void Start()
        {
            solvedOrdersCounter.text = "Количество выполненных заказов: " + countOfSolvedOrdersForUi + " из 7";
        }

        public void Update()
        {
            ManageOrderState();

            if (gameState == GameStates.FirstOrder)
            {
                ActivateOrderButton(0);
                dialogManager.ShowSomeDialog(0);
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.SecondOrder)
            {
                ActivateOrderButton(1);
                
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(1);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.ThirdOrder)
            { 
                ActivateOrderButton(2);
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(2);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.FourthOrder)
            { 
                ActivateOrderButton(3);
                
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(3);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.FifthOrder)
            { 
                ActivateOrderButton(4);
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(4);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.SixthOrder)
            { 
                ActivateOrderButton(5);
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(5);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.SeventhOrder)
            { 
                ActivateOrderButton(6);
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(6);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            
        }
        
        private void ActivateOrderButton(int numOfOrderButton) => orderButtons[numOfOrderButton].gameObject.SetActive(true);

        private void SolveOrder(int numOfOrder) => ordersManager.SolveOrder(numOfOrder);

        private void ManageOrderState()
        {
            if (ordersState == OrderLoading.FirstOrderLoaded)
            {
                SolveOrder(1);
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.SecondOrderLoaded)
            {
                SolveOrder(2);
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.ThirdOrderLoaded)
            {
                SolveOrder(3);
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.FourthOrderLoaded)
            {
                SolveOrder(4);
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.FifthOrderLoaded)
            {
                SolveOrder(5);
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.SixthOrderLoaded)
            {
                SolveOrder(6);
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
            if (ordersState == OrderLoading.SeventhOrderLoaded)
            {
                SolveOrder(7);
                MakeSolveOrderHints();
                ordersState = OrderLoading.WithoutOrders;
            }
        }
        
        private void UpdateMaxPossibleShift() => ordersManager.maxPossibleShift += 2;
        
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