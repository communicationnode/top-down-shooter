using UnityEngine;
using UnityEngine.AI;

using ExampleThirdPersonShooter.Player;

[RequireComponent(typeof(NavMeshAgent))]
[AddComponentMenu("Universal Entity Modules/Follow To Player Module")]
public sealed class EntityFollowToPlayerModule : MonoBehaviour
{
    public static PlayerCarcass player;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        //вызовется один раз на сцене и не будет повторяться в случаях:
        // 1. игрок уже найден;
        // 2. игрок уже мертв;
        if (player is null && !PlayerCarcass.isDead)
        {
            player = FindObjectOfType<PlayerCarcass>();
        }
    }
    private void Update()
    {
        if (PlayerCarcass.isDead)
        {
            return;
        }
        if (player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}
