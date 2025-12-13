
using System;
using Sockets;
using UnityEngine;
using UnityEngine.Events;


public class Finish : MonoBehaviour {

    public GameOptionDto WinningOption { get; private set; } = null;
    public bool BallFinished => WinningOption != null;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (BallFinished) { return; }
        if (!other.TryGetComponent(out BallData ball)) { return; }
        WinningOption = ball.GameOption;
        GameEventBus.instance.PlayerFinished?.Invoke(WinningOption);
    }
}