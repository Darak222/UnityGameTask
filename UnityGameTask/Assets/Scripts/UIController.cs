using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Playable Characters")]
    [SerializeField] List<GameObject> charactersList;

    [Header("Character Selection Buttons")]
    [SerializeField] GameObject[] characterSelectionButtons;

    Camera mainCamera;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for(int i = 0; i < characterSelectionButtons.Length; i++)
        {
            if (charactersList[i].tag == "Player")
            {
                Button buttonStarterState = characterSelectionButtons[i].GetComponent<Button>();
                buttonStarterState.interactable = false;
            }
        }
    }

    public void ChoseCharacter(int index)
    {
        SetNewButtonState(index);
        SetNewCharacterTarget(index);
        SetNewCameraTarget(index);
    }

    private void SetNewButtonState(int index)
    {
        for (int i = 0; i < characterSelectionButtons.Length; i++)
        {
            Button buttonNewState = characterSelectionButtons[i].GetComponent<Button>();
            if (i == index)
            {
                buttonNewState.interactable = false;
            }
            else
            {
                buttonNewState.interactable = true;
            }
        }
    }

    private void SetNewCharacterTarget(int index)
    {
        for (int i = 0; i < charactersList.Count; i++)
        {
            CharacterMovement character = charactersList[i].GetComponent<CharacterMovement>();
            if (i == index)
            {
                character.isChosen = true;
                character.GetComponent<NavMeshAgent>().isStopped = false;
                character.GetComponent<NavMeshAgent>().destination = character.transform.position;
                character.tag = "Player";
            }
            else
            {
                character.isChosen = false;
                character.tag = "Follower";
                character.playerToFollow = charactersList[index];
                character.GetComponent<NavMeshAgent>().destination = character.transform.position;
                Debug.Log(character.playerToFollow);
            }
        }
    }

    private void SetNewCameraTarget(int index)
    {
        CameraMovement newTarget = mainCamera.GetComponent<CameraMovement>();
        newTarget.target = charactersList[index].transform;
        Debug.Log(newTarget.target.transform.position);
    }
}
