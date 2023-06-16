using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioSource audioSource;
    private bool _isPlaying;
    private Button _playButton;
    private Slider _progressSlider;
    private bool _isDraggingSlider;
    private bool _wasPlayingBeforeDrag;
    private float _dragStartProgress;

    void Start()
    {
        _playButton = gameObject.transform.Find("PlayPauseButton").GetComponent<Button>();
        _playButton.onClick.AddListener(ToggleAudio);

        _progressSlider = gameObject.transform.Find("ProgressSlider").GetComponent<Slider>();
        _progressSlider.onValueChanged.AddListener(SeekAudio);

        _isPlaying = false;
        _isDraggingSlider = false;
        _wasPlayingBeforeDrag = false;
    }

    void Update()
    {
        if (_isPlaying && !_isDraggingSlider)
        {
            if (!audioSource.isPlaying)
            {
                _isPlaying = false;
                audioSource.Stop();
                _playButton.GetComponentInChildren<Text>().text = "Play";
                _progressSlider.value = 0f;
            }
            else
            {
                _progressSlider.value = audioSource.time / audioSource.clip.length;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDraggingSlider = true;
        _wasPlayingBeforeDrag = _isPlaying;
        if (_wasPlayingBeforeDrag)
        {
            audioSource.Pause();
        }
        _dragStartProgress = _progressSlider.value;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDraggingSlider = false;
        if (_wasPlayingBeforeDrag)
        {
            audioSource.Play();
        }
        var targetTime = _progressSlider.value * audioSource.clip.length;
        audioSource.time = targetTime;

        if (Math.Abs(_progressSlider.value - 1f) < 1e-5 && !audioSource.isPlaying)
        {
            _isPlaying = false;
            _playButton.GetComponentInChildren<Text>().text = "Play";
        }
    }

    public void ToggleAudio()
    {
        if (_isPlaying)
        {
            _isPlaying = false;
            audioSource.Pause();
            _playButton.GetComponentInChildren<Text>().text = "Play";
        }
        else
        {
            _isPlaying = true;
            audioSource.Play();
            _playButton.GetComponentInChildren<Text>().text = "Pause";
        }
    }

    void SeekAudio(float progress)
    {
        if (!_isDraggingSlider)
        {
            var targetTime = progress * audioSource.clip.length;
            if (targetTime < audioSource.clip.length)
                audioSource.time = targetTime;
        }
        else
        {
            // Корректировка значения слайдера в пределах допустимых границ
            var correctedValue = Mathf.Clamp01(progress);
            _progressSlider.value = correctedValue;
        }
    }
}