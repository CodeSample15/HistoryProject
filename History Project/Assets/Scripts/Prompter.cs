using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Prompter : MonoBehaviour
{
    //public
    public bool finished;

    //private
    private TextMeshProUGUI textBox;
    private string currentText;

    void Awake()
    {
        finished = true;

        textBox = GetComponent<TextMeshProUGUI>();
        textBox.SetText("");
    }

    public void printText(string text, float speed)
    {
        currentText = "";
        StartCoroutine(printingText(text, 0, speed));
    }

    IEnumerator printingText(string text, int index, float speed)
    {
        finished = false;
        currentText = currentText + text[index];
        textBox.SetText(currentText);

        yield return new WaitForSeconds(speed);

        if(index == text.Length-1)
        {
            finished = true;
        }
        else
        {
            StartCoroutine(printingText(text, index+1, speed));
        }
    }
}