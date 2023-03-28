using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppOpener : MonoBehaviour
{
    public GameObject app;

    public void ToggleMessenger() {
        app.SetActive(!app.activeSelf);
    }
}
