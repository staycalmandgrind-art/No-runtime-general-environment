using UnityEngine;
using UnityEngine.UI;

public class MobileCombatUIBinder : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;

    [Header("Optional UI Text Labels")]
    [SerializeField] private Text kitNameText;
    [SerializeField] private Text[] abilityButtonTexts = new Text[4];

    public void SetMoveLeft(bool isPressed)
    {
        if (playerMovement == null)
        {
            return;
        }

        playerMovement.SetMoveInput(isPressed ? -1f : 0f);
    }

    public void SetMoveRight(bool isPressed)
    {
        if (playerMovement == null)
        {
            return;
        }

        playerMovement.SetMoveInput(isPressed ? 1f : 0f);
    }

    public void PressJump()
    {
        if (playerMovement == null)
        {
            return;
        }

        playerMovement.PressJump();
    }

    public void PressLightAttack()
    {
        if (playerCombat == null)
        {
            return;
        }

        playerCombat.LightAttack();
    }

    public void PressHeavyAttack()
    {
        if (playerCombat == null)
        {
            return;
        }

        playerCombat.HeavyAttack();
    }

    public void PressAbility1() => PressAbilityByIndex(0);
    public void PressAbility2() => PressAbilityByIndex(1);
    public void PressAbility3() => PressAbilityByIndex(2);
    public void PressAbility4() => PressAbilityByIndex(3);

    public void PressKitSwitch()
    {
        if (playerCombat == null)
        {
            return;
        }

        playerCombat.SwitchKit();
        RefreshAbilityLabels();
    }

    public void RefreshAbilityLabels()
    {
        if (playerCombat == null)
        {
            return;
        }

        if (kitNameText != null)
        {
            kitNameText.text = playerCombat.ActiveKitIndex == 0 ? "Kit 1" : "Kit 2";
        }

        for (int i = 0; i < abilityButtonTexts.Length; i++)
        {
            if (abilityButtonTexts[i] == null)
            {
                continue;
            }

            string abilityName = playerCombat.GetAbilityNameForActiveKitSlot(i);
            abilityButtonTexts[i].text = string.IsNullOrWhiteSpace(abilityName) ? $"Ability {i + 1}" : abilityName;
        }
    }

    private void PressAbilityByIndex(int index)
    {
        if (playerCombat == null)
        {
            return;
        }

        playerCombat.UseAbility(index);
    }
}
