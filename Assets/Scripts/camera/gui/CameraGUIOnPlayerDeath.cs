using TMPro;
using UnityEngine.UI;
using UnityEngine;
using ExampleThirdPersonShooter.Player;

[DisallowMultipleComponent]
public sealed class CameraGUIOnPlayerDeath : MonoBehaviour
{
    #region alterable values
    [Header("Window:")]
    [SerializeField] private GameObject deathWindow;

    [Header("Parameters")]
    [SerializeField] private TextMeshProUGUI    textScores;
    [SerializeField] private Button             buttonRestartSession;
    [SerializeField] private Button             buttonGoInMenu;
    #endregion

    private void Start()
    {
        FindObjectOfType<PlayerCarcass>().hpModule.OnDeath += () => 
        {
            deathWindow.SetActive(true);

            textScores.text = $"Score:{ScoreModule.sessionScore}";

            if (ScoreModule.sessionScore > PlayerPrefs.GetInt("record"))
            {
                textScores.text += $" | Record !!!";

                PlayerPrefs.SetInt("record", ScoreModule.sessionScore);
            }
            textScores.text += $"\n Your past scores: { PlayerPrefs.GetInt("record")}";
        };

        buttonGoInMenu.onClick.AddListener(()           => { UnityEngine.SceneManagement.SceneManager.LoadScene("main-menu"); });

        buttonRestartSession.onClick.AddListener(()     => { UnityEngine.SceneManagement.SceneManager.LoadScene("game-scene"); });
    }


}
