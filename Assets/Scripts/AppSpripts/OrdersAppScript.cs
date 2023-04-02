using UnityEngine;

namespace AppSpripts
{
    public class OrdersAppScript : MonoBehaviour
    {
        public GameObject CurrentApp;
        
        public GameObject EncryptionMachinesApp;
        public GameObject DocumentsApp;
        public GameObject SurrenderPeopleApp;
        
        public void OpenOrdersApp()
        {
            if (EncryptionMachinesApp.activeSelf)
            {
                EncryptionMachinesApp.GetComponent<EncryptionMachinesAppScript>().CloseApp();
            }
            if (DocumentsApp.activeSelf)
            {
                DocumentsApp.GetComponent<DocumentsAppScript>().CloseApp();
            }
            if (SurrenderPeopleApp.activeSelf)
            {
                SurrenderPeopleApp.GetComponent<SurrenderPeopleAppScript>().CloseApp();
            }

            CurrentApp.SetActive(true);
        }
        
        public void CloseApp()
        {
            CurrentApp.SetActive(false);
        }
    }
}