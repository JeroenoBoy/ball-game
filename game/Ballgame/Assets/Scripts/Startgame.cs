using System.Collections;
using UnityEngine;
using TMPro;

public class Startgame : MonoBehaviour
{
    [SerializeField] float timeBetweenCD;
    [SerializeField] TextMeshProUGUI Cdtext;
    [SerializeField] GameObject startingCage;

    public void StartingGame()
    {
        StartCoroutine(StartGameCD());
    }

    private IEnumerator StartGameCD()
    {
        Cdtext.text = "3";
        yield return new WaitForSeconds(timeBetweenCD);
        Cdtext.text = "2";
        yield return new WaitForSeconds(timeBetweenCD);
        Cdtext.text = "1";
        yield return new WaitForSeconds(timeBetweenCD);
        startingCage.SetActive(false);
        Cdtext.text = "";
    }
}
