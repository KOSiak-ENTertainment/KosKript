using UnityEngine;
using UnityEngine.UI;

public class QuTunnelTesterScript : MonoBehaviour
{
    public GameObject bugSolver;
    
    private Text _text;
    private Button _button;

    public void Start()
    {
        bugSolver.SetActive(false);
        _text = gameObject.transform.Find("TesterText").GetComponent<Text>();
        _button = gameObject.transform.Find("TesterButton").GetComponent<Button>();
        _text.gameObject.SetActive(false);
        _button.onClick.AddListener(ShowText);
    }

    private void ShowText() 
    {
        _text.gameObject.SetActive(true);
        bugSolver.SetActive(true);
    }
}
