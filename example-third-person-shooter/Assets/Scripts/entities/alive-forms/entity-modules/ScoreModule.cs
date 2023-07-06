using UnityEngine;

[AddComponentMenu("Universal Entity Modules/Score Module")]
[RequireComponent(typeof(HPModule))]
[DisallowMultipleComponent]

public sealed class ScoreModule : MonoBehaviour
{
    public static ushort            sessionScore = 0;

    private void Start()
    {
        GetComponent<HPModule>().OnDeath += () => 
        { 
            sessionScore++;
            CameraGUIScores.instance.textMesh.text = $"SCORES: {ScoreModule.sessionScore}";
        };
    }
}
