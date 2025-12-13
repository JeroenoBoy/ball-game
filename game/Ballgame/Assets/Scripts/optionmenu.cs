using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
public class optionmenu : MonoBehaviour
{
    public string title;
    public TMP_InputField titlefieldTmpro;
    [SerializeField] int themes;
    public TMP_InputField[] themeOptions;
    public GameObject inputFieldpref;
    public GameObject parentToPlace;

    public void SetTitle()
    {
        title = titlefieldTmpro.text;
    }

    public void PlusThemes()
    {
        themes = themes + 1;
    }
    public void MinusThemes()
    {
        themes = themes - 1;
        
    }
    public void GenerateThemes()
    {
        System.Array.Resize(ref themeOptions, themes);

        for (int i = 0; i < themeOptions.Length; i++)
        {
            GameObject currentInputField = Instantiate(inputFieldpref);
            currentInputField.transform.SetParent(parentToPlace.transform);
            themeOptions[i] = currentInputField.GetComponent<TMP_InputField>();
        }
    }
}
