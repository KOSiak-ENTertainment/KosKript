using UnityEngine;

public class HintsManager : MonoBehaviour
{
    public GameObject hintForOrderButton;
    public GameObject hintForDocsButton;
    public GameObject hintForEmButton;
    public GameObject hintForSpButton;

    public void ShowHintForOrderWindow(string text) => hintForOrderButton.GetComponent<Tooltip>().ShowHint(text);
    
    public void ShowHintForDocsWindow(string text) => hintForDocsButton.GetComponent<Tooltip>().ShowHint(text);
    
    public void ShowHintForEmWindow(string text) => hintForEmButton.GetComponent<Tooltip>().ShowHint(text);
    
    public void ShowHintForSpWindow(string text) => hintForSpButton.GetComponent<Tooltip>().ShowHint(text);
}