using System.Collections.Generic;
using UnityEngine;

public class FollowingTheRoad : MonoBehaviour
{
    [SerializeField] private RoadPath _roadPath;
    [SerializeField, Range(0.1f, 10)] private float _distanceOffset;
    [SerializeField] private float _movingSpeed = 5f;

    private IEnumerator<Transform> _dotInPath;

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        Follow();
    }

    private void Init()
    {
        _dotInPath = _roadPath.GetNextPathPoint();
        _dotInPath.MoveNext();

        if (_dotInPath.Current == null)
            return;

        transform.position = _dotInPath.Current.position;
    }

    private void Follow()
    {
        if (_dotInPath == null || _dotInPath.Current == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, _dotInPath.Current.position, Time.deltaTime * _movingSpeed);

        float suffitientOffset = (transform.position - _dotInPath.Current.position).sqrMagnitude;

        if (suffitientOffset < Mathf.Pow(_distanceOffset, 2))
        {
            _dotInPath.MoveNext();
            Vector3 direction = (_dotInPath.Current.position - transform.position).normalized;
        }
    }
}
