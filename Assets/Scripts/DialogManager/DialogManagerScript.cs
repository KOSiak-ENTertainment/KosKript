using GameManagementScripts;
using Orders;
using UnityEngine;
using UnityEngine.UI;

namespace DialogManager
{
    public class DialogManagerScript : MonoBehaviour
    {
        public Text customerName;
        public Text orderText;
        public Scrollbar Scrollbar;
        public Button submitOrderButton;
        public GameObject audioPlayer;
        public GameObject ordersManager;
        private TextTyperScript _textTyperScript;

        public void ShowSomeDialog(int indexOfCustomer)
        {
            var ordersManagerScript = ordersManager.GetComponent<OrdersManager>();
            var audioSource = audioPlayer.GetComponent<AudioSource>();
            var audioSlider = audioSource.GetComponentInChildren<Slider>();
            audioSlider.value = 0f;
            audioSource.Play();
            audioSource.clip = ordersManagerScript.orders[indexOfCustomer].GetComponent<Order>().dialogSound;
            ShowDialog(ordersManagerScript.orders[indexOfCustomer].GetComponent<Order>().dialogFilePath);
        }
        
        private void ShowDialog(string dialogPath)
        {
            _textTyperScript = gameObject.GetComponent<TextTyperScript>();
            var dialogParagraphs = _textTyperScript.GetTextParagraphs(dialogPath);
            
            customerName.text = dialogParagraphs[0];
            orderText.text = dialogParagraphs[1];

            if (dialogParagraphs.Length == 3)
                submitOrderButton.GetComponent<SubmitOrderButtonScript>().thanks = dialogParagraphs[2];
        }
    }
}
