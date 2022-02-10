using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helion4x.Runtime.Gui
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private GameObject timeManagerOject;
        private TimeManager _timeManager;
        
        private Label _timeLabel;
        private Label _timewarpLabel;
        private Button _slowerButton;
        private Button _pauseButton;
        private Button _fasterButton;

        private void Start()
        {
            _timeManager = timeManagerOject.GetComponent<TimeManager>();
            var root = GetComponent<UIDocument>().rootVisualElement;
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

        private void Update()
        {
            _timeLabel.text = _timeManager.Time.ToString(CultureInfo.InvariantCulture);
            _timewarpLabel.text = _timeManager.Timewarp.ToString();
        }
    }
}
