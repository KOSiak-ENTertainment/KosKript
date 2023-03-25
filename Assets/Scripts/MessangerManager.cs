using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessangerManager : MonoBehaviour
{  
    public GameObject objectToDeactivate;
 
    private void Start()
    {
         objectToDeactivate.SetActive(false);
    }
}
