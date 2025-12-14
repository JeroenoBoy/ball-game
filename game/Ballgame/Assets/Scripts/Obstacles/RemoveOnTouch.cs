using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles {
    public class RemoveOnTouch : MonoBehaviour {

        private Color[] colors;

        private int currHealth = 0;

        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            GenerateColors();
            UpdateColor();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            currHealth++;
            UpdateColor();
        }

        private void UpdateColor() {
            if (currHealth >= colors.Length) {
                Destroy(gameObject);
                return;
            }
            spriteRenderer.color = colors[currHealth];
        }

        private void GenerateColors() {
            float s = Random.Range(0.20f, 0.35f);
            float v = Random.Range(0.95f, 1f);
            colors = new[] { Color.HSVToRGB(0.5f, s, v), Color.HSVToRGB(0.4f, s, v), Color.HSVToRGB(0.3f, s, v), Color.HSVToRGB(0.2f, s, v), Color.HSVToRGB(0.1f, s, v), Color.HSVToRGB(0.0f, s, v) };
        }
    }
}