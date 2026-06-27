using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [Header("Scenes")]
    [SerializeField] private string mainMenuScene = "MainMenu";

    private Controls controls;

    void Awake()
    {
        Instance = this;
        controls = new();
    }

    void OnEnable()
    {
        controls.Enable();

        controls.UI.Pause.performed += TryPause;
    }

    void OnDisable()
    {
        controls.Disable();

        controls.UI.Pause.performed -= TryPause;
    }

    private void TryPause(InputAction.CallbackContext ctx = default)
    {
        SceneLoader.Instance.LoadScene(mainMenuScene);
    }
}
