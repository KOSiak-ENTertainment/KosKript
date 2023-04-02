using UnityEngine;

namespace AppSpripts
{
    public class EncryptionMachinesAppScript : MonoBehaviour
    {
        public GameObject CurrentApp;
        
        public GameObject OrdersApp;
        public GameObject DocumentsApp;
        public GameObject SurrenderPeopleApp;
        
        public void OpenMachinesApp()
        {
            if (OrdersApp.activeSelf)
            {
                OrdersApp.GetComponent<OrdersAppScript>().CloseApp();
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