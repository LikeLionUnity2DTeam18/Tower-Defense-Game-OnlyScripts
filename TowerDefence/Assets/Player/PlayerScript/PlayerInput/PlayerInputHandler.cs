using System;
using UnityEngine;

/// <summary>
/// 유니티의 신규 인풋시스템을 사용하기 위한 클래스
/// 사용 할 입력 키 등록
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls inputActions;

    public Vector2 MoveInput { get; private set; }

    // 이벤트 방식
    public event Action OnSkillQPressed;
    public event Action OnSkillWPressed;
    public event Action OnSkillEPressed;
    public event Action OnSkillRPressed;
    public event Action GPressed;
    public event Action OnLeftClick;
    public event Action OnRightClick;

    private void Awake()
    {
        inputActions = new PlayerControls();

        // 이벤트 등록
        inputActions.PlayerControl.Skill_Q.performed += _ => OnSkillQPressed?.Invoke();
        inputActions.PlayerControl.Skill_W.performed += _ => OnSkillWPressed?.Invoke();
        inputActions.PlayerControl.Skill_E.performed += _ => OnSkillEPressed?.Invoke();
        inputActions.PlayerControl.Skill_R.performed += _ => OnSkillRPressed?.Invoke();
        inputActions.PlayerControl.FlipSkill_G.performed += _ => GPressed?.Invoke();
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