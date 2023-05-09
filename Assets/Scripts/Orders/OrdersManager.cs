using System.Collections;
using System.Collections.Generic;
using MachinesScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Orders
{
    public class OrdersManager : MonoBehaviour
    {
        public List<GameObject> orders;
        public Text encryptionMachineTextUI;

        public void SolveFirstOrder()
        {
            var firstOrderScript = orders[0].GetComponent<CaesarOrder>();
            var caesarMachine = new CaesarMachine(firstOrderScript.GetTextParagraphs("Orders/ArchieRochesterOrder.txt")[0]);
            firstOrderScript.InitFirstBug(caesarMachine);
            var bug1 = firstOrderScript.Bug1;
            var firstPartOfText = caesarMachine.UncodedFile.Substring(0, bug1.IntervalBegin);
            var bugSolver = firstOrderScript.bugSolver;
            var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
            var inputField = bugSolver.transform.Find("InputField").GetComponent<InputField>();

            encryptionMachineTextUI.text = firstPartOfText;
            bugSolver.SetActive(true);

            bugWarning.text = "Зашифруйте данную часть текста: \"" + bug1.UnencryptedPieceText + "\"";

            StartCoroutine(WaitForInputAndValidate(inputField, bug1.UnencryptedPieceText, 3));
            
            Debug.Log("Fuck");
        }
        
        private IEnumerator WaitForInputAndValidate(InputField inputField, string correctInput, int maxAttempts)
        {
            var attempts = 0;
            var inputCorrect = false;

            while (!inputCorrect && attempts < maxAttempts)
            {
                // Wait for user to press enter
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                if (inputField.text == correctInput)
                {
                    // Input is correct, break out of loop
                    inputCorrect = true;
                }
                else
                {
                    // Input is incorrect, display warning message and increment attempts
                    attempts++;
                    inputField.text = "";
                    Debug.Log($"Incorrect input. {maxAttempts - attempts} attempts left.");
                }
            }

            if (inputCorrect)
            {
                Debug.Log("Input correct!");
            }
            else
            {
                Debug.Log("Out of attempts.");

                // Reset some fields and start again
                var firstOrderScript = orders[0].GetComponent<CaesarOrder>();
                var caesarMachine = new CaesarMachine(firstOrderScript.GetTextParagraphs("Orders/ArchieRochesterOrder.txt")[0]);
                var bug1 = new SymbolAutoRegistrationError(caesarMachine);
                var firstPartOfText = caesarMachine.UncodedFile.Substring(0, bug1.IntervalBegin);
                var bugSolver = firstOrderScript.bugSolver;
                var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                
                encryptionMachineTextUI.text = firstPartOfText;
                bugSolver.SetActive(true);
        
                bugWarning.text = "Зашифруйте данную часть текста: \"" + bug1.UnencryptedPieceText + "\"";
        
                yield return StartCoroutine(WaitForInputAndValidate(inputField, bug1.UnencryptedPieceText, 3));
            }

            Debug.Log("Coroutine finished!");

            // Wait for 2 seconds before executing the code below
            yield return new WaitForSeconds(2);

            Debug.Log("Some code that should run after the coroutine has finished.");
        }
    }
}