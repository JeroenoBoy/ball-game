using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class optionmenu : MonoBehaviour
{
    public string title;
    public TMP_InputField titlefieldTmpro;

    public void SetTitle()
    {
        title = titlefieldTmpro.text;
    }
}
