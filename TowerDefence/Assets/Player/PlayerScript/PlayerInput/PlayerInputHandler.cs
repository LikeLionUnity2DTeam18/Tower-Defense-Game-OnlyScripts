using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls inputActions;

    public Vector2 MoveInput { get; private set; }

    // 이벤트 방식
    public event Action OnSkillQPressed;
    public event Action OnLeftClick;
    public event Action OnRightClick;

    private void Awake()
    {
        inputActions = new PlayerControls();

        // 이벤트 등록
        inputActions.PlayerControl.Skill_Q.performed += _ => OnSkillQPressed?.Invoke();
        inputActions.PlayerControl.LeftClick.performed += _ => OnLeftClick?.Invoke();
        inputActions.PlayerControl.RightClick.performed += _ => OnRightClick?.Invoke();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        MoveInput = inputActions.PlayerControl.Move.ReadValue<Vector2>();
    }
}