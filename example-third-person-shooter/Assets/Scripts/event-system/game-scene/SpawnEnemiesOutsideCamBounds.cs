using UnityEngine;
using System.Threading.Tasks;

public sealed class SpawnEnemiesOutsideCamBounds : MonoBehaviour
{
    #region alterable values
    //----------------------------------------- монстры
    [SerializeField]    private Enemy[]         enemies;

    //----------------------------------------- параметры async таймера
    [SerializeField]    private ushort          charge      = 0;
    [SerializeField]    private ushort          interval    = 2000;
                        private const ushort    SUBSTRACT   = 100;

    //----------------------------------------- переходник между async таском и главным потоком Update
                        private bool            elapsed     = false;

    //----------------------------------------- камера, на основе которой читаются границы
                        private new Camera      camera;
    #endregion


    #region methods
    private         void        Start           ()
    {
        camera = Camera.main;
        Task.Run(StartLoop);
    }
    private async   Task        StartLoop       ()
    {
        try
        {
            ushort bufferedInterval = interval;

            while (true)
            {
                charge++;
                interval--;

                if (interval <= 0)
                {
                    elapsed = true;
                    interval = bufferedInterval;
                }

                if (charge >= 10000)
                {       
                    charge  = 0;

                    if (bufferedInterval > 500) bufferedInterval -= SUBSTRACT;
                }
                await Task.Delay(1);
            }
        }
        catch
        {
            return;
        }
    }
    private         void        Update          ()
    {
        if (elapsed)
        {
            elapsed = false;
            TrySpawn();
        }
    }
    private         void        TrySpawn        ()
    {
        // 0 = up | 1 = left | 2 = down | 3 = right.
        byte spawnBoundMode = (byte)Random.Range(0, 4);

        // тут нужно получить точку для спавна за границами камеры.
        RaycastHit spawnRaycastHit = RaycastHitGet(spawnBoundMode);

        // тут спавнится монстр с определенным шансом + обрезать точку спавна до границ карты
        SpawnEnemy(new Vector3(Mathf.Clamp(spawnRaycastHit.point.x,-20,20), spawnRaycastHit.point.y,Mathf.Clamp(spawnRaycastHit.point.z,-15,15)));
    }
    private         RaycastHit  RaycastHitGet   (in byte mode)
    {
        Vector3 notFormattedPoint = new Vector3(0,0,0);
        switch (mode)
        {
            case 0: // точка на верхних границах камеры
                notFormattedPoint = camera.ScreenToWorldPoint(new Vector3(Random.Range(0,camera.pixelWidth), camera.pixelHeight + 1, 0));
                break;
            case 1: // точка на левых границах камеры
                notFormattedPoint = camera.ScreenToWorldPoint(new Vector3(-1, Random.Range(0, camera.pixelHeight), 0));
                break;
            case 2: // точка на нижних границах камеры
                notFormattedPoint = camera.ScreenToWorldPoint(new Vector3(Random.Range(0, camera.pixelWidth), -1, 0));
                break;
            case 3: // точка на правых границах камеры
                notFormattedPoint = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth+1, Random.Range(0, camera.pixelHeight), 0));
                break;
        }

        // пустить луч на точку
        Physics.Raycast(notFormattedPoint, camera.transform.forward, out RaycastHit hit);
        return hit;
    }
    private         void        SpawnEnemy      (in Vector3 position)
    {
        // выбивание монстра, учитывая его шанс
        for (byte slot = 0; slot < enemies.Length; slot++)
        {
            if (enemies[slot].chance > Random.Range(0, 100))
            {
                GameObject instance = Instantiate(enemies[slot].prefab, position, Quaternion.identity);
                return;
            }
        }

        // если в рандоме ни один монстр небыл выбран, начать перевыбор
        SpawnEnemy(position);
        return;
    }
    #endregion


    [System.Serializable]
    public sealed class Enemy
    {
        public GameObject   prefab;
        public byte         chance;
    }
}
