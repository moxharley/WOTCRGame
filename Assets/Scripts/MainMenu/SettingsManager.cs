using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private string masterVolumeParam;

    public void ToggleFullscreen(bool active)
    {
        Screen.fullScreenMode = active ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.fullScreen = active;
    }

    public void SetMasterVolume(float vol)
    {
        float clamped = Mathf.Clamp(vol, 0.001f, 1f);
        float dB = Mathf.Log10(clamped) * 20f;
        masterMixer.SetFloat(masterVolumeParam, dB);
    }
}
