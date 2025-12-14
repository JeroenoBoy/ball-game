using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GameJoinScreen : MonoBehaviour {

        [SerializeField] private TMP_Text codeTxt;
        [SerializeField] private Image qrImage;
        [SerializeField] private Button submitButton;

        public event Action SubmitPressed;

        public void Show() {
            gameObject.SetActive(true);
            Color color = Color.white;
            color.a = 0;
            qrImage.color = color;
            SetCode("???-???");
            SetQrCode(null);
        }
        
        public void Hide() {
            gameObject.SetActive(false);
        }

        public void SetCode(string code) {
            codeTxt.text = $"Join: balls.jeroenvdg.com\ncode: {code}";
        }

        public void SetQrCode(Sprite sprite) {
            qrImage.DOFade(sprite != null ? 1 : 0, 0.25f);
            qrImage.sprite = sprite;
        }
        
        private void Awake() {
            gameObject.SetActive(false);
            submitButton.onClick.AddListener(HandleSubmitPressed);
        }

        private void OnDestroy() {
            submitButton.onClick.RemoveListener(HandleSubmitPressed);
        }

        private void HandleSubmitPressed() {
            SubmitPressed?.Invoke();
        }
    }
}