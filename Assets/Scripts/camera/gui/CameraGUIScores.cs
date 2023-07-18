using TMPro;
using UnityEngine;

public sealed class CameraGUIScores : MonoBehaviour
{
    #region alterable values
    public static CameraGUIScores instance;
    public TextMeshProUGUI textMesh;
    #endregion

    #region methods
    private void Start()
    {
        instance = this;
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    #endregion
}
