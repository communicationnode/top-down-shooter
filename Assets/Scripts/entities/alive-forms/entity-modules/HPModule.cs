using UnityEngine;

[AddComponentMenu("Universal Entity Modules/HP Module")]
public sealed class HPModule : MonoBehaviour
{
    #region alterable values
    public System.Action OnDeath;
    [Header("Модуль здоровья. При значении меньше нуля модуль убивает gameObject")]

    [SerializeField] private sbyte   _heath  = 10;
    public  sbyte   health  { get => _heath; set { _heath = value; CheckDie(); } }

    [SerializeField] private GameObject objectAfterDeath;

    public bool lockNonForcedDamage = false;
    #endregion

    #region methods
    private void CheckDie   ()
    {
        if (_heath <= 0)
        {
            if (OnDeath != null)
            {
                OnDeath();
            }
            if (objectAfterDeath != null)
            {
                GameObject instance = Instantiate(objectAfterDeath, transform.position, transform.rotation);
            }
            Destroy(gameObject);
            return;
        }
    }
    public  void Damage     (in sbyte _value)
    {
        if (lockNonForcedDamage) return;
        health -= _value;
    }
    public  void ForceDamage (in sbyte _value)
    {      
        health -= _value;
    }
    #endregion
}
