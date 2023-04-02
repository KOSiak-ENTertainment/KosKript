using UnityEngine;

namespace AppSpripts
{
    public class EncryptionMachinesAppScript : MonoBehaviour
    {
        public GameObject currentApp;
        
        public GameObject ordersApp;
        public GameObject documentsApp;
        public GameObject surrenderPeopleApp;
        
        public void OpenMachinesApp()
        {
            if (ordersApp.activeSelf)
            {
                ordersApp.GetComponent<OrdersAppScript>().CloseApp();
            }
            if (documentsApp.activeSelf)
            {
                documentsApp.GetComponent<DocumentsAppScript>().CloseApp();
            }
            if (surrenderPeopleApp.activeSelf)
            {
                surrenderPeopleApp.GetComponent<SurrenderPeopleAppScript>().CloseApp();
            }

            currentApp.SetActive(true);
        }
        
        public void CloseApp()
        {
            currentApp.SetActive(false);
        }
    }
}