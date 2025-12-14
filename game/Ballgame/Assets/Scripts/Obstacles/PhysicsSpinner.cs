using System;
using UnityEngine;

namespace Obstacles {
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsSpinner : MonoBehaviour {

        [SerializeField] private float spinForce;

        private Rigidbody rb;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            rb.AddTorque(0, 0, spinForce, ForceMode.Force);
        }
    }
}