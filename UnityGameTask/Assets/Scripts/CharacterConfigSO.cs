using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Character Config", fileName = "New Character Config", order = 1)]
public class CharacterConfigSO : ScriptableObject
{
    [Header("Speed statistic generation range")]
    [SerializeField] float minSpeedRange = 0f;
    [SerializeField] float maxSpeedRange = 10f;

    [Header("Angular speed statistic generation range")]
    [SerializeField] float minAngularSpeedRange = -30f;
    [SerializeField] float maxAngularSpeedRange = 100f;

    [Header("Stamina statistic generation range")]
    [SerializeField] float minStaminaRange = 10f;
    [SerializeField] float maxStaminaRange = 100f;

    [Header("Stamina depletion and regeneartion rate")]
    [SerializeField] float exhaustionRate = 1.5f;
    [SerializeField] float staminaRegenerationRate = 2f;

    //Methods generating statistics on start
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

    //Getters
    public float GetExhaustionRate()
    {
        return exhaustionRate;
    }

    public float GetStaminaRegenerationRate()
    {
        return staminaRegenerationRate;
    }

}
