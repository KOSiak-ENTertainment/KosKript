using UnityEngine;
using UnityEngine.Serialization;

namespace Orders.Quant
{
    public class QuantOrder : Order
    {
        public QuTunnelTesterScript quTunnelTesterScript;
        public KeyManager KeyManager;
    }
}