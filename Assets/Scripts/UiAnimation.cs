using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiAnimation : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]public  Image[] skuls = new Image[3];
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Animation>().Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Animation>().Stop();
        transform.localScale = new Vector3((float)0.7, (float)0.7, 1);
    }
}
