using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private DummyTarget currentTarget;

    [Header("Basic Attacks (No Cooldown)")]
    [SerializeField] private int lightAttackDamage = 8;
    [SerializeField] private int heavyAttackDamage = 14;

    [Header("Fight Kits (2 total)")]
    [SerializeField] private FightKit kit1;
    [SerializeField] private FightKit kit2;

    [Header("Keyboard Test Keys")]
    [SerializeField] private KeyCode lightAttackKey = KeyCode.J;
    [SerializeField] private KeyCode heavyAttackKey = KeyCode.K;
    [SerializeField] private KeyCode ability1Key = KeyCode.U;
    [SerializeField] private KeyCode ability2Key = KeyCode.I;
    [SerializeField] private KeyCode ability3Key = KeyCode.O;
    [SerializeField] private KeyCode ability4Key = KeyCode.P;
    [SerializeField] private KeyCode switchKitKey = KeyCode.LeftShift;

    public int ActiveKitIndex { get; private set; }

    private void Awake()
    {
        InitializeKits();
        ActiveKitIndex = 0;
    }

    private void Update()
    {
        TickCooldowns(Time.deltaTime);
        HandleKeyboardInput();
    }

    private void InitializeKits()
    {
        if (kit1 == null)
        {
            kit1 = new FightKit { kitName = "Kit 1" };
        }

        if (kit2 == null)
        {
            kit2 = new FightKit { kitName = "Kit 2" };
        }

        kit1.EnsureArrays();
        kit2.EnsureArrays();
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(lightAttackKey))
        {
            LightAttack();
        }

        if (Input.GetKeyDown(heavyAttackKey))
        {
            HeavyAttack();
        }

        if (Input.GetKeyDown(ability1Key))
        {
            UseAbility(0);
        }

        if (Input.GetKeyDown(ability2Key))
        {
            UseAbility(1);
        }

        if (Input.GetKeyDown(ability3Key))
        {
            UseAbility(2);
        }

        if (Input.GetKeyDown(ability4Key))
        {
            UseAbility(3);
        }

        if (Input.GetKeyDown(switchKitKey))
        {
            SwitchKit();
        }
    }

    private void TickCooldowns(float dt)
    {
        TickKitCooldowns(kit1, dt);
        TickKitCooldowns(kit2, dt);
    }

    private void TickKitCooldowns(FightKit kit, float dt)
    {
        for (int i = 0; i < kit.cooldownTimers.Length; i++)
        {
            if (kit.cooldownTimers[i] > 0f)
            {
                kit.cooldownTimers[i] -= dt;
                if (kit.cooldownTimers[i] < 0f)
                {
                    kit.cooldownTimers[i] = 0f;
                }
            }
        }
    }

    public void LightAttack()
    {
        Debug.Log("Light Attack used (no cooldown).");
        DealDamage(lightAttackDamage);
    }

    public void HeavyAttack()
    {
        Debug.Log("Heavy Attack used (no cooldown).");
        DealDamage(heavyAttackDamage);
    }

    public void UseAbility(int abilitySlot)
    {
        FightKit activeKit = GetActiveKit();

        if (abilitySlot < 0 || abilitySlot >= 4)
        {
            Debug.LogWarning("Ability slot is out of range.");
            return;
        }

        AbilityData ability = activeKit.abilities[abilitySlot];

        if (ability == null)
        {
            Debug.LogWarning($"Ability slot {abilitySlot + 1} in {activeKit.kitName} has no ability assigned.");
            return;
        }

        if (activeKit.cooldownTimers[abilitySlot] > 0f)
        {
            Debug.Log($"{ability.abilityName} is on cooldown: {activeKit.cooldownTimers[abilitySlot]:0.0}s");
            return;
        }

        Debug.Log($"Used {ability.abilityName} from {activeKit.kitName}.");
        DealDamage(ability.damage);
        activeKit.cooldownTimers[abilitySlot] = Mathf.Max(0f, ability.cooldownSeconds);
    }

    public void SwitchKit()
    {
        ActiveKitIndex = 1 - ActiveKitIndex;
        Debug.Log($"Switched to {GetActiveKit().kitName}");
    }

    public float GetCooldownForActiveKitSlot(int abilitySlot)
    {
        FightKit activeKit = GetActiveKit();

        if (abilitySlot < 0 || abilitySlot >= 4)
        {
            return 0f;
        }

        return activeKit.cooldownTimers[abilitySlot];
    }

    public string GetAbilityNameForActiveKitSlot(int abilitySlot)
    {
        FightKit activeKit = GetActiveKit();

        if (abilitySlot < 0 || abilitySlot >= 4)
        {
            return string.Empty;
        }

        AbilityData ability = activeKit.abilities[abilitySlot];
        return ability != null ? ability.abilityName : string.Empty;
    }

    private FightKit GetActiveKit()
    {
        return ActiveKitIndex == 0 ? kit1 : kit2;
    }

    private void DealDamage(int damage)
    {
        if (currentTarget == null)
        {
            return;
        }

        currentTarget.TakeDamage(damage);
    }
}
