/* 구조체 이름을 이벤트 이름으로 사용합니다
 * 해당 이벤트에서 전달할 값들을 선언해주시면 됩니다. 여러개도 가능
 * 생성자를 정의해주시면 트리거 하기 편해집니다
 * Trigger(new PlayerHealthChanged { PrevHealth = 20, ChangedHealth = 10 });
 * Trigger(new PlayerHealthChanged(20,10);
 */

public enum StageChangeEventType { Start, End}
public struct StageChangeEvent
{
    public StageChangeEventType EventType;
    public int Stage;

    public StageChangeEvent (StageChangeEventType type, int stage)
    {
        EventType = type; 
        Stage = stage;
    }
}

