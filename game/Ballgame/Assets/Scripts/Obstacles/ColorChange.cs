using System;
using UnityEngine;

namespace Obstacles {
    public class ColorChange : MonoBehaviour {

        private MeshRenderer meshRenderer;
        private MaterialPropertyBlock propertyBlock;

        private float lastChanged = 0;

        private void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            propertyBlock = new MaterialPropertyBlock();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (Time.time - lastChanged < 0.5f) { return; }
            if (!other.gameObject.TryGetComponent(out BallData ballData)) { return;}
            lastChanged = Time.time;
            propertyBlock.SetColor("_BaseColor", ballData.SpriteRenderer.color);
            meshRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}