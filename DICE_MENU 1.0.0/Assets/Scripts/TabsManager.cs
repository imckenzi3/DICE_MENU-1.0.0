using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using DG.Tweening;

public enum TabType{
    ClassTab,
    ManaTab
}

public class TabsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{
    public TabType tabType;

    public void OnPointerClick(PointerEventData eventData){
    
    if (tabType == TabType.ClassTab){
        transform.DOPunchRotation(new Vector3(0, 0, 30), 0.2f, 4, 0.5f);
    }

    if (tabType == TabType.ManaTab){
        transform.DOPunchPosition(new Vector3(0, 40, 0), 0.4f, 12, 0.5f);
        transform.DOPunchRotation(new Vector3(0, 0, -30), 0.4f, 12, 0.5f);
    }
    }

    public void OnPointerEnter(PointerEventData eventdata){
        if (tabType == TabType.ClassTab){
            LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 1.5f).setEasePunch();
        }

        if (tabType == TabType.ManaTab){
           transform.DOPunchScale(new Vector3(-0.2f, 0, 0), 0.4f, 12, 0.5f);
        }
    }

    public void OnPointerExit(PointerEventData eventData){

    }
}
