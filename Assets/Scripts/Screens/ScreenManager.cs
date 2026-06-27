using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }
    private Controls controls;

    [Header("Screens")]
    [SerializeField] private GameObject pauseMenu;

    public bool IsPaused { get; private set; } = false;

    void Awake()
    {
        Instance = this;
        controls = new();
    }

    void Start()
    {
        PauseGame();
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
        TogglePause();
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        PauseGame();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(IsPaused);
        Time.timeScale = IsPaused ? 0f : 1f;
    }
}
