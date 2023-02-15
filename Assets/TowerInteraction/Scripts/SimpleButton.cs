using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SimpleButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite hover;

    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData) 
    { 

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }



}
