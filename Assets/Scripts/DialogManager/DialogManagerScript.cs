using GameManagementScripts;
using UnityEngine;
using UnityEngine.UI;

namespace DialogManager
{
    public class DialogManagerScript : MonoBehaviour
    {
        public Text customerName;
        public Text orderText;
        public Button submitOrderButton;
        private TextTyperScript _textTyperScript;

        public void Start() => _textTyperScript = gameObject.AddComponent<TextTyperScript>();

        public void ShowFirstCustomerDialogs() => ShowDialog("Dialogs/ArchieRochesterDialog.txt");

        public void ShowSecondCustomerDialogs() => ShowDialog("Dialogs/KolesnikovaTamaraDialog.txt");
        
        public void ShowThirdCustomerDialogs() => ShowDialog("Dialogs/SolmatovaEkaterinaDialog.txt");
        
        public void ShowFourthCustomerDialogs() => ShowDialog("Dialogs/PetrSanDialog.txt");
        
        public void ShowFifthCustomerDialogs() => ShowDialog("Dialogs/ComradeNDialog.txt");
        
        public void ShowSixthCustomerDialogs() => ShowDialog("Dialogs/UtevDialog.txt");
        
        private void ShowDialog(string dialogPath)
        {
            var dialogParagraphs = _textTyperScript.GetTextParagraphs(dialogPath);
            
            customerName.text = dialogParagraphs[0];
            orderText.text = dialogParagraphs[1];

            if (dialogParagraphs.Length != 3) 
                return;
            
            submitOrderButton.GetComponent<SubmitOrderButtonScript>().thanks = dialogParagraphs[2];
        }
    }
}
