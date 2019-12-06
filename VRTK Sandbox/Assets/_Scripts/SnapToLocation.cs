using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToLocation : MonoBehaviour
{
  public float speed = 20.0f;

  private Vector3 _localSnapPosition;
  private Quaternion _localSnapRotation;

  private void Start()
  {
    _localSnapPosition = transform.localPosition;
    _localSnapRotation = transform.localRotation;

    Debug.LogFormat("\n\t\t LocalPos: {0} -- LocalRot: {1}", _localSnapPosition, _localSnapRotation.eulerAngles.ToString());
  }

  public void Snap()
  {
    StartCoroutine("DoSnap");
  }

  private IEnumerator DoSnap()
  {    
    var distanceLeft = Vector3.Distance(transform.localPosition, _localSnapPosition);
    
    while (distanceLeft > 0.005)
    {
      float step = speed * Time.deltaTime;
      transform.localPosition = Vector3.MoveTowards(transform.localPosition, _localSnapPosition, speed * Time.deltaTime);
      transform.localRotation = Quaternion.Lerp(transform.localRotation, _localSnapRotation, speed * Time.deltaTime);
      yield return null;
      distanceLeft = Vector3.Distance(transform.localPosition, _localSnapPosition);
    }
    transform.localPosition = _localSnapPosition;
    transform.localRotation = _localSnapRotation;
  }
}
