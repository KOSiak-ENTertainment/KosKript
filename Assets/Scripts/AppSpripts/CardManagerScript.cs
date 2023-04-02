using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManagerScript : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public List<GameObject> cards = new();
    private int _currentIndex;

    void Start()
    { 
        leftButton.onClick.AddListener(ShowPrevCard);
        rightButton.onClick.AddListener(ShowNextCard);
    }

    private void ShowPrevCard()
    {
        cards[_currentIndex].SetActive(false);
        _currentIndex--;
        if (_currentIndex < 0) _currentIndex = cards.Count - 1;
        cards[_currentIndex].SetActive(true);
    }

    private void ShowNextCard()
    {
        cards[_currentIndex].SetActive(false);
        _currentIndex++;
        if (_currentIndex >= cards.Count) _currentIndex = 0;
        cards[_currentIndex].SetActive(true);
    }
}