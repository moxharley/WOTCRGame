using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    
}
