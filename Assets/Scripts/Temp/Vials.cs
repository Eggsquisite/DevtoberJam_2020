using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Vials : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPos;
    public CanvasGroup beaker;
    public CanvasGroup ladel;
    private CanvasGroup canvasGroup;
    private Animation anim;
    private static float animTime = 2f;

    //public enum LiquidColor { Red, Yellow, Blue, Purple, Green, Orange };

    public LiquidColor m_color;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        canvasGroup = GetComponent<CanvasGroup>();
        anim = GetComponent<Animation>();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        //Debug.Log("Beginning Drag with color: " + m_color);
        canvasGroup.blocksRaycasts = false;

        if (beaker != null)
            beaker.blocksRaycasts = true;
        if (ladel != null)
            ladel.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        transform.position = originalPos;
        canvasGroup.blocksRaycasts = true;

        if (beaker != null)
            beaker.blocksRaycasts = false;
        if (ladel != null)
            ladel.blocksRaycasts = true;
        // If vial is over potion mix and can change color, change potion mix to appropriate color depending on vial
    }

    public LiquidColor GetColor()
    {
        return m_color;
    }

    public void Animation()
    {
        StopCoroutine("PlayAnimation");
        StartCoroutine("PlayAnimation");
    }

    IEnumerator PlayAnimation()
    {
        anim.Play();
        canvasGroup.blocksRaycasts = false;
        yield return new WaitForSeconds(animTime);

        anim.Stop();
        transform.position = originalPos;
        transform.rotation = Quaternion.identity;
        canvasGroup.blocksRaycasts = true;
    }
}
