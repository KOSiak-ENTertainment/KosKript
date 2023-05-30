using UnityEngine;
using UnityEngine.UI;

public class HighlightButton : MonoBehaviour
{
    private Button button;
    private Color defaultColor;
    private bool isColorChanged = false;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        defaultColor = button.colors.normalColor;
    }

    public void ChangeButtonColor() => button.image.color = Color.red;

    public void ChangeButtonToDefaultColor() => button.image.color = defaultColor;
}