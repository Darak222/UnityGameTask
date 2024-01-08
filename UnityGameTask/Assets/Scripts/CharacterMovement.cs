using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterConfigSO characterConfig;
    [SerializeField] bool isChosen = false;

    float exhaustionRate;
    float staminaRegenerationRate;

    float stamina;
    float initialStamina;
    bool isExhausted = false;

    private NavMeshAgent agent;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed += characterConfig.GenerateSpeedValue();
        agent.angularSpeed += characterConfig.GenerateAngularSpeedValue();
        stamina = characterConfig.GenerateStaminaValue();
        initialStamina = stamina;
        exhaustionRate = characterConfig.GetExhaustionRate();
        staminaRegenerationRate = characterConfig.GetStaminaRegenerationRate();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToPoint();
            Debug.Log(stamina);
        }

        if (agent.velocity != Vector3.zero)
        {
            DepleteStamina();
        }
        else
        {
            RegenerateStamina();
        }


    }

    private void MoveToPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    private void DepleteStamina()
    {
        stamina -= exhaustionRate * Time.deltaTime;
        if (stamina <= 0)
        {
            stamina = 0;
            agent.isStopped = true;
            isExhausted = true;
        }
    }

    public void RegenerateStamina()
    {
        if (isExhausted && initialStamina != stamina)
        {
            stamina += staminaRegenerationRate * 2 * Time.deltaTime;
            if (stamina >= initialStamina)
            {
                stamina = initialStamina;
                isExhausted = false;
                agent.isStopped = false;
            }
        }
        else
        {
            stamina += staminaRegenerationRate * Time.deltaTime;
            if (stamina >= initialStamina)
            {
                stamina = initialStamina;
            }
        }
    }
}
