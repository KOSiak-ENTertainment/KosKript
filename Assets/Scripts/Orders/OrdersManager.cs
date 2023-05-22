using System.Collections;
using System.Collections.Generic;
using Documents;
using GameManagementScripts;
using MachinesScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Orders
{
    public class OrdersManager : MonoBehaviour
    {
        public List<GameObject> orders;
        public Text encryptionMachineTextUI;
        public int countOfOrders = 3;
        public int maxPossibleShift = 2;
        public GameObject documentsButtonsManager;

        public void SolveFirstOrder() => SolveOrder(1, "Orders/ArchieRochesterOrder.txt");
        
        public void SolveSecondOrder() => SolveOrder(2, "Orders/KolesnikovaTamaraOrder.txt");

        private void SolveOrder(int numOfOrder, string orderFilePath)
        {
            var firstOrderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
            if (gameObject == null) 
                return;
            var doc = documentsButtonsManager.GetComponent<DocumentsButtonsManager>();
            doc.ChangeCurrentOrderText(gameObject.AddComponent<TextTyperScript>().GetTextParagraphs(orderFilePath)[0]);

            var caesarMachine =
                new CaesarMachine(gameObject.AddComponent<TextTyperScript>().GetTextParagraphs(orderFilePath)[0],
                    maxPossibleShift, false, 1);
            firstOrderScript.InitBug(caesarMachine);
            var bug1 = firstOrderScript.Bug1;
            var firstPartOfText = caesarMachine.EncodedFile.Substring(0, bug1.IntervalBegin);
            var bugSolver = firstOrderScript.bugSolver;
            var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
            var inputField = bugSolver.transform.Find("InputField").GetComponent<InputField>();

            encryptionMachineTextUI.text = firstPartOfText;
            bugSolver.SetActive(true);
            inputField.text = bug1.EncryptedPieceText;

            bugWarning.text = "Зашифруйте данную часть текста: \"" + bug1.UnencryptedPieceText + "\" со сдвигом: " + caesarMachine.CaesarAlphabet.Shift;

            StartCoroutine(WaitForInputAndValidate(inputField, bug1.EncryptedPieceText, 3,
                firstOrderScript.submitOrder, numOfOrder, orderFilePath));
        }

        private void ContinueOrder(string userInput, CaesarMachine caesarMachine, int numOfOrder)
        {
            var firstOrderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
            var bug1 = firstOrderScript.Bug1;
            
            encryptionMachineTextUI.text += userInput;
            encryptionMachineTextUI.text += caesarMachine.EncodedFile.Substring(bug1.IntervalBegin + bug1.CountCipherSymbol);
        }
        
        private IEnumerator WaitForInputAndValidate(InputField inputField, string correctInput, int maxAttempts, GameObject submitOrder, int numOfOrder, string orderFilePath)
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
                    inputField.text = null;
                    Debug.Log("input field cleaned");
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
                submitOrder.SetActive(true);
                ContinueOrder(correctInput,
                    new CaesarMachine(gameObject.AddComponent<TextTyperScript>().GetTextParagraphs(orderFilePath)[0],
                        maxPossibleShift, false, 1), numOfOrder);
                var orderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
                var bugSolver = orderScript.bugSolver;
                var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                bugWarning.text = "Шифорвка выполнена успешно!";
            }
            else
            {
                Debug.Log("Out of attempts.");

                // Reset some fields and start again
                var orderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
                var doc = documentsButtonsManager.GetComponent<DocumentsButtonsManager>();
            
                doc.ChangeCurrentOrderText(gameObject.AddComponent<TextTyperScript>().GetTextParagraphs(orderFilePath)[0]);
                var caesarMachine =
                    new CaesarMachine(gameObject.AddComponent<TextTyperScript>().GetTextParagraphs(orderFilePath)[0],
                        maxPossibleShift, false, 1);
                orderScript.InitBug(caesarMachine);
                var bug1 = orderScript.Bug1;
                var firstPartOfText = caesarMachine.EncodedFile.Substring(0, bug1.IntervalBegin);
                var bugSolver = orderScript.bugSolver;
                var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                
                encryptionMachineTextUI.text = firstPartOfText;
                bugSolver.SetActive(true);
                inputField.text = bug1.EncryptedPieceText;

                bugWarning.text = "Зашифруйте данную часть текста: \"" + bug1.UnencryptedPieceText + "\" со сдвигом: " +
                                  caesarMachine.CaesarAlphabet.Shift;

                yield return StartCoroutine(WaitForInputAndValidate(inputField, bug1.EncryptedPieceText, 3,
                    orderScript.submitOrder, numOfOrder, orderFilePath));
            }

            Debug.Log("Coroutine finished!");

            // Wait for 2 seconds before executing the code below
            yield return new WaitForSeconds(2);

            Debug.Log("Some code that should run after the coroutine has finished.");
        }
    }
}