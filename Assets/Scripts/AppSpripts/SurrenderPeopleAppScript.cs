using UnityEngine;

namespace AppSpripts
{
    public class SurrenderPeopleAppScript : MonoBehaviour
    {
        public GameObject CurrentApp;
        
        public GameObject EncryptionMachinesApp;
        public GameObject OrdersApp;
        public GameObject DocumentsApp;

        public void OpenSurrenderPeopleApp()
        {
            if (EncryptionMachinesApp.activeSelf)
            {
                EncryptionMachinesApp.GetComponent<EncryptionMachinesAppScript>().CloseApp();
            }
            if (DocumentsApp.activeSelf)
            {
                DocumentsApp.GetComponent<DocumentsAppScript>().CloseApp();
            }
            if (OrdersApp.activeSelf)
            {
                OrdersApp.GetComponent<OrdersAppScript>().CloseApp();
            }

            CurrentApp.SetActive(true);
        }
        
        public void CloseApp()
        {
            CurrentApp.SetActive(false);
        }
    }
}