using System;
using Sockets;
using TMPro;
using UnityEngine;

public class OptionWon : MonoBehaviour {

    [SerializeField] private TMP_Text winText;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private void Awake() {
    }

    private void Start() {
        canvasGroup.alpha = 0;
        GameEventBus.instance.PlayerFinished += HandlePlayerFinished;
    }

    private void OnDestroy() {
        GameEventBus.instance.PlayerFinished -= HandlePlayerFinished;
    }

    private void HandlePlayerFinished(GameOptionDto obj) {
        canvasGroup.alpha = 1;
        winText.text = obj.name;
        if (ColorUtility.TryParseHtmlString(obj.color, out Color color)) {
            winText.color = color;
        }
    }
}