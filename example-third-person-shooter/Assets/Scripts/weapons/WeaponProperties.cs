using UnityEngine;

/// <summary>
/// Класс параметров оружия. Описывает слот с параметрами. Обычно хранится в бонусах и игроке
/// </summary>

[System.Serializable]

public sealed class WeaponProperties
{
    #region alterable values
    [Header("Bullet Options:")]
    public GameObject   bullet;
    public byte         bulletPerShoot      = 1;
    public float        bulletLifeDistance  = 256;
    public short        ammo;

    [Header("Weapon Prefab:")]
    public GameObject   weapon;

    [Header("Shoot Interval:")]
    public ushort                       interval;
    [HideInInspector] public ushort     currentInverval;

    [Header("Additional:")]
    public bool     dieOnMousePoint = false;
    public Vector3  bufferedMousePoint;
    #endregion


    #region methods
    public  void Shoot                  (in Vector3 spawnPosition, in Quaternion direction, in GameObject owner)
    {
        // если выстрел дает одну пулю, она просто появляется и летит
        if (bulletPerShoot == 1)
        {
            GameObject instance = GameObject.Instantiate(bullet, spawnPosition, direction);

            AppendBulletOptions(instance, owner);
        }

        //если пуль при выстреле несколько -> do magic
        else
        {
            for (byte count = 0; count < bulletPerShoot; count++)
            {
                GameObject instance = GameObject.Instantiate(bullet, spawnPosition, direction);
                instance.transform.Rotate(new Vector3(0, (((10 * count) * -1)) + (10 * (bulletPerShoot - 1)) / 2, 0));

                AppendBulletOptions(instance, owner);
            }
        }
    }
    private void AppendBulletOptions    (in GameObject _instance, in GameObject _owner)
    {
        BulletMove bulletLogic          = _instance.GetComponent<BulletMove>();

        bulletLogic.bufferedMousePoint  = bufferedMousePoint;
        bulletLogic.dieOnMousePoint     = dieOnMousePoint;
        bulletLogic.bulletLifeDistance  = bulletLifeDistance;
        bulletLogic.owner               = _owner;
    }
    #endregion
}
