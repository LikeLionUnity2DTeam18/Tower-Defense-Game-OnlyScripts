using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 옵저버 패턴을 위한 이벤트 매니저
/// 이벤트 이름은 각 파트별 이벤트 리스트 파일에서 정의
/// PlayerEvents.cs 파일 참고해주세요
/// 리스너 등록
///     EventManager.Instance.AddListner<이벤트이름>(이벤트를 메서드 명)
/// 트리거 발동
///     EventManager.Instance.Trigger(new 이벤트이름 구조체 생성)
///     EX) EventManager.Instance.Trigger(new PlayerHealthChanged { prevHealth = 20, changedHealth = 10 });
/// 이벤트를 사용할 메서드 정의
///     OnPlayerHealthChanged(PlayerHealthChanged event)
/// </summary>
public class EventManager : MonoBehaviour
{
    // 싱글톤
    public static EventManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    

    private static readonly Dictionary<Type, Delegate> _eventTable = new();

    public static void AddListener<T>(Action<T> listener)
    {
        if (_eventTable.TryGetValue(typeof(T), out var del))
        {
            _eventTable[typeof(T)] = (Action<T>)del + listener;
        }
        else
        {
            _eventTable[typeof(T)] = listener;
        }
    }

    public static void RemoveListener<T>(Action<T> listener)
    {
        if (_eventTable.TryGetValue(typeof(T), out var del))
        {
            var currentDel = (Action<T>)del - listener;
            if (currentDel == null)
                _eventTable.Remove(typeof(T));
            else
                _eventTable[typeof(T)] = currentDel;
        }
    }

    public static void Trigger<T>(T eventData)
    {
        if (_eventTable.TryGetValue(typeof(T), out var del))
        {
            ((Action<T>)del)?.Invoke(eventData);
        }
    }
}