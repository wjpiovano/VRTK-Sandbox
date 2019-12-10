using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
  private LineRenderer lineRenderer;

  private void Start()
  {
    lineRenderer = GetComponent<LineRenderer>();
  }

  private void LateUpdate()
  {
    lineRenderer.SetPosition(0, transform.position);
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.up, out hit))
    {
      if (hit.collider != null)
      {
        lineRenderer.SetPosition(1, hit.point);
      }
    }
    else
    {
      lineRenderer.SetPosition(1, transform.position + transform.up * 10);
    }
  }
}
