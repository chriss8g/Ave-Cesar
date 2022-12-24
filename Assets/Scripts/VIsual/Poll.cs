using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Poll : MonoBehaviour, IDropHandler
{   
    /// <summary>
    /// asigna como padre del elemento soltado a este contenedor
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        DragAndDrop.itemDragging.transform.SetParent(transform);
    }
}
