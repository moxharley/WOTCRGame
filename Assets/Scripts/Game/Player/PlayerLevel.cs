using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int currentXp = 0;

    public delegate void LevelUpEvent(int level);
    public static event LevelUpEvent OnLevelUp;

    private void Start()
    {
        level = Mathf.Max(1, level);
    }

    public void AddXp(int amount)
    {
        currentXp += amount;

        while (currentXp >= level*10)
        {
            LevelUp();
        }
    }

    [ContextMenu("Level Up")]
    public void LevelUp()
    {
        currentXp -= level * 10;
        level++;

        OnLevelUp?.Invoke(level);
    }
}
