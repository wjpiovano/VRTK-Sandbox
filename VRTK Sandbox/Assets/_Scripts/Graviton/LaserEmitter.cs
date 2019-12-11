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

  private void ActivateReceptorIfInactive(GameObject collidedObject)
  {
    if (_receptor == null)
      {
        _receptor = collidedObject.GetComponent<LaserReceptor>();
        _receptor.PowerUpReceptor();
      }
  }

  public int maxReflectionCount = 5;
  public float maxStepDistance = 200f;

  void LateUpdate()
  {
    //if (this.transform.hasChanged)
    {
      _lineRenderer.SetPosition(0, transform.position);
      var receptorHit = false;      
      DrawReflectionPattern(this.transform.position, this.transform.up, maxReflectionCount, ref receptorHit);
      if (!receptorHit)
      {
        // We have reached the end of the reflection cycle and did not hit a receptor.
        // Deactivate any receptor we happened to have activated before.
        DeActivateReceptorIfActive();
      }

      this.transform.hasChanged = false;
    }
  }

  private void DrawReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining, ref bool receptorHit)
  {
    var positionIndex = maxReflectionCount - reflectionsRemaining + 1;

    if (positionIndex >= _lineRenderer.positionCount)
    {
      return;
    }

    Vector3 startingPosition = position;
    bool continueReflecting = false;

    Ray ray = new Ray(position, direction);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, maxStepDistance))
    {
      position = hit.point;
      var collidedObject = hit.collider.gameObject;      
      if (this.ReceptorHit(collidedObject))
      {
        ActivateReceptorIfInactive(collidedObject);
        receptorHit = true;
        NullifyRemainingPoints(positionIndex, position);
      }
      else if (this.MirrorHit(collidedObject))
      {
        direction = Vector3.Reflect(direction, hit.normal);
        continueReflecting = true;
      }
      else
      {
        NullifyRemainingPoints(positionIndex, position);
      }
    }
    else
    {
      position += direction * maxStepDistance;
    }

    //Debug.DrawLine(startingPosition, position, Color.red);
    _lineRenderer.SetPosition(positionIndex, position);

    if (continueReflecting)
    {
      DrawReflectionPattern(position, direction, reflectionsRemaining - 1, ref receptorHit);
    }
  }

  // Check if we hit a laser emitter
  private bool ReceptorHit(GameObject collidedObject)
  { 
    return collidedObject.tag == "LaserReceptor";
  }

  // Check if we hit a mirror
  private bool MirrorHit(GameObject collidedObject)
  {
    return collidedObject.tag == "Mirror";
  }

  private void NullifyRemainingPoints(int positionIndex, Vector3 position)
  {
    for (int index = positionIndex; index < _lineRenderer.positionCount; index++)
    {
      _lineRenderer.SetPosition(index, position);
    }
  }

}
