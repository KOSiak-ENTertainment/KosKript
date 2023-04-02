using UnityEngine;

namespace AppSpripts
{
    public class DocumentsAppScript : MonoBehaviour
    {
        public GameObject CurrentApp;
        
        public GameObject EncryptionMachinesApp;
        public GameObject OrdersApp;
        public GameObject SurrenderPeopleApp;
        
        public void OpenDocumentsApp()
        {
            if (EncryptionMachinesApp.activeSelf)
            {
                EncryptionMachinesApp.GetComponent<EncryptionMachinesAppScript>().CloseApp();
            }
            if (SurrenderPeopleApp.activeSelf)
            {
                SurrenderPeopleApp.GetComponent<SurrenderPeopleAppScript>().CloseApp();
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