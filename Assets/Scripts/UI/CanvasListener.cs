using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasListener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        //Debug.Log("POINTER CLICKED");
        if (PatronManager.patronState == PatronState.READY_TO_LEAVE) {
            GameObject.Find("PatronManager").GetComponent<PatronManager>().SayByeBye();
        }
    }
}

