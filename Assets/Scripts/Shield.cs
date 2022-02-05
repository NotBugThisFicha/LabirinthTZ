using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Shield : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static Shield Instance;

    internal bool shieldIsActiv;
    public static UnityEvent ColorEvent = new UnityEvent();

    private void Awake()
    {
        Instance = this;
        shieldIsActiv = false;
        Debug.Log("tabu" + shieldIsActiv);
    }

    public void ShieldActivator(bool isActiv)
    {
        shieldIsActiv = isActiv;

        Debug.Log("tabu" + shieldIsActiv);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShieldActivator(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ShieldActivator(false);
    }
}
