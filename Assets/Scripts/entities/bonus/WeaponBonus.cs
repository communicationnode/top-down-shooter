using UnityEngine;
using ExampleThirdPersonShooter.Player;

public sealed class WeaponBonus : BonusMonoBehaviour
{
    [SerializeField] private WeaponProperties properties;
    public WeaponProperties Properties { get => properties; }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<PlayerCarcass>(out PlayerCarcass player))
        {
            Destroy(gameObject);
            player.weaponsModule.GetWeapon(properties);
        }
    }
}

