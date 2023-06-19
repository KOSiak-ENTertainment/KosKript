using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IndexesManager : MonoBehaviour
{
    public Text textUI;
    public List<int> indexesArray;

    public void PrintIndexesArray()
    {
        if (indexesArray != null && textUI != null)
        {
            string numbers = "";
                
            for (int i = 0; i < indexesArray.Count; i++)
            {
                numbers += indexesArray[i].ToString();

                if (i < indexesArray.Count - 1)
                {
                    numbers += "\t";
                }
            }

            textUI.text = numbers;
        }
    }
}
