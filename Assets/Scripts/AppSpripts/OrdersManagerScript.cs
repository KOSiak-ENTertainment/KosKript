using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersManagerScript : MonoBehaviour
{
    public List<Button> orderButtons; // список всех кнопок заказов
    private int _currentOrderIndex = -1; // индекс текущей открытой кнопки, -1 означает, что ни одна кнопка не открыта

    public void OpenOrder(int index)
    {
        if (_currentOrderIndex != -1)
        {
            // если уже открыта другая кнопка, деактивируем ее текстовые объекты
            orderButtons[_currentOrderIndex].transform.Find("CustomerName").gameObject.SetActive(false);
            orderButtons[_currentOrderIndex].transform.Find("OrderText").gameObject.SetActive(false);
        }

        // активируем текстовые объекты выбранной кнопки
        orderButtons[index].transform.Find("CustomerName").gameObject.SetActive(true);
        orderButtons[index].transform.Find("OrderText").gameObject.SetActive(true);

        _currentOrderIndex = index; // обновляем индекс текущей открытой кнопки
    }
}