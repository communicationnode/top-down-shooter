using UnityEngine;
using ExampleThirdPersonShooter.Player;
public sealed class CameraToPlayerModule : MonoBehaviour
{
    #region alterable values
    [Header("Модуль ищет объект с этим компонентом и следует за ним")]
    [SerializeField] private PlayerCarcass  playerCarcass;
    [Header("Позиция камеры относительно найденного игрока")]
    [SerializeField] private Vector3        cameraOffset = new Vector3(0,15.98f,-9.31f);
    
  
    private new Camera camera;
    [SerializeField] private Vector3 bounds = new Vector3(40,0,30);
        #endregion
    private void OnEnable()
    {
        camera = GetComponent<Camera>();
        playerCarcass = FindObjectOfType<PlayerCarcass>();
    }
    private void FixedUpdate()
    {
        if (PlayerCarcass.isDead)
        {
            return;
        }

        transform.position += ((playerCarcass.gameObject.transform.position + cameraOffset)-transform.position) * 0.1f;

        if (transform.position.x < -(bounds.x / 2)) { transform.position = new Vector3(-(bounds.x / 2), transform.position.y, transform.position.z); }
        if (transform.position.x > (bounds.x / 2))  { transform.position = new Vector3((bounds.x / 2), transform.position.y, transform.position.z); }
        if (transform.position.z < -(bounds.z / 2)) { transform.position = new Vector3(transform.position.x, transform.position.y, -(bounds.z / 2)); }
        if (transform.position.z > (bounds.z / 2))  { transform.position = new Vector3(transform.position.x, transform.position.y, (bounds.z / 2)); }
    }
}
