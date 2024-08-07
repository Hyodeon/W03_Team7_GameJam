using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>�÷��̾��� ���� ����</para>
/// <para>�÷��̾� ������ �������� ���Ƹ��� ��ġ</para>
/// <para>Follower ����</para>
/// </summary>
public class PlayerBase : MonoBehaviour
{
    private HashSet<GameObject> _followerList;

    private Vector3 _followPoint;

    private FollowCamera _followCamera;

    public Vector3 FollowPoint { get { return _followPoint; } }
    

    private void Awake()
    {
        _followerList = new HashSet<GameObject>();

        _followCamera = Camera.main.GetComponentCinemachine

        if (_followCamera == null)
        {
            Debug.Log("�Ӵ�");
        }

        Follower[] allObjectsWithMyComponent = FindObjectsOfType<Follower>();

        foreach (Follower fo in allObjectsWithMyComponent)
        {
            Debug.Log("Follower �߰�!");
            _followerList.Add(fo.gameObject);
        }
    }

    private void Update()
    {
        RefreshFollowPoint();
    }

    private void LocateFollowers()
    {
        
    }

    public void AddFollower(GameObject go)
    {
        _followerList.Add(go);
        Camera.main.gameObject.GetComponent<FollowCamera>().AddFollower();
    }

    public void DeleteObejctFromList(GameObject go)
    {
        _followerList.Remove(go);
        Camera.main.GetComponent<FollowCamera>().DeleteFollower();
    }

    public void RefreshFollowPoint()
    {
        float radius = Mathf.Sqrt(1.2f * 30 * 0.5f);

        _followPoint = transform.position + (-transform.forward) * radius;
    }

    public void PropagateJump()
    {
        foreach (GameObject go in _followerList)
        {
            Vector3 distVector = FollowPoint - go.transform.position;
            distVector.y = 0;
            float distance = (transform.position - go.transform.position).magnitude;

            StartCoroutine(DoJump(go, distance));
        }
    }

    private IEnumerator DoJump(GameObject follower, float distance)
    {
        if (distance < 10)
        {
            Follower foComp = follower.GetComponent<Follower>();

            yield return new WaitForSeconds(distance / foComp.MoveSpeed);

            foComp.TriggerState(Follower.State.Jump);
        }
    }
}


#region [Legacy] Chick Location Random Generation

//void CalculateRadius()
//{
//    radius = Mathf.Sqrt(minDistance * numberOfPoints * 0.5f);
//    radius *= 1.2f;
//    Debug.Log($"�� {numberOfPoints}�� ���� �е� {minDistance}�� �������� {radius} �Դϴ�!");
//}

//void SpawnPointsInCircle(int count)
//{
//    int attempts = 0;

//    while (points.Count < count && attempts < count * 10)
//    {
//        Vector2 point = GetRandomPointInCircle(radius);

//        if (IsPointValid(point))
//        {
//            points.Add(point);
//            //Instantiate(pointPrefab, point, Quaternion.identity);
//        }

//        attempts++;
//    }

//    Debug.Log($"���� ������ �� ���� : {points.Count}");
//}


//Vector2 GetRandomPointInCircle(float radius)
//{
//    float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
//    float r = radius * Mathf.Sqrt(UnityEngine.Random.Range(0f, 1f));
//    float x = r * Mathf.Cos(angle);
//    float y = r * Mathf.Sin(angle);
//    return new Vector2(x, y);
//}
//bool IsPointValid(Vector2 point)
//{
//    foreach (Vector2 existingPoint in points)
//    {
//        if (Vector2.Distance(point, existingPoint) < minDistance)
//        {
//            return false;
//        }
//    }
//    return true;
//}

//void OnDrawGizmos()
//{
//    if (points == null)
//        return;

//    Gizmos.color = Color.red;
//    foreach (Vector2 point in points)
//    {
//        Gizmos.DrawSphere(point, 0.7f); // ������ ��ġ�� ���� ���� ǥ��
//    }
//}

#endregion