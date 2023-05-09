using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public void Start()
        {
            _textTyperScript = gameObject.AddComponent<TextTyperScript>();
        }

        public void ShowFirstDayDialogs()
        {
            ShowDialog("Dialogs/ArchieRochesterDialog.txt", true);
        }
        
        private void ShowDialog(string dialogPath, bool isThereGratitude)
        {
            var dialogParagraphs = _textTyperScript.GetTextParagraphs(dialogPath); 
            
            customerName.text = dialogParagraphs[0];
            orderText.text = dialogParagraphs[1];
            
            if (isThereGratitude)
            {
                var submitOrderButtonScript = submitOrderButton.GetComponent<SubmitOrderButtonScript>();
                submitOrderButtonScript.thanks = dialogParagraphs[2];
            }
        }
    }
}
