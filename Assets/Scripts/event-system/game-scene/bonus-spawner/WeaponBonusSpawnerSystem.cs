using UnityEngine;

public sealed class WeaponBonusSpawnerSystem : BonusSpawnerSystemBase
{    
    public override void GenerateBonusInstance()
    {
        byte randomPoint = (byte)Random.Range(0, bonuses.Length);

        //оружий не должно быть больше 9 в кармане
        if (player.weaponsModule.weaponItems.Length > 8)
        {
            return;
        }

        //запустится проверка, если:
        // 1. игрок выбрал оружие от 0 до 8.
        // 2. у игрока вообще есть оружие в инвентаре
        // 3. игрок в руке держит оружие в диапазоне кол-ва инвентарного оружия
        if (player.weaponsModule.currentItem < 9 && player.weaponsModule.weaponItems.Length > 0 && player.weaponsModule.currentItem < player.weaponsModule.weaponItems.Length)
        {
            //если у игрока в руке стоит оружие, которое вот вот появится на сцене, произойдет перегенерация на другое случайное оружие
            if (bonuses[randomPoint].GetComponent<WeaponBonus>().Properties.weapon.name == player.weaponsModule.weaponItems[player.weaponsModule.currentItem].weapon.name)
            {
                GenerateBonusInstance();
                return;
            }
        }


        GameObject instance = Instantiate(bonuses[randomPoint], spawnPoint, Quaternion.identity);

        Destroy(instance, bonusLifetime);
    }
}
