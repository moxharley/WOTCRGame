using System;
using UnityEngine;

namespace grcubes
{
    public abstract class Spell : ScriptableObject
    {
        public string spellName;
        public Sprite spellSprite;

        public abstract void Cast(Vector2 origin, float rotation);
    }
}