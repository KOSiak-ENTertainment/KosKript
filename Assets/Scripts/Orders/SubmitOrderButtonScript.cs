using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialogManager;
using GameManagementScripts;
using Orders.RSA;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitOrderButtonScript : MonoBehaviour
{
    public GameObject submitButton;
    public RsaCustomerCheckerScript customerCheckerScript;
    public int numOfOrderToSubmit;
    public List<GameObject> objectsForDeactivate;
    public string thanks = string.Empty;

    public void SubmitFirstOrder() => SubmitOrder(numOfOrderToSubmit - 1);

    private async void SubmitOrder(int numOfOrder)
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        
        gameManager.gameState = (GameManagerScript.GameStates)numOfOrder + gameManager.ordersManager.orders.Count;
        gameManager.countOfSolvedOrdersForUi++;
        gameManager.countOfSolvedOrders++;
        gameManager.solvedOrdersCounter.text = "Количество выполненных заказов: " + gameManager.countOfSolvedOrdersForUi + " из 7";

        if (thanks != string.Empty)
        {
            var dialogManager = GameObject.Find("DialogsManager").GetComponent<DialogManagerScript>();
            dialogManager.orderText.text = thanks;
            thanks = string.Empty;
        }
        
        gameManager.HighlightOrdersButton();
        submitButton.gameObject.SetActive(false);
        await WaitAndChangeGameState(gameManager, (GameManagerScript.GameStates)numOfOrder);
    }
    
    async Task WaitAndChangeGameState(GameManagerScript gameManager, GameManagerScript.GameStates newGameState)
    {
        await Task.Delay(10000);
        gameManager.gameState = newGameState + 1;
        Debug.Log("Order has been submitted");
        numOfOrderToSubmit++;
        if (objectsForDeactivate != null && objectsForDeactivate.Count != 0 && SceneManager.GetActiveScene().name.Equals("SecondDay"))
        {
            DeactivateObjects();
            ActivateLastButton();
            customerCheckerScript.nameOfBugSolver = null;
        }
        if (objectsForDeactivate != null && objectsForDeactivate.Count != 0 && SceneManager.GetActiveScene().name.Equals("ThirdDay"))
        {
            DeactivateObjects();
        }
    }

    private void DeactivateObjects()
    {
        foreach (var obj in objectsForDeactivate)
        {
            obj.SetActive(false);
        }
    }

    private void ActivateLastButton()
    {
        if (objectsForDeactivate[^1].TryGetComponent(out Button component))
        {
            component.interactable = true;
        }
    }
}