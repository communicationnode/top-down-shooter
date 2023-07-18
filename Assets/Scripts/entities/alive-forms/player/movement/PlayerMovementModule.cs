//script icon image: back-dark.png;

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ExampleThirdPersonShooter.Player.Modules
{
    /// <summary> »грок разбит на элементы. Ёто элемент перемещени€ <br/>
    /// √лавный класс игрока -> <see cref="PlayerCarcass"/></summary>
    
    [AddComponentMenu("Player/Module/Movement Module")]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerMovementModule : MonoBehaviour
    {
        #region alterable values
        private readonly float  defaultSpeed = 4f;
        private float           directionX = 0;
        private float           directionZ = 0;
        private Rigidbody       rigbody;

        [SerializeField] private List<SpeedModifier> modifiers = new List<SpeedModifier>(4);
        [System.Serializable]
        public sealed class SpeedModifier
        {
            public string           tag;

            public readonly float   multiplier;
            public ushort           lifetime;
            public SpeedModifier    (in float _multiplier, in ushort _lifetime, in string _tag = "none")
            {
                multiplier = _multiplier;
                lifetime = _lifetime;
                tag = _tag;
            }
            public bool IsAlive     () => lifetime > 0 ? true : false;
        }
        #endregion


        #region methods
        private void                Start                           ()
        {
            rigbody = GetComponent<Rigidbody>();
        }
        private void                FixedUpdate                     ()
        {
            HandleDirection(InputConstants.UP_MOVE, InputConstants.DOWN_MOVE, ref directionZ);
            HandleDirection(InputConstants.RIGHT_MOVE, InputConstants.LEFT_MOVE, ref directionX);

            CreateAndAppedFinalDirection();

            ClearOldModifiers();
        }
        private void                HandleDirection                 (in KeyCode _value, in KeyCode _analog, ref float _direction)
        {
            // формирование направлени€ через клавишу или ее аналог. если ничего не нажато, ничего не формируетс€
            if (Input.GetKey(_value))
            {
                _direction += (defaultSpeed - _direction) * 0.1f;
            }
            else if (Input.GetKey(_analog))
            {
                _direction += (-defaultSpeed - _direction) * 0.1f;
            }
            else
            {
                _direction += (0 - _direction) * 0.1f;
            }
        }
        private void                CreateAndAppedFinalDirection    ()
        {
            // do magic. +здесь происходит умножение всех модов на пред-финальное значение направлени€ движени€
            float finalMultiplier = 1;

            for (ushort i = 0; i < modifiers.Count; i++)
            {
                finalMultiplier *= modifiers[i].multiplier;
            }

            // формирование вектора движени€
            Vector3 speed = new Vector3(directionX * finalMultiplier, rigbody.velocity.y, directionZ * finalMultiplier);

            // идти
            rigbody.velocity = speed;
        }
        public  void                AddModifier                     (in float _modifier, in ushort _lifetime, in string _tag = "none")
        {
            // создать новый экземпл€р мода
            SpeedModifier mod = new SpeedModifier(_modifier, _lifetime, _tag);

            // запустить его процесс старени€
            Task.Run(async () =>
            {
                try
                {
                    while (mod.lifetime > 0)
                    {
                        mod.lifetime--;
                        await Task.Delay(1);
                    }
                }
                catch (System.Exception exception)
                {
                    Debug.Log(exception.Message);
                }
            });

            // добавить в список модов
            modifiers.Add(mod);

        }
        public  SpeedModifier[]     GetModifiers                    () => modifiers.ToArray();
        public  void                ClearOldModifiers               ()
        {
            // стирать старые модификаторы с истекщим сроком годности
            for (short slot = (short)(modifiers.Count - 1); slot >= 0; slot--)
            {
                if (!modifiers[slot].IsAlive())
                {
                    modifiers.RemoveAt(slot);
                }
            }
        }
        #endregion
    }
}