using UnityEngine;

namespace AppSpripts
{
    public class SurrenderPeopleAppScript : MonoBehaviour
    {
        public GameObject currentApp;
        
        public GameObject encryptionMachinesApp;
        public GameObject ordersApp;
        public GameObject documentsApp;

        public void OpenSurrenderPeopleApp()
        {
            if (encryptionMachinesApp.activeSelf)
            {
                encryptionMachinesApp.GetComponent<EncryptionMachinesAppScript>().CloseApp();
            }
            if (documentsApp.activeSelf)
            {
                documentsApp.GetComponent<DocumentsAppScript>().CloseApp();
            }
            if (ordersApp.activeSelf)
            {
                ordersApp.GetComponent<OrdersAppScript>().CloseApp();
            }

            currentApp.SetActive(true);
        }
        
        public void CloseApp()
        {
            currentApp.SetActive(false);
        }
    }
}