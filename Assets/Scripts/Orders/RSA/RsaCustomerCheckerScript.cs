using UnityEngine;
using UnityEngine.UI;

namespace Orders.RSA
{
    public class RsaCustomerCheckerScript : MonoBehaviour
    {
        public GameObject firstBugSolver;
        public GameObject secondBugSolver;
        public string nameOfBugSolver = null;
        public GameObject taskSolver;
        
        private Button _button;
        private Text _textUI;

        private readonly string[] _uniqueTexts = { "Заказчик в базе", "Заказчик не в базе" };

        private void Start()
        {
            _button = GameObject.Find("CheckCustomer").GetComponent<Button>();
            _textUI = GameObject.Find("BaseStatus").GetComponent<Text>();
            
            _button.onClick.AddListener(ActivateRandomObject);
        }

        private void ActivateRandomObject()
        {
            if (nameOfBugSolver == null)
            { 
                _textUI.gameObject.SetActive(true);
                _textUI.text = "Вы ещё не загрузили ни одного заказа!!!";
            }
            
            else if (nameOfBugSolver.Equals("FirstBugSolver"))
            {
                _textUI.gameObject.SetActive(true);
                _textUI.text = _uniqueTexts[0];
                taskSolver.SetActive(true);
            }
            
            else if (nameOfBugSolver.Equals("SecondBugSolver"))
            {
                _textUI.gameObject.SetActive(true);
                _textUI.text = _uniqueTexts[1];
                taskSolver.SetActive(true);
            }
           
        }
    }
}