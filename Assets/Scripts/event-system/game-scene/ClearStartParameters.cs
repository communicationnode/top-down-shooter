using ExampleThirdPersonShooter.Player;
using UnityEngine;

public sealed class ClearStartParameters : MonoBehaviour
{
    private void Start()
    {
        PlayerCarcass.isDead                = false;
        EntityFollowToPlayerModule.player   = null;
        ScoreModule.sessionScore            = 0;
    }
}
