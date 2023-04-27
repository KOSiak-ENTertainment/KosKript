using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public Text _textUI;
    public string _fileName;

    bool WaitUntil() => Input.GetKeyDown(KeyCode.Space);
    
    void Start()
    {
        WriteText();
    }

    private void WriteText()
    {
        var path = Path.Combine(Application.dataPath, _fileName);

        if (File.Exists(path))
        {
            var paragraphs = File.ReadAllText(path)
                .Split(new[] { "\r\n\r\n", "\n\n" }, System.StringSplitOptions.RemoveEmptyEntries);

            _textUI.text = "";

            for (var paragraphIndex = 0; paragraphIndex < paragraphs.Length; paragraphIndex++)
            {
                var paragraph = paragraphs[paragraphIndex];

                _textUI.text += paragraph + "\n\n";
            }
        }
        else
        {
            Debug.LogError("File not found: " + path);
        }
    }

}