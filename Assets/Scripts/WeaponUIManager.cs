using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    public PlayerAttack player;
    public Button meleeButton;
    public Button rangedButton;
    public Text currentWeaponText;

    void Start()
    {
        meleeButton.onClick.AddListener(() => SelectWeapon(PlayerAttack.AttackType.Melee));
        rangedButton.onClick.AddListener(() => SelectWeapon(PlayerAttack.AttackType.Ranged));
        UpdateUI();
    }

    void SelectWeapon(PlayerAttack.AttackType type)
    {
        player.SetAttackType(type);
        UpdateUI();
    }

    void UpdateUI()
    {
        currentWeaponText.text = "Weapon: " + player.attackType.ToString();
    }
}