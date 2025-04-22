using UnityEngine;

/// <summary>
/// 플레이어 애니메이터에서 사용할 파라미터들을 관리하기 위한 클래스
/// 에디터의 자동완성 기능으로 오타없이 안전한 사용 보장
/// string이 아닌 int 형태의 Hash로 저장해서 미약하지만 성능 최적화
/// 사용 예시:
/// animator.SetFloat(PlayerAnimationParams.MoveX, input.x);
/// animator.SetBool(PlayerAnimationParams.Move, true);
/// </summary>
public static class PlayerAnimationParams
{
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int MoveX = Animator.StringToHash("MoveX");
    public static readonly int MoveY = Animator.StringToHash("MoveY");
    public static readonly int Move = Animator.StringToHash("Move");
}

