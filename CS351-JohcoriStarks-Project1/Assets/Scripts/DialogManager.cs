using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentence;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogPanel;
    // Start is called before the first frame update

    private void OnEnable()
    {
        StartCoroutine(Type());
        {

        }
    }

    IEnumerator Type()
    {
        textbox.text = "";
        foreach (char letter in sentence[index])
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }





    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentence.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());


        }
        else
        {
            textbox.text = "";
        }
    }
}
