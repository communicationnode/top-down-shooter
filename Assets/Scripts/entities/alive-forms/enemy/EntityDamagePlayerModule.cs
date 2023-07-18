using UnityEngine;
using ExampleThirdPersonShooter.Player;

[AddComponentMenu("Universal Entity Modules/Damage A Player Module")]
public sealed class EntityDamagePlayerModule : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerCarcass>(out PlayerCarcass player))
        {
            player.hpModule.Damage(sbyte.MaxValue);
        }
    }
}
