using UnityEngine;
using ExampleThirdPersonShooter.Player;
using System.Threading.Tasks;

public class BonusSpawnerSystemBase : MonoBehaviour
{
    #region alterable values
    protected PlayerCarcass player;

    [SerializeField] protected GameObject[]   bonuses;
    [SerializeField] protected ushort         interval        = 1000;
    [SerializeField] protected float          bonusLifetime   = 10;

    //бонусы не должны спавниться за пределами этих границ
    [SerializeField] private Vector3        sceneBounds = new Vector3(40,0,30);

    // посредник между async таском и главным потоком в Update
    private bool elapsed = false;

    protected Vector3 spawnPoint;
    #endregion


    #region methods
    private         void Start                  ()
    {
        // найти игрока, около которого будут появляться бонусы
        player = FindObjectOfType<PlayerCarcass>();

        // запустить миллисекундный таймер
        Task.Run(StartInterval);
    }
    private         void Update                 ()
    {
        if (player is null || PlayerCarcass.isDead) return;
        if (elapsed)
        {
            elapsed = false;

            spawnPoint = new Vector3(
                Mathf.Clamp(player.transform.position.x + Random.Range(-10f, 10f), -(sceneBounds.x / 2), (sceneBounds.x / 2)),
                0,
                Mathf.Clamp(player.transform.position.z + Random.Range(-10f, 10f), -(sceneBounds.z / 2), (sceneBounds.z / 2)));
            GenerateBonusInstance();
        }
    }
    public virtual  void GenerateBonusInstance  ()
    {
        GameObject instance = Instantiate(bonuses[Random.Range(0, bonuses.Length)], spawnPoint, Quaternion.identity);

        Destroy(instance, bonusLifetime);
    }
    private async   Task StartInterval          ()
    {
        try
        {
            ushort bufferedMaxInterval = interval;

            while (true)
            {
                interval--;
                if (interval <= 0)
                {
                    interval = bufferedMaxInterval;
                    elapsed = true;
                }
                await Task.Delay(1);
            }
        }
        catch
        {
            return;
        }
    }
    #endregion
}
