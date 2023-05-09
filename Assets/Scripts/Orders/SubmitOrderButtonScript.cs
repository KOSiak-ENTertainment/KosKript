using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialogManager;
using GameManagementScripts;
using UnityEngine;

public class SubmitOrderButtonScript : MonoBehaviour
{
    public string thanks;
    public GameObject submitButton;

    public async void SubmitFirstOrder()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        gameManager.gameState = GameManagerScript.GameStates.FirstOrderCompleted;

        if (thanks != null)
        {
            var dialogManager = GameObject.Find("DialogsManager").GetComponent<DialogManagerScript>();
            dialogManager.orderText.text = thanks;
            thanks = null;
        }

        await WaitAndChangeGameState(gameManager, GameManagerScript.GameStates.SecondOrder);
    }
    
    async Task<Task> WaitAndChangeGameState(GameManagerScript gameManager, GameManagerScript.GameStates newGameState)
    {
        await Task.Delay(5000);
        gameManager.gameState = newGameState;
        submitButton.SetActive(false);
        Debug.Log("GG");

        return Task.CompletedTask;
    }
}