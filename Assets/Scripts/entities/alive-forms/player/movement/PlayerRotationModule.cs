//script icon image: back-dark.png;
using UnityEngine;

namespace ExampleThirdPersonShooter.Player.Modules
{
    /// <summary> Игрок разбит на элементы. Это элемент наблюдения за курсором <br/>
    /// Главный класс игрока -> <see cref="PlayerCarcass"/></summary>
    
    [AddComponentMenu("Player/Module/Rotation Module")]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerRotationModule : MonoBehaviour
    {
        #region alterable value
        public enum RotateMode  { LINEAR, SLERP }
        public      RotateMode  mode = RotateMode.LINEAR;

        private Vector3     direction;
        private Quaternion  toTargetQuaternion;
        private Rigidbody   rigbody;

        public float        rotationSpeed = 2.25f;

        private Vector3  _lastMousePointer;
        public Vector3 lastRaycastPointer { get => _lastMousePointer; }
        #endregion


        #region methods
        private void Start  ()
        {
            rigbody = GetComponent<Rigidbody>();
            direction = Vector3.forward;
        }
        private void Update ()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitData))
            {
                _lastMousePointer = hitData.point;
                Debug.DrawLine(transform.position, _lastMousePointer);
            }

            if (Input.GetMouseButton(InputConstants.SHOOT_MOUSE) || Input.GetMouseButton(InputConstants.ALT_MOUSE))
            {
                direction = (hitData.point - transform.position).normalized;
                direction = new Vector3(direction.x, 0, direction.z);
                SlerpIt();
            }
            else
            {
                if (rigbody.velocity.magnitude > 1)
                {
                    direction = new Vector3(rigbody.velocity.x, 0, rigbody.velocity.z);
                    SlerpIt();
                }
            }
        }
        private void SlerpIt()
        {
            toTargetQuaternion = Quaternion.LookRotation(direction);
            switch (mode)
            {
                case RotateMode.LINEAR:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetQuaternion, rotationSpeed);
                    break;
                case RotateMode.SLERP:
                    transform.rotation = Quaternion.Slerp(transform.rotation, toTargetQuaternion, rotationSpeed * 0.015f);
                    break;
                default:
                    return;
            }
        }
        #endregion
    }
}
