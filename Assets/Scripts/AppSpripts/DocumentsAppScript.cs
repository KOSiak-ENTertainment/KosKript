using UnityEngine;

namespace AppSpripts
{
    public class DocumentsAppScript : MonoBehaviour
    {
        public GameObject currentApp;
        
        public GameObject encryptionMachinesApp;
        public GameObject ordersApp;
        public GameObject surrenderPeopleApp;
        
        public void OpenDocumentsApp()
        {
            if (encryptionMachinesApp.activeSelf)
            {
                encryptionMachinesApp.GetComponent<EncryptionMachinesAppScript>().CloseApp();
            }
            if (surrenderPeopleApp.activeSelf)
            {
                surrenderPeopleApp.GetComponent<SurrenderPeopleAppScript>().CloseApp();
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