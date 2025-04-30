/* 구조체 이름을 이벤트 이름으로 사용합니다
 * 해당 이벤트에서 전달할 값들을 선언해주시면 됩니다. 여러개도 가능
 * 생성자를 정의해주시면 트리거 하기 편해집니다
 * Trigger(new PlayerHealthChanged { PrevHealth = 20, ChangedHealth = 10 });
 * Trigger(new PlayerHealthChanged(20,10);
 */


/// <summary>
/// 플레이어 체력이 변경됐을 때, 
/// 생성자 (변경 전 체력, 변경 후 체력)
/// </summary>
public struct PlayerHealthChanged
{
    public int PrevHealth { get; private set; }
    public int ChangedHealth { get; private set; }

    public PlayerHealthChanged(int prev, int changed)
    {
        PrevHealth = prev;
        ChangedHealth = changed;
    }
}

public struct PlayerFireBreathEnded
{

}