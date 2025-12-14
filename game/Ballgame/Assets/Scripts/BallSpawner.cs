using System.Collections.Generic;
using Sockets;
using UnityEngine;


public class BallSpawner : MonoBehaviour {

    [SerializeField] private BallData ballPrefab;
    [SerializeField] private float circleRadius = 10;

    public void Spawn(GameOptionDto gameOption) {
        BallData instance = Instantiate(ballPrefab, transform);
        instance.transform.localPosition = Random.insideUnitCircle * Random.Range(0f, circleRadius);
        instance.SetOption(gameOption);
    }

}