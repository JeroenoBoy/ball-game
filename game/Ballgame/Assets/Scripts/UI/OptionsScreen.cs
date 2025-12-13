using System;
using System.Collections.Generic;
using JUtils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI {
    public class OptionsScreen : SingletonBehaviour<OptionsScreen> {
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private Button submit;
        [SerializeField] private Button addWidget;
        [SerializeField] private int maxWidgets;
        [SerializeField] private Transform widgetParent;
        [SerializeField] private GameOptionWidget gameOptionWidgetPrefab;

        public event Action SubmitPressed;
        public event Action<GameOptionWidget> WidgetAdded; 
        
        public string Title => nameInput.text;
        public List<GameOptionWidget> OptionWidgets { get; } = new();
        
        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
            foreach (GameOptionWidget gameOptionWidget in OptionWidgets) {
                gameOptionWidget.Deleted -= HandleWidgetDeleted;
                Destroy(gameOptionWidget);
            }
            OptionWidgets.Clear();
        }

        protected override void Awake() {
            base.Awake();
            gameObject.SetActive(false);
            submit.onClick.AddListener(HandleSubmitClicked);
            addWidget.onClick.AddListener(HandleAddWidget);
        }

        public void HandleSubmitClicked() {
            SubmitPressed?.Invoke();
        }

        private void HandleAddWidget() {
            if (OptionWidgets.Count >= maxWidgets) {
                return;
            }

            GameOptionWidget widget = Instantiate(gameOptionWidgetPrefab, widgetParent);
            widget.Deleted += HandleWidgetDeleted;
            OptionWidgets.Add(widget);
            widget.Init(Color.HSVToRGB(Random.Range(0f, 1f), 0.5f, 0.5f));
            WidgetAdded?.Invoke(widget);
        }

        private void HandleWidgetDeleted(GameOptionWidget widget) {
            OptionWidgets.Remove(widget);
        }
    }
}