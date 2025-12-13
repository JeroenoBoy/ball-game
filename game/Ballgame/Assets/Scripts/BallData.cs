using Sockets;
using UnityEngine;


public class BallData : MonoBehaviour {
    public GameOptionDto GameOption { get; private set; }
    
    private SpriteRenderer spriteRenderer;

    public void SetOption(GameOptionDto gameOption) {
        GameOption = gameOption;
        if (ColorUtility.TryParseHtmlString(gameOption.color, out Color color)) {
            spriteRenderer.color = color;
        }
    }
}