using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Playable Characters")]
    [SerializeField] List<GameObject> charactersList;

    [Header("Selection Buttons")]
    [SerializeField] GameObject[] characterSelectionButtons;

    [Header("Character Statistics")]
    [SerializeField] TextMeshProUGUI speedInfo;
    [SerializeField] TextMeshProUGUI angularSpeedInfo;
    [SerializeField] TextMeshProUGUI staminaTextInfo;

    [Header("Stamina Slider")]
    [SerializeField] Slider staminaSlider;

    Camera mainCamera;
    CharacterMovement chosenCharacter;

    //Initialize camera, buttons and player character on start
    void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        InitializeButtons();
        GetChosenCharacter();
    }

    void Start()
    {
        SetStaminaBarMaxValue();
    }

    void Update()
    {
        DisplayStats();
    }

    //Button initialization
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

    //Player character getter
    private void GetChosenCharacter()
    {
        for(int i = 0; i < charactersList.Count; i++)
        {
            CharacterMovement character = charactersList[i].GetComponent<CharacterMovement>();
            if (character.isChosen)
            {
                chosenCharacter = character;
                break;
            }
        }
    }

    //Player changing character
    public void ChoseCharacter(int index)
    {
        SetNewButtonState(index);
        SetNewCharacterTarget(index);
        SetNewCameraTarget(index);
        GetChosenCharacter();
        SetStaminaBarMaxValue();
    }

    //Pause menu methods
    public void PauseMenu()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    //Change character methods
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
                character.playerToFollow = charactersList[index];
                character.GetComponent<NavMeshAgent>().destination = charactersList[index].transform.position;
                character.tag = "Follower";
            }
        }
    }

    private void SetNewCameraTarget(int index)
    {
        CameraMovement newTarget = mainCamera.GetComponent<CameraMovement>();
        newTarget.target = charactersList[index].transform;
    }

    private void SetStaminaBarMaxValue()
    {
        staminaSlider.maxValue = chosenCharacter.GetInitialStaminaValue();
    }

    public void DisplayStats()
    {
        speedInfo.text = "Speed: " + chosenCharacter.GetSpeedValue();
        angularSpeedInfo.text = "Angular Speed: " + chosenCharacter.GetAngularSpeedValue();
        staminaTextInfo.text = chosenCharacter.GetCurrentStaminaValue() + "/" + chosenCharacter.GetInitialStaminaValue();
        staminaSlider.value = chosenCharacter.GetCurrentStaminaValue();
    }
}
