using System;
using Sockets;
using UnityEngine;


public class BallData : MonoBehaviour {
    public GameOptionDto GameOption { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    
    public void SetOption(GameOptionDto gameOption) {
        GameOption = gameOption;
        if (ColorUtility.TryParseHtmlString(gameOption.color, out Color color)) {
            SpriteRenderer.color = color;
        }
    }

    private void Awake() {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}