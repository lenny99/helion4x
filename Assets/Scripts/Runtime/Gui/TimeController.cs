using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helion4x.Runtime.Gui
{
    public abstract class BaseController : MonoBehaviour
    {
        protected bool isBound;

        private void Start()
        {
            isBound = false;
            StartUi();
        }

        private void Update()
        {
            if (!isBound) return;
            UpdateUi();
        }

        protected abstract void StartUi();

        protected abstract void UpdateUi();

        public void Bind(VisualElement root)
        {
            BindUi(root);
            isBound = true;
        }

        protected abstract void BindUi(VisualElement root);
    }

    public class TimeController : MonoBehaviour
    {
        [SerializeField] private TimeManager timeManager;

        private Button _fasterButton;
        private Button _pauseButton;
        private Button _slowerButton;
        private Label _timeLabel;
        private TimeManager _timeManager;
        private Label _timewarpLabel;

        private void Start()
        {
            _timeManager = timeManager.GetComponent<TimeManager>();
            var root = GetComponentInParent<UIDocument>().rootVisualElement;
            BindUi(root);
        }

        private void Update()
        {
            _timeLabel.text = _timeManager.Time.ToString(CultureInfo.InvariantCulture);
            _timewarpLabel.text = _timeManager.Timewarp.ToString();
        }


        private void BindUi(VisualElement root)
        {
            _timeLabel = root.Q<Label>("TimeLabel");
            _timewarpLabel = root.Q<Label>("TimewarpLabel");
            _slowerButton = root.Q<Button>("SlowerButton");
            _slowerButton.clicked += OnSlowerButtonClicked;
            _pauseButton = root.Q<Button>("PauseButton");
            _pauseButton.clicked += OnPauseButtonClicked;
            _fasterButton = root.Q<Button>("FasterButton");
            _fasterButton.clicked += OnFasterButtonClicked;
        }

        private void OnSlowerButtonClicked()
        {
            _timeManager.SlowDown();
        }

        private void OnPauseButtonClicked()
        {
            _timeManager.Pause();
        }

        private void OnFasterButtonClicked()
        {
            _timeManager.SpeedUp();
        }
    }
}