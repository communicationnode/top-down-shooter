using TMPro;
using UnityEngine;

public sealed class ReadLastRecord : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        if (!PlayerPrefs.HasKey("record") || PlayerPrefs.GetInt("record") <= 0)
        {
            textMesh.text = "Run & Shoot";
        }
        else
        {
            textMesh.text = $"Your last record: {PlayerPrefs.GetInt("record")}";
        }
    }
}
