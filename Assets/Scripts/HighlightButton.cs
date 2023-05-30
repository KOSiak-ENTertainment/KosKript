using UnityEngine;
using UnityEngine.UI;

public class HighlightButton : MonoBehaviour
{
    public Button button;
    private Color originalColor;
    private float highlightStartTime;

    private void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.colors.normalColor;
    }

    private void Update()
    {
        // Проверяем, прошло ли достаточное время для подсветки кнопки
        if (Time.time - highlightStartTime >= 1.0f)
        {
            // Возвращаем оригинальный цвет кнопки
            ColorBlock colors = button.colors;
            colors.normalColor = originalColor;
            button.colors = colors;
        }
    }

    public void HighlightButtons()
    {
        // Запоминаем время начала подсветки и устанавливаем новый цвет кнопки
        highlightStartTime = Time.time;
        ColorBlock colors = button.colors;
        colors.normalColor = Color.red;
        button.colors = colors;
    }
}