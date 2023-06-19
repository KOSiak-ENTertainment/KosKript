using UnityEngine;
using UnityEngine.Serialization;

namespace Orders.Quant
{
    public class QuantOrder : Order
    {
        public QuTunnelTesterScript quTunnelTesterScript;
        public KeyManager KeyManager;
        public MVPIManager MvpiManager;
        public IndexesManager IndexesManager;
        public NumberInput NumberInput;
    }
}