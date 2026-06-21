using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] Sprite[] sprites = new Sprite[9];
    private SpriteRenderer cachedRenderer;

    private void Awake()
    {
        cachedRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(string spellName)
    {
        if (cachedRenderer == null) return;

        switch (spellName)
        {
            case "Curse":
                cachedRenderer.sprite = sprites[0];
                break;
            case "Heal":
                cachedRenderer.sprite = sprites[1];
                break;
            case "Fire Bolt":
                cachedRenderer.sprite = sprites[2];
                break;
            case "Frostbite":
                cachedRenderer.sprite = sprites[3];
                break;
            case "Poison":
                cachedRenderer.sprite = sprites[4];
                break;
            case "Sacrifice":
                cachedRenderer.sprite = sprites[5];
                break;
            case "Plant Growth":
                cachedRenderer.sprite = sprites[6];
                break;
            case "Aqua Splash":
                cachedRenderer.sprite = sprites[7];
                break;
            case "Thunder Bolt":
                cachedRenderer.sprite = sprites[8];
                break;
            default:
                cachedRenderer.sprite = null;
                break;
        }
    }
}
