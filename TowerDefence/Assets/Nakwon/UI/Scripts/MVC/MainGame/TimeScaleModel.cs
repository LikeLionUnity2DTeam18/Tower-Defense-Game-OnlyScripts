using UnityEngine;

public enum SpeedType
{
    OneSpeed = 1,
    TwoSpeed = 2,
    ThreeSpeed = 3
}

public class TimeScaleModel
{
    //현재 속도 상태를 저장하는 private 변수, 기본은 OneSpeed
    private SpeedType speedType = SpeedType.OneSpeed;
    //외부에서 현재 속도를 읽을 수 있는 public 프로퍼티
    public SpeedType SpeedType => speedType;

    // 속도 변경 메서드
    public void ChangeSpeed()
    {
        switch (speedType)
        {
            case SpeedType.OneSpeed:
                speedType = SpeedType.TwoSpeed; //1->2 배속
                break;
            case SpeedType.TwoSpeed:
                speedType = SpeedType.ThreeSpeed; //2->3 배속
                break;
            case SpeedType.ThreeSpeed:
                speedType = SpeedType.OneSpeed; //3->1 배속
            break;
            default:
                Debug.Log("배속 고장났다!!!");
                break;
        }
        
        // Unity의 시간 배속을 반영
        Time.timeScale = (int)speedType;
        Debug.Log($"Trigger 배속: {(int)speedType} ({speedType})");
        EventManager.Trigger(new SpeedChanged(speedType));
    }
}
