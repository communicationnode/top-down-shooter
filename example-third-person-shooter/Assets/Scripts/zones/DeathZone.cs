//script icon image: ... iconchecked ... .png;

using UnityEngine;
using ExampleThirdPersonShooter.Player;

namespace ExampleThirdPersonShooter.Zones
{

    [AddComponentMenu("Zones/Death Zone")]

    public sealed class DeathZone : ZoneMonoBehaviour
    {
        #region methods
        private void OnTriggerStay(Collider other)
        {
            if (activated is false) return;

            if (other.TryGetComponent<PlayerCarcass>(out PlayerCarcass player))
            {
                player.KillPlayer(true);
            }
        }
        #endregion
    }
}