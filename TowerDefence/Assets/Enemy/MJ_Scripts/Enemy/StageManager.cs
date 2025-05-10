//using UnityEngine;
//using System.Collections;

//public class StageManager : MonoBehaviour
//{
//    public EnemySpawnManager spawnManager;  // 스폰 매니저 연결 (인스펙터에서 할당)
//    public float stageDuration = 30f;        // 스테이지 지속 시간
//    private bool isStageActive = false;

//    private void Start()
//    {
//        StartCoroutine(StageFlow());
//    }

//    private IEnumerator StageFlow()
//    {
//        while (true)
//        {
//            // 1. 스테이지 시작
//            Debug.Log("[StageManager] 스테이지 시작!");
//            isStageActive = true;
//            spawnManager.StartSpawning();

//            // 2. 스테이지 지속 시간 대기
//            yield return new WaitForSeconds(stageDuration);

//            // 3. 스테이지 종료
//            Debug.Log("[StageManager] 스테이지 종료!");
//            isStageActive = false;
//            spawnManager.StopSpawning();

//            // 4. 스테이지 종료 후 정리 시간 (예: 5초 휴식)
//            yield return new WaitForSeconds(5f);
//        }
//    }
//}
