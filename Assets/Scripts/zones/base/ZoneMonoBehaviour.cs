using UnityEngine;

public class ZoneMonoBehaviour : MonoBehaviour
{
    /// <summary>
    /// Ёто поле используетс€ в классе <see cref="ExampleThirdPersonShooter.Zones.ZoneInitializerAtStart"/>. <br/>
    /// ¬ ходе генерации зон в начале игры они могут задеть игрока и внести нежелательные изменени€ в геймплей. <br/>
    /// -----------------------------------------------------------------------------------------------------------------<br/>
    /// ¬ышеописанный компонент сам автоматически активирует зоны, если генераци€ будет признана успешной.
    /// </summary>
    [HideInInspector] public bool activated = false;
    
}
