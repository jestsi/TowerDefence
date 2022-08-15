using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class Timer : MonoBehaviour
    {
        private Coroutine _startedCoroutine;
        private float _timeForWait;
        private bool _isStarted;

        public Coroutine StartedCoroutine => _startedCoroutine;
        public float TimeForWait => _timeForWait;
        public bool IsStarted => _isStarted;

        public event Action InvokingBeforeTimeForWait;

        public Coroutine StartTimer(float repeatTime, Action action)
        {
            _timeForWait = repeatTime;

            _startedCoroutine = StartCoroutine(nameof(StartCoroutine), action);

            return _startedCoroutine;
        }

        public Coroutine StartTimer(Action action, float repeatTime = 0)
        {
            return StartCoroutine(nameof(StartCoroutineE), (repeatTime, action));
        }
        
        public Coroutine StartTimer(Action action, object param)
        {
            return StartCoroutine(nameof(StartCoroutineE), (param, action));
        }

        public void StopTimer()
        {
            if (_startedCoroutine is null) return;
            StopCoroutine(_startedCoroutine);
            _timeForWait = -1;
            _startedCoroutine = default;
            _isStarted = false;
        }

        public IEnumerator StartCoroutine(Action action)
        {
            _isStarted = true;
            while (_isStarted)
            {
                yield return new WaitForSeconds(_timeForWait);

                InvokingBeforeTimeForWait?.Invoke();
                action?.Invoke();
            }
            yield break;
        }
        public IEnumerator StartCoroutineE((float repeatTime, Action action) tuple)
        {
            while (true)
            {
                yield return new WaitForSeconds(tuple.repeatTime);

                InvokingBeforeTimeForWait?.Invoke();
                tuple.action?.Invoke();
            }
        }
    }
}
