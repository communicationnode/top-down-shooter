//script icon image: ... iconoutofsync.png;

using UnityEngine;
using ExampleThirdPersonShooter.Player;
public sealed class ModifierBonus : BonusMonoBehaviour
{
    public enum Type { NONE, SPEEDUP, INVULNERABILITY }
    public Type type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCarcass>(out PlayerCarcass player))
        {
            player.modifiersModule.HandleGettedBonus(type);
            Destroy(gameObject);

            return;
        }
    }
}

