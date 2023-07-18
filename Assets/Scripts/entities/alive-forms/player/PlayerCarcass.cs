//script icon image: ... TrackAvatarButton ... .png;

using UnityEngine;
using ExampleThirdPersonShooter.Player.Modules;
using RC = UnityEngine.RequireComponent;

namespace ExampleThirdPersonShooter.Player
{
    [AddComponentMenu("Player/Player Carcass")]
    [RC(typeof(PlayerMovementModule)), RC(typeof(PlayerRotationModule)), RC(typeof(PlayerWeaponsInventoryModule)), RC(typeof(PlayerModifiersHandlerModule))]
    [RC(typeof(HPModule))]
    public sealed class PlayerCarcass : MonoBehaviour
    {
        #region alterable values
        // Статус жизни игрока + Предсмертный делегат
        public static bool isDead = false;

        // Модули игрока. 
        public PlayerMovementModule         movementModule;
        public PlayerRotationModule         rotationModule;
        public PlayerWeaponsInventoryModule weaponsModule;
        public PlayerModifiersHandlerModule modifiersModule;

        public HPModule hpModule;
        #endregion


        #region methods
        private void Start      ()
        {
            hpModule.OnDeath += () =>
            {
                isDead = true;
            };
        }
        public  void KillPlayer (in bool force = false)
        {
            switch (force)
            {
                case true:
                    hpModule.ForceDamage(sbyte.MaxValue);
                    break;
                case false:
                    hpModule.Damage(sbyte.MaxValue);
                    break;
            }
        }
        private void OnDestroy  ()
        {
            KillPlayer();
        }
        #endregion
    }
}

