using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float minSpeedRange = 0f;
    [SerializeField] float maxSpeedRange = 10f;

    [SerializeField] float minAngularSpeedRange = -30f;
    [SerializeField] float maxAngularSpeedRange = 100f;

    [SerializeField] float minStaminaRange = 10f;
    [SerializeField] float maxStaminaRange = 100f;

    [SerializeField] float exhaustionRate = 1.5f;
    [SerializeField] float staminaRegenerationRate = 2f;

    float stamina;
    float initialStamina;
    bool isExhausted = false;

    private NavMeshAgent agent;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed += Random.Range(minSpeedRange, maxSpeedRange);
        agent.angularSpeed += Random.Range(minAngularSpeedRange, maxAngularSpeedRange);
        stamina = Random.Range(minStaminaRange, maxStaminaRange);
        initialStamina = stamina;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToPoint();
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
