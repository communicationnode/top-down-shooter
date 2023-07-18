using TMPro;
using UnityEngine;
using ExampleThirdPersonShooter.Player;
public sealed class CameraGUIPlayerInventory : MonoBehaviour
{
    #region alterable values
    private const string CLR_S = "<color=#00D5FF>";
    private const string CLR_E = "</color>";

    [SerializeField] TextMeshProUGUI    textMesh;
    [SerializeField] PlayerCarcass      player;
    #endregion

    #region methods
    private void Start  ()
    {
        textMesh    = GetComponent<TextMeshProUGUI>();
        player      = FindObjectOfType<PlayerCarcass>();
    }
    private void Update ()
    {
        if (!PlayerCarcass.isDead)
        {
            textMesh.text = "";

            for (byte slot = 0; slot < player.weaponsModule.weaponItems.Length; slot++)
            {
                textMesh.text += slot == player.weaponsModule.currentItem ? "<color=yellow>--></color> " : "";
                textMesh.text += 
                    $"{player.weaponsModule.weaponItems[slot].weapon.name}|{CLR_S}ammo:{CLR_E} {player.weaponsModule.weaponItems[slot].ammo}|\n" +
                    $"{CLR_S}charge:{CLR_E}({player.weaponsModule.weaponItems[slot].currentInverval}/{player.weaponsModule.weaponItems[slot].interval})\n" +
                    $"----------------\n";
            }
        }
    }
    #endregion
}
