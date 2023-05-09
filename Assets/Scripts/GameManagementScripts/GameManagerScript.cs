using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MachinesScripts;
using Orders;
using UnityEngine.Serialization;

namespace GameManagementScripts
{
    public class GameManagerScript : MonoBehaviour
    { 
        public OrdersManager ordersManager;

        public void Start()
        {
            ordersManager.SolveFirstOrder();
        }
    }
}