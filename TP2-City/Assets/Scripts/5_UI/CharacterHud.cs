using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class CharacterHUD : MonoBehaviour
{
    private InputAction switchCharacterLeft;
    private InputAction switchCharacterRight;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text characterState;
    [SerializeField] private Image avatarImage;
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private Scrollbar hungerBar;
    [SerializeField] private Scrollbar sleepinessBar;
    [SerializeField] private Scrollbar lonelinessBar;

    private Character[] characters;
    private int currentIndex = 0;
    private Character targetCharacter;
    private CharacterStateMachine stateMachine;
    private CharacterVitals vitals;

    private void Start()
    {
        characters = Object.FindAnyObjectByType<GameManager>().CityObjects.Characters;
        switchCharacterRight = InputSystem.actions.FindAction("NextCharacter");
        switchCharacterLeft = InputSystem.actions.FindAction("PreviousCharacter");

        SetTargetCharacter(characters[currentIndex]);
    }

    private void Update()
    {
        if (switchCharacterLeft.WasPerformedThisFrame())
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = characters.Length - 1;
            SetTargetCharacter(characters[currentIndex]);
        }
        else if (switchCharacterRight.WasPerformedThisFrame())
        {
            currentIndex++;
            if (currentIndex >= characters.Length) currentIndex = 0;
            SetTargetCharacter(characters[currentIndex]);
        }

        nameText.text = targetCharacter.FullName;
        avatarImage.sprite = targetCharacter.Avatar;
        if (stateMachine.TrashBehaviour == CityCharacterTrashBehaviour.PickUp)
        {
            stateText.text = "Propre";
        }
        else
            stateText.text = "Sale";

        hungerBar.size = Mathf.Clamp01(1f - vitals.Hunger / 100f);  //chat GPT pour l'affichage de la value du vitals dans la bar
        sleepinessBar.size = Mathf.Clamp01(1f - vitals.Sleepiness / 100f);
        lonelinessBar.size = Mathf.Clamp01(1f - vitals.Loneliness / 100f);

        characterState.text = stateMachine.CurrentStateName;
    }

    private void SetTargetCharacter(Character character)
    {
        foreach (var charac in characters)
        {
            var pointer = charac.transform.Find("Pointer");
            pointer.gameObject.SetActive(false);
        }

        targetCharacter = character;
        stateMachine = targetCharacter.GetComponent<CharacterStateMachine>();
        vitals = targetCharacter.GetComponent<CharacterVitals>();
        var targetPointer = targetCharacter.transform.Find("Pointer");
        targetPointer.gameObject.SetActive(true);
    }
}
