using GameManagementScripts;
using UnityEngine;

namespace Orders
{
    public class Order : MonoBehaviour
    {
        public string orderFilePath;
        public string dialogFilePath;
        public string[] orderText;
        public GameObject submitOrder;
        public AudioClip orderSound;
        public AudioClip dialogSound;
        
        public void LoadOrderText() =>
            orderText = gameObject.AddComponent<TextTyperScript>().GetTextParagraphs(orderFilePath);
        
        public string[] GetOrderText() => orderText; 
    }
}