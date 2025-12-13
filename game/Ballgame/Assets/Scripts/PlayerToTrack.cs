using System;
using Unity.Cinemachine;
using UnityEngine;


public class PlayerToTrack : MonoBehaviour {

    [SerializeField] private Transform playerParent;
    
    private CinemachineCamera cinemachineCamera;

    private void Awake() {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    private void FixedUpdate() {
        Transform closestPlayer = null;
        float playerDistance = float.MaxValue;
        
        foreach (object o in playerParent) {
            Transform playerTransform = o as Transform;
            if (playerTransform.position.y > playerDistance) {
                continue;
            }

            closestPlayer = playerTransform;
            playerDistance = playerTransform.position.y;
        }

        if (closestPlayer == null) {
            return;
        }

        cinemachineCamera.Target.TrackingTarget = closestPlayer;
    }
}