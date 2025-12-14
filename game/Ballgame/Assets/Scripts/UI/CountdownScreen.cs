using System;
using TMPro;
using UnityEngine;

namespace UI {
    public class CountdownScreen : MonoBehaviour {
        [SerializeField] private TMP_Text cdText;

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void SetCountdown(int text) {
            cdText.text = text.ToString();
        }

        private void Awake() {
            gameObject.SetActive(false);
        }
    }
}