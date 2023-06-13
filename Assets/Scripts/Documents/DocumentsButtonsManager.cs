using System;
using System.Collections.Generic;
using System.Linq;
using GameManagementScripts;
using Orders;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Documents
{
    public class DocumentsButtonsManager : MonoBehaviour
    {
        public string machineDeviceFilePath;
        public string machineAlgorithmFilePath;
        public string machineBugsFilePath;
        public Text documentText; 
        public Button[] appButtons;
        public GameObject ordersManager;
        public AudioSource audioPlayer;
        private List<string> _machinesDocs;

        public void Start()
        {
            var textTyperScript = gameObject.AddComponent<TextTyperScript>();
            
            _machinesDocs = new List<string>
            {
                textTyperScript.ReadTextFile(machineDeviceFilePath),
                textTyperScript.ReadTextFile(machineAlgorithmFilePath),
                textTyperScript.ReadTextFile(machineBugsFilePath),
                "Вы ещё не начали ни одного заказа!"
            };
            
            for (var i = 0; i < appButtons.Length; i++)
            {
                var index = i;
                appButtons[i].onClick.AddListener(() => { ShowCanvas(index); });
            }
        }

        public void ChangeCurrentOrderText(string text)
        {
            _machinesDocs[3] = text;
            documentText.text = text;
            var ordersManagerScript = ordersManager.GetComponent<OrdersManager>();
            var audioSource = audioPlayer.GetComponent<AudioSource>();
            var audioSlider = audioSource.GetComponentInChildren<Slider>();
            audioSlider.value = 0f;
            audioSource.clip = ordersManagerScript.orders[GameObject.Find("GameManager").GetComponent<GameManagerScript>().countOfSolvedOrders].GetComponent<Order>().orderSound;
        }

        private void ShowCanvas(int indexToShow)
        {
            documentText.text = "";

            documentText.text = _machinesDocs[indexToShow];
        }
    }
}