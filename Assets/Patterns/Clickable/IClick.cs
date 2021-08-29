using UnityEngine;

namespace Patterns
{
    public interface IClick
    {
        public float Health { get; }

        public void Click(Vector2 pos, float maxDamage, float damageRadius);

        public void InitHealth(float hpValue);
    }
}
