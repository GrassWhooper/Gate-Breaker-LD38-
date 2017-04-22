using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public class CameraRayCaster : MonoBehaviour
{
    //rendered by custom editor;
    [Tooltip("Time Before Next Click Is Regiestered")]
    [SerializeField]
    float clickTimeDamp = 0.17f;


    [SerializeField] int[] layerPriorities = new int[0];

    public delegate void OnLayerChange(int newLayer);
    public event OnLayerChange OnLayerChangedEvent;

    public delegate void OnMouseClickDelegate(RaycastHit whatWasHit, int layerHit);
    public event OnMouseClickDelegate OnMouseClickEvent;

    int topPriorityLastFrame;
    float maxCastLength = 300;
    Camera viewCamera;
    SortRayCastHitsByDist sortRayCastByDist = new SortRayCastHitsByDist();
    float clickTimeCounter;
    private void Start()
    {
        viewCamera = Camera.main;
        clickTimeCounter = clickTimeDamp;
    }

    private void Update()
    {
        if (clickTimeCounter > 0)
        {
            clickTimeCounter -= Time.deltaTime;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Cursor Is ON UI
            NotifyIfLayerChanged(5);
            return;
        }

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        //Sort All The Hits (The Closest To Camera Will Come First).
        RaycastHit[] allContacts = Physics.RaycastAll(ray, maxCastLength);
        List<RaycastHit> allContactsList = allContacts.ToList();
        allContactsList.Sort(sortRayCastByDist);
        allContacts = allContactsList.ToArray();

        RaycastHit? topPriorityHit = FindTopPriorityHit(allContacts);
        if (topPriorityHit.HasValue == false)
        {
            //Cursor Is Set To Default Layer, Due To Not Hitting Any Thing.
            NotifyIfLayerChanged(0);
            return;
        }

        int topLayer = topPriorityHit.Value.transform.gameObject.layer;
        NotifyIfLayerChanged(topLayer);

        if (Input.GetMouseButtonUp(0) && clickTimeCounter <= 0)
        {
            clickTimeCounter = clickTimeDamp;
            if (OnMouseClickEvent != null)
            {
                OnMouseClickEvent(topPriorityHit.Value, topLayer);
            }
        }
        #region Deprecated
        //foreach (Layer l in priorities)
        //{
        //    RaycastHit? hit = RayCastForLayer(l);
        //    if (hit.HasValue == true)
        //    {
        //        this.hit = hit.Value;
        //        if (layerHit != l)
        //        {
        //            layerHit = l;
        //            OnLayerChangedEvent(layerHit);
        //        }
        //        return;
        //    }
        //}
        //hit.distance = maxCastLength;
        //layerHit = Layer.RayCastEndStop;
        //OnLayerChangedEvent(layerHit);
        #endregion
    }
    RaycastHit? FindTopPriorityHit(RaycastHit[] allHits)
    {
        RaycastHit? topPriority = null;
        foreach (int layer in layerPriorities)
        {
            foreach (RaycastHit hitTocompare in allHits)
            {
                if (hitTocompare.transform.gameObject.layer == layer)
                {
                    return hitTocompare;
                }
            }
        }
        return topPriority;
    }
    void NotifyIfLayerChanged(int topLayer)
    {
        if (topLayer != topPriorityLastFrame)
        {
            topPriorityLastFrame = topLayer;
            if (OnLayerChangedEvent != null)
            {
                OnLayerChangedEvent(topLayer);
            }

        }
    }

    RaycastHit? RayCastForLayer(int layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool didHit = Physics.Raycast(ray, out hit, maxCastLength, layerMask);
        if (didHit)
        {
            return hit;
        }
        return null;
    }
}