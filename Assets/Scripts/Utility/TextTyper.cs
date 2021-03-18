using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTyper : MonoBehaviour
{
    #region Singleton
    public static TextTyper instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("Multiple TextTypers in Scene");
            Debug.Log(gameObject.name + " was Destroyed");
            Destroy(this.gameObject);
        }
    }
    #endregion

    [SerializeField] TMP_Text descriptionText;


    public void TypeText(string inputString)
    {
        StartCoroutine(Printer(inputString));
    }

    IEnumerator Printer(string stringToPrint)
    {

        Time.timeScale = Mathf.Epsilon;

        descriptionText.text = "";
        for (int i = 0; i < stringToPrint.Length; i++)
        {
            descriptionText.text += stringToPrint[i];
            yield return new WaitForSecondsRealtime(.02f);
        }
        yield return null;

    }

}
