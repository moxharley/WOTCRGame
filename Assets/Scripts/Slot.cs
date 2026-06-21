using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] Sprite[] sprites = new Sprite[3];
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
            default:
                cachedRenderer.sprite = null;
                break;
        }
    }
}
