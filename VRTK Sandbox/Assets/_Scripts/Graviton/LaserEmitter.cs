using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LaserEmitter : MonoBehaviour
{
  private LineRenderer _lineRenderer;
  private LaserReceptor _receptor;

  private void Start()
  {
    _lineRenderer = GetComponent<LineRenderer>();
  }

  // private void LateUpdate2()
  // {
  //   _lineRenderer.SetPosition(0, transform.position);
  //   RaycastHit hit;
  //   if (Physics.Raycast(transform.position, transform.up, out hit))
  //   {
  //     if (hit.collider != null)
  //     {
  //       _lineRenderer.SetPosition(1, hit.point);

  //       // Check if we hit a laser emitter
  //       var collidedObject = hit.collider.gameObject;
  //       if (collidedObject.tag == "LaserReceptor")
  //       {
  //         if (_receptor == null)
  //         {
  //           _receptor = collidedObject.GetComponent<LaserReceptor>();
  //           _receptor.PowerUpReceptor();
  //         }
  //       }
  //       else
  //       {
  //         DeActivateReceptorIfActive();
  //       }
  //     }
  //   }
  //   else
  //   {
  //     _lineRenderer.SetPosition(1, transform.position + transform.up * 10);
  //     DeActivateReceptorIfActive();
  //   }
  // }

  private void DeActivateReceptorIfActive()
  {
    if (_receptor != null)
    {
      _receptor.PowerDownReceptor();
      _receptor = null;
    }
  }

  public int maxReflectionCount = 5;
  public float maxStepDistance = 200f;

  void LateUpdate()
  {
    //DrawReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    _lineRenderer.SetPosition(0, transform.position);
    DrawReflectionPattern(this.transform.position, this.transform.up, maxReflectionCount);
  }

  private void DrawReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
  {
    var positionIndex = maxReflectionCount - reflectionsRemaining + 1;
    if (positionIndex >= _lineRenderer.positionCount)
    {
      return;
    }

    Vector3 startingPosition = position;

    Ray ray = new Ray(position, direction);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, maxStepDistance))
    {
      direction = Vector3.Reflect(direction, hit.normal);
      position = hit.point;
    }
    else
    {
      position += direction * maxStepDistance;
    }

    //Debug.DrawLine(startingPosition, position, Color.red);
    if (positionIndex >= _lineRenderer.positionCount)
    {
      //Debug.LogWarning("Invalid laser emitter position index.");
    }
    else
    {
      _lineRenderer.SetPosition(positionIndex, position);
    }

    DrawReflectionPattern(position, direction, reflectionsRemaining - 1);
  }

}
