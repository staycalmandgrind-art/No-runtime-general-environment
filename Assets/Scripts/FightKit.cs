using UnityEngine;

[System.Serializable]
public class FightKit
{
    public string kitName = "Kit";
    public AbilityData[] abilities = new AbilityData[4];

    [HideInInspector] public float[] cooldownTimers = new float[4];

    public void EnsureArrays()
    {
        if (abilities == null || abilities.Length != 4)
        {
            AbilityData[] rebuilt = new AbilityData[4];

            if (abilities != null)
            {
                for (int i = 0; i < Mathf.Min(abilities.Length, rebuilt.Length); i++)
                {
                    rebuilt[i] = abilities[i];
                }
            }

            for (int i = 0; i < rebuilt.Length; i++)
            {
                if (rebuilt[i] == null)
                {
                    rebuilt[i] = new AbilityData();
                }
            }

            abilities = rebuilt;
        }

        if (cooldownTimers == null || cooldownTimers.Length != 4)
        {
            cooldownTimers = new float[4];
        }
    }
}
