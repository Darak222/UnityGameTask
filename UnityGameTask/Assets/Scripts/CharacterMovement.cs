using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterConfigSO characterConfig;

    [SerializeField] public bool isChosen = false;
    [SerializeField] public GameObject playerToFollow;

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
        if (isChosen){
            if (Input.GetMouseButton(0))
            {
                MoveToPoint();
            }

            if (agent.velocity != Vector3.zero && agent.destination != agent.transform.position)
            {
                DepleteStamina();
            }
            else
            {
                RegenerateStamina();
            }
        }
        else
        {
            FollowPlayer();
        }

    }

    private void MoveToPoint()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

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

    public void FollowPlayer()
    {
        if (stamina != initialStamina)
        {
            RegenerateStamina();
        }
        agent.destination = playerToFollow.transform.position;
    }

    public int GetSpeedValue()
    {
        return (int)agent.speed;
    }

    public int GetAngularSpeedValue()
    {
        return (int)agent.angularSpeed;
    }

    public int GetInitialStaminaValue()
    {
        return (int)initialStamina;
    }

    public int GetCurrentStaminaValue()
    {
        return (int)stamina;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Follower" && isChosen)
        {
            other.GetComponent<NavMeshAgent>().destination = other.transform.position;
            other.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Follower" && isChosen)
        {
            other.GetComponent<NavMeshAgent>().destination = playerToFollow.transform.position;
            other.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Follower" && isChosen)
        {
            other.GetComponent<NavMeshAgent>().destination = other.transform.position;
            other.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

}
