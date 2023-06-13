using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSpeed : MonoBehaviour
{
    private const float ScrollSpeed = 0.5f;
    private Scrollbar _scrollbar;

    private void Start()
    {
        _scrollbar = gameObject.GetComponent<Scrollbar>();
    }

    private void Update()
    {
        var scrollInput = Input.GetAxis("Mouse ScrollWheel");
        
        _scrollbar.value += scrollInput * ScrollSpeed;
    }
}