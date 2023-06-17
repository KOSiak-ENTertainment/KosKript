using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteController : MonoBehaviour
{
    public Sprite pressedSprite;
    public Sprite releasedSprite;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = gameObject.GetComponent<Image>();
        buttonImage.sprite = releasedSprite;
    }

    public void OnButtonPressed()
    {
        buttonImage.sprite = pressedSprite;
    }

    public void OnButtonReleased()
    {
        buttonImage.sprite = releasedSprite;
    }
}