using UnityEngine;
using UnityEngine.UI;

namespace Orders.Quant
{
    public class KeyManager : MonoBehaviour
    {
        public InputField[] InputFields;
        public int[] Key;

        public void FillFieldsWithNumbers()
        {
            if (InputFields.Length != Key.Length)
            {
                Debug.LogError("InputFields and numbers arrays should have the same length.");
                return;
            }

            for (var i = 0; i < InputFields.Length; i++)
            {
                InputFields[i].text = Key[i].ToString();
            }
        }
    }
}