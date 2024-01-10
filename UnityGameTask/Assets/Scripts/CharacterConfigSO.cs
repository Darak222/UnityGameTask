using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Character Config", fileName = "New Character Config", order = 1)]
public class CharacterConfigSO : ScriptableObject
{
    [SerializeField] float minSpeedRange = 0f;
    [SerializeField] float maxSpeedRange = 10f;

    [SerializeField] float minAngularSpeedRange = -30f;
    [SerializeField] float maxAngularSpeedRange = 100f;

    [SerializeField] float minStaminaRange = 10f;
    [SerializeField] float maxStaminaRange = 100f;

    [SerializeField] float exhaustionRate = 1.5f;
    [SerializeField] float staminaRegenerationRate = 2f;

    public float GenerateSpeedValue()
    {
        return Random.Range(minSpeedRange, maxSpeedRange);
    }

    public float GenerateAngularSpeedValue()
    {
        return Random.Range(minAngularSpeedRange, maxAngularSpeedRange);
    }

    public float GenerateStaminaValue()
    {
        return Random.Range(minStaminaRange, maxStaminaRange);
    }

    public float GetExhaustionRate()
    {
        return exhaustionRate;
    }

    public float GetStaminaRegenerationRate()
    {
        return staminaRegenerationRate;
    }

}
