using System.Threading.Tasks;
using DialogManager;
using GameManagementScripts;
using UnityEngine;

public class SubmitOrderButtonScript : MonoBehaviour
{
    public string thanks;
    public GameObject submitButton;
    public int numOfOrderToSubmit;

    public void SubmitFirstOrder() => SubmitOrder(numOfOrderToSubmit - 1);

    private async void SubmitOrder(int numOfOrder)
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        gameManager.gameState = (GameManagerScript.GameStates)numOfOrder + gameManager.ordersManager.countOfOrders;

        if (thanks != null)
        {
            var dialogManager = GameObject.Find("DialogsManager").GetComponent<DialogManagerScript>();
            dialogManager.orderText.text = thanks;
            thanks = null;
        }

        await WaitAndChangeGameState(gameManager, (GameManagerScript.GameStates)numOfOrder);
    }
    
    async Task WaitAndChangeGameState(GameManagerScript gameManager, GameManagerScript.GameStates newGameState)
    {
        await Task.Delay(5000);
        gameManager.gameState = newGameState + 1;
        submitButton.SetActive(false);
        Debug.Log("Order has been submitted");
        numOfOrderToSubmit++;
    }
}