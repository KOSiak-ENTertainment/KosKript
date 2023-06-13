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

        public void StartGame()
        {
            ActivateCurrentOrderButton();
            dialogManager.Start();
            dialogManager.ShowSomeDialog(countOfSolvedOrders);
        }

        public void Update()
        {
            ManageOrderState();
            
            if (gameState == GameStates.SecondOrder)
            {
                ActivateCurrentOrderButton();
                
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(countOfSolvedOrders);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.ThirdOrder)
            { 
                ActivateCurrentOrderButton();
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(countOfSolvedOrders);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.FourthOrder)
            { 
                ActivateCurrentOrderButton();
                
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(countOfSolvedOrders);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.FifthOrder)
            { 
                ActivateCurrentOrderButton();
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(countOfSolvedOrders);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.SixthOrder)
            { 
                ActivateCurrentOrderButton();
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(countOfSolvedOrders);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            if (gameState == GameStates.SeventhOrder)
            { 
                ActivateCurrentOrderButton();
                UpdateMaxPossibleShift();
                dialogManager.ShowSomeDialog(countOfSolvedOrders);
                HighlightOrdersButton();
                gameState = GameStates.NothingToDo;
            }
            
            
        }
        
        private void ActivateCurrentOrderButton() => orderButtons[countOfSolvedOrders].gameObject.SetActive(true);

        private int GetCurrentOrderNumber() => countOfSolvedOrders + 1;

        private void SolveOrder() => ordersManager.SolveOrder(GetCurrentOrderNumber());

        private void ManageOrderState()
        {
            if (ordersState != OrderLoading.WithoutOrders)
            {
                SolveOrder();
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