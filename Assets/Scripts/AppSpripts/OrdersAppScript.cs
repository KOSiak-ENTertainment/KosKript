using UnityEngine;

namespace AppSpripts
{
    public class OrdersAppScript : MonoBehaviour
    {
        public GameObject currentApp;
        
        public GameObject encryptionMachinesApp;
        public GameObject documentsApp;
        public GameObject surrenderPeopleApp;
        
        public void OpenOrdersApp()
        {
            if (encryptionMachinesApp.activeSelf)
            {
                encryptionMachinesApp.GetComponent<EncryptionMachinesAppScript>().CloseApp();
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