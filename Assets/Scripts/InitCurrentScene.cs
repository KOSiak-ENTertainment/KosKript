using System.Collections.Generic;
using GameManagementScripts;
using Orders;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InitCurrentScene : MonoBehaviour
    {
        public GameObject buttonPrefab;
        public GameObject parentObject;

        private GameManagerScript _gameManagerScript;
        private OrdersManager _ordersManager;
        
        public void Start()
        {
            InitGameManager();
            InitOrdersManager();

            for (var i = 0; i < _ordersManager.orders.Count; i++)
            {
                CreateButton(i + 1, _gameManagerScript.orderButtons);
            }
            
            _gameManagerScript.StartGame();
        }

        private void InitGameManager() =>
            _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        
        private void InitOrdersManager() => _ordersManager =
            GameObject.Find("OrdersManager").GetComponent<OrdersManager>();
        
        private void CreateButton(int numOfButton, List<Button> buttons)
        {
            var buttonObject = Instantiate(buttonPrefab, parentObject.transform);
            buttonObject.transform.localPosition = buttonPrefab.transform.localPosition;

            var textComponent = buttonObject.transform.Find("OrderButtonText").GetComponent<Text>();
            textComponent.text = "Заказ №" + numOfButton;

            var buttonComponent = buttonObject.GetComponent<Button>();
            buttons.Add(buttonComponent);
            
            if (buttonComponent != null)
            {
                var loaderScript = GameObject.Find("Loader").GetComponent<LoaderManager>();
                buttonComponent.onClick.AddListener(DisableButtonOnClick);
            }
        }
        
        private void DisableButtonOnClick()
        {
            var loaderScript = GameObject.Find("Loader").GetComponent<LoaderManager>();
            loaderScript.OnButtonClick();
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            button.interactable = false;
        }
    }
}