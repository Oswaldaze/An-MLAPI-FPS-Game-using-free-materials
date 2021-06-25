using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    //Remove all reference points created
    public void RemoveAllReferencePoints()
    {
        foreach (var referencePoint in m_ReferencePoint)
        {
            Destroy(referencePoint);
        }
        m_ReferencePoint.Clear();
    }


    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_ReferencePointManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_ReferencePoint = new List<ARAnchor>();
    }


    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            TrackableId planeId = s_Hits[0].trackableId; //get the ID of the plane hit by the raycast
            var referencePoint = m_ReferencePointManager.AttachAnchor(m_PlaneManager.GetPlane(planeId), hitPose);
            if (referencePoint != null)
            {
                RemoveAllReferencePoints();
                m_ReferencePoint.Add(referencePoint);
            }
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_ReferencePointManager;
    List<ARAnchor> m_ReferencePoint; 
    ARPlaneManager m_PlaneManager;

}

