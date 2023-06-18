using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Orders.Quant
{
    public class MVPIManager : MonoBehaviour
    {
        public Text textUI;
        public List<int> byteArray;

        public void PrintByteArray()
        {
            if (byteArray != null && textUI != null)
            {
                string numbers = "";
                
                for (int i = 0; i < byteArray.Count; i++)
                {
                    numbers += byteArray[i].ToString();

                    if (i < byteArray.Count - 1)
                    {
                        numbers += " ";
                    }
                }

                textUI.text = numbers;
            }
        }
    }
}