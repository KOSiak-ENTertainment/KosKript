using System.Collections;
using System.Collections.Generic;
using Documents;
using MachinesScripts;
using Orders.Quant;
using Orders.RSA;
using UnityEngine;
using UnityEngine.UI;
using static MachinesScripts.RSA;

namespace Orders
{
    public class OrdersManager : MonoBehaviour
    {
        public List<GameObject> orders;
        public Text encryptionMachineTextUI;
        public int maxPossibleShift = 2;
        public GameObject documentsButtonsManager;

        public void SolveOrder(int numOfOrder)
        {
            var machineName = orders[numOfOrder - 1].GetComponent<Order>().machineName;
            
            if (machineName.Equals("Caesar"))
            {
                var orderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
                var orderFilePath = orderScript.orderFilePath;
                orderScript.LoadOrderText();
                if (gameObject == null) 
                    return;
                var doc = documentsButtonsManager.GetComponent<DocumentsButtonsManager>();
                doc.ChangeCurrentOrderText(orderScript.GetOrderText()[0]);

                var caesarMachine =
                    new CaesarMachine(orderScript.GetOrderText()[0],
                        maxPossibleShift, false, 1);
                orderScript.InitBug(caesarMachine);
                var bug = orderScript.Bug;
                var firstPartOfText = caesarMachine.EncodedFile.Substring(0, bug.IntervalBegin);
                var bugSolver = orderScript.RandomlySelectBugSolver();
                if (bugSolver.name == "FirstBugSolver")
                {
                    var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                    var inputField = bugSolver.transform.Find("InputField").GetComponent<InputField>();

                    encryptionMachineTextUI.text = firstPartOfText;
                    bugSolver.SetActive(true);

                    bugWarning.text = "System Null Reference: Зашифруйте данную часть текста: \"" + bug.UnencryptedPieceText + "\" со сдвигом: " +
                                      caesarMachine.CaesarAlphabet.Shift;

                    StartCoroutine(WaitForInputAndValidate(inputField, bug.EncryptedPieceText, 3,
                        orderScript.submitOrder, numOfOrder, orderFilePath));
                }
                if (bugSolver.name == "SecondBugSolver")
                {
                    var shiftInputField = bugSolver.transform.Find("ShiftInputField").GetComponent<InputField>();
                    var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                    var mostPopularLetter = bugSolver.transform.Find("MostPopularLetter").GetComponent<Text>();
                    var popularLetterButton = bugSolver.transform.Find("GetPopularLetter").GetComponent<Button>();

                    bugWarning.text = "SystemOverflowLetter";
                    mostPopularLetter.text = "Самая частотная буква: ";

                    encryptionMachineTextUI.text = firstPartOfText;
                    bugSolver.SetActive(true);

                    popularLetterButton.onClick.AddListener(() =>
                    {
                        mostPopularLetter.text = "Самая частотная буква: " + bug.MostPopularLetterInText;
                    });

                    StartCoroutine(WaitForShiftInputAndValidate(shiftInputField, bug.Shift.ToString(), 3, orderScript.submitOrder, numOfOrder, orderFilePath));
                }
            }
            else if (machineName.Equals("RSA"))
            {
                var orderScript = orders[numOfOrder - 1].GetComponent<RsaOrder>();
                orderScript.LoadOrderText();
                orderScript.customerChecker.nameOfBugSolver = null;
                var doc = documentsButtonsManager.GetComponent<DocumentsButtonsManager>();
                doc.ChangeCurrentOrderText(orderScript.GetOrderText()[0]);
                var rsa = new MachinesScripts.RSA(orderScript.GetOrderText()[0]);

                var randomIndex = Random.Range(0, 2);

                // Активируем выбранный объект и деактивируем другой
                if (randomIndex == 0)
                {
                    Debug.Log("FirstBug!!!");
                    var firstBug = new Ruk1Bug(rsa);
                    orderScript.customerChecker.firstBugSolver.SetActive(true);
                    orderScript.customerChecker.secondBugSolver.SetActive(false);
                    orderScript.customerChecker.nameOfBugSolver = "FirstBugSolver";
                    orderScript.taskButtonScript.ModN = firstBug.ModulN;
                    var encryptorScript = orderScript.taskButtonScript.encryptor.GetComponent<EncryptorScript>();
                    encryptorScript.CodSymbol = firstBug.CodSymbol;
                    encryptorScript.CompleteSymbol = firstBug.CompleteSymbol;
                    encryptorScript.isInputEnabled = true;
                    encryptorScript.nums.text = encryptorScript.GetCodSymbol();
                }
                else
                {
                    Debug.Log("SecondBug!!!");
                    orderScript.customerChecker.firstBugSolver.SetActive(false);
                    orderScript.customerChecker.secondBugSolver.SetActive(true);
                    orderScript.customerChecker.nameOfBugSolver = "SecondBugSolver";
                    orderScript.CharacterSolverScript.FirstNum = rsa.SimpleNumberOne;
                    orderScript.CharacterSolverScript.SecondNum = rsa.SimpleNumberTwo;
                    orderScript.EulerSolverScript.correctValues = new List<string>()
                    {
                        rsa.SimpleNumberOne.ToString(), rsa.SimpleNumberTwo.ToString(),
                        (rsa.SimpleNumberOne - 1).ToString(), (rsa.SimpleNumberTwo - 1).ToString()
                    };
                    orderScript.EulerSolverScript.successDisplayed = false;
                }
            }
            else if (machineName.Equals("Quant"))
            {
                Debug.Log("QUANT!");
                var orderScript = orders[numOfOrder - 1].GetComponent<QuantOrder>();
                orderScript.LoadOrderText();
                var doc = documentsButtonsManager.GetComponent<DocumentsButtonsManager>();
                doc.ChangeCurrentOrderText(orderScript.GetOrderText()[0]);
                var bug = new BB84(orderScript.GetOrderText()[0]);
                var ruk = new Ruk2Bug(bug);
                orderScript.KeyManager.Key = ruk.SecretKey;
                orderScript.KeyManager.FillFieldsWithNumbers();
                orderScript.MvpiManager.byteArray = ruk.Number;
                orderScript.MvpiManager.PrintByteArray();
            }
        }

        private void ContinueOrder(string userInput, CaesarMachine caesarMachine, int numOfOrder)
        {
            var firstOrderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
            var bug1 = firstOrderScript.Bug;
            
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
                var orderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
                ContinueOrder(correctInput,
                    new CaesarMachine(orderScript.GetOrderText()[0],
                        maxPossibleShift, false, 1), numOfOrder);
                var bugSolver = orderScript.firstBugSolver;
                var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                bugWarning.text = "Шифорвка выполнена успешно! Вы можете начать новый заказ!";
                bugSolver.SetActive(false);
            }
            else
            {
                Debug.Log("Out of attempts.");

                // Reset some fields and start again
                var orderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
                var doc = documentsButtonsManager.GetComponent<DocumentsButtonsManager>();
            
                doc.ChangeCurrentOrderText(orderScript.GetOrderText()[0]);
                var caesarMachine =
                    new CaesarMachine(orderScript.GetOrderText()[0],
                        maxPossibleShift, false, 1);
                orderScript.InitBug(caesarMachine);
                var bug = orderScript.Bug;
                var firstPartOfText = caesarMachine.EncodedFile.Substring(0, bug.IntervalBegin);
                var bugSolver = orderScript.firstBugSolver;
                var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                
                encryptionMachineTextUI.text = firstPartOfText;
                bugSolver.SetActive(true);

                bugWarning.text = "System Null Reference: Зашифруйте данную часть текста: \"" + bug.UnencryptedPieceText + "\" со сдвигом: " +
                                  caesarMachine.CaesarAlphabet.Shift;

                yield return StartCoroutine(WaitForInputAndValidate(inputField, bug.EncryptedPieceText, 3,
                    orderScript.submitOrder, numOfOrder, orderFilePath));
            }

            Debug.Log("Coroutine finished!");

            // Wait for 2 seconds before executing the code below
            yield return new WaitForSeconds(2);

            Debug.Log("Some code that should run after the coroutine has finished.");
        }
        
        private IEnumerator WaitForShiftInputAndValidate(InputField shiftInputField, string correctShift, int maxAttempts, GameObject submitOrder, int numOfOrder, string orderFilePath)
        {
            var attempts = 0;
            var inputCorrect = false;

            while (!inputCorrect && attempts < maxAttempts)
            {
                // Wait for user to press enter
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                if (shiftInputField.text == correctShift)
                {
                    // Input is correct, break out of loop
                    inputCorrect = true;
                    shiftInputField.text = null;
                    Debug.Log("Shift input field cleaned");
                }
                else
                {
                    // Input is incorrect, display warning message and increment attempts
                    attempts++;
                    shiftInputField.text = "";
                    Debug.Log($"Incorrect shift input. {maxAttempts - attempts} attempts left.");
                }
            }

            if (inputCorrect)
            {
                Debug.Log("Shift input correct!");
                submitOrder.SetActive(true);
                var orderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
                ContinueOrder("", new CaesarMachine(orderScript.GetOrderText()[0], maxPossibleShift, false, 1), numOfOrder);
                var bugSolver = orderScript.secondBugSolver;
                var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                //bugWarning.text = "Шифровка выполнена успешно! Вы можете начать новый заказ!";
                bugSolver.SetActive(false);
            }
            else
            {
                Debug.Log("Out of shift input attempts.");

                // Reset some fields and start again
                var orderScript = orders[numOfOrder - 1].GetComponent<CaesarOrder>();
                var doc = documentsButtonsManager.GetComponent<DocumentsButtonsManager>();

                doc.ChangeCurrentOrderText(orderScript.GetOrderText()[0]);
                var caesarMachine = new CaesarMachine(orderScript.GetOrderText()[0], maxPossibleShift, false, 1);
                orderScript.InitBug(caesarMachine);
                var bug2 = orderScript.Bug;
                var firstPartOfText = caesarMachine.EncodedFile.Substring(0, bug2.IntervalBegin);
                var bugSolver = orderScript.secondBugSolver;
                var bugWarning = bugSolver.transform.Find("BugWarning").GetComponent<Text>();
                var mostPopularLetter = bugSolver.transform.Find("MostPopularLetter").GetComponent<Text>();
                var popularLetterButton = bugSolver.transform.Find("GetPopularLetter").GetComponent<Button>();
                bugWarning.text = "SystemOverflowLetter";

                encryptionMachineTextUI.text = firstPartOfText;
                bugSolver.SetActive(true);
                
                mostPopularLetter.text = "Самая частотная буква: ";

                popularLetterButton.onClick.AddListener(() =>
                {
                    mostPopularLetter.text = "Самая частотная буква: " + bug2.MostPopularLetterInText;
                });

                yield return StartCoroutine(WaitForShiftInputAndValidate(shiftInputField, correctShift, 2, orderScript.submitOrder, numOfOrder, orderFilePath));
            }

            Debug.Log("Shift input coroutine finished!");

            // Wait for 2 seconds before executing the code below
            yield return new WaitForSeconds(2);

            Debug.Log("Some code that should run after the shift input coroutine has finished.");
        }
    }
}