using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GameOptionWidget : MonoBehaviour {

        [SerializeField] private TMP_InputField textInput;
        [SerializeField] private Image bgImage;
        [SerializeField] private Button removeButton;

        public event Action<GameOptionWidget> Deleted;

        public string OptionName => textInput.text;
        public Color OptionColor { get; private set; }

        public void Init(Color color) {
            OptionColor = color;
            bgImage.color = color;
        }

        private void Awake() {
            removeButton.onClick.AddListener(HandleRemoveButtonPressed);
        }

        private void OnDestroy() {
            removeButton.onClick.RemoveListener(HandleRemoveButtonPressed);
            Deleted?.Invoke(this);
        }

        private void HandleRemoveButtonPressed() {
            Destroy(gameObject);
        }
    }
}