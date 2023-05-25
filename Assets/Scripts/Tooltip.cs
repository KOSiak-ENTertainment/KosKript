using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public GameObject hintPanel;
    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseHint);
    }

    public void ShowHint(string hintMessage)
    {
        hintPanel.SetActive(true);
        Text hintText = hintPanel.GetComponentInChildren<Text>();
        hintText.text = hintMessage;
    }

    public void CloseHint()
    {
        hintPanel.SetActive(false);
    }
}