using UnityEngine;

[DisallowMultipleComponent]
public sealed class LoadLevel : MonoBehaviour
{
    public void Load(string _name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_name);
    }
}
