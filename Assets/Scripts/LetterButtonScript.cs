using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterButtonScript : MonoBehaviour
{
    public void OnClick() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
