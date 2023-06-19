using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Text textUI;
    
    private string previousText = "";

    private void Start()
    {
        previousText = textUI.text;
        UpdateScrollView();
    }
    
    private void Update()
    {
        if (previousText != textUI.text)
        {
            UpdateScrollView();
        }
    }

    private void UpdateScrollView()
    {
        // Обновляем размеры контента
        ReloadContentSize();

        // Проверяем, требуется ли скролл
        bool shouldScroll = textUI.preferredHeight > scrollRect.viewport.rect.height;
        scrollRect.verticalScrollbar.gameObject.SetActive(shouldScroll);
    }

    private void ReloadContentSize()
    {
        // Устанавливаем преферированный размер контента
        RectTransform contentRect = scrollRect.content;
        contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textUI.preferredHeight);

        // Устанавливаем преферированный размер TextUI
        RectTransform textRect = textUI.rectTransform;
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textUI.preferredHeight);
    }
}