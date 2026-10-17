using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = AudioListener.volume;
        if (MusicManager.instance == null)
        {
            Debug.LogError("MusicManager не найден!");
            return;
        }

        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(
            MusicManager.instance.SetVolume
        );
    }
}