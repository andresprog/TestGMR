using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This component makes objects react aesthetically to pointer events.
/// Physical and UI objects can make use of this component.
/// </summary>
public class Highlighter : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler
{
    /// <summary> The curve used to scale the object </summary>
    public AnimationCurve scalingCurve;

    /// <summary> The time we will take to traverse the scaling curve. </summary>
    public float highlightTime = 0.2f;

    /// <summary> This controls how much we will grow when we point the object with our mouse/touch. </summary>
    public float intensity = 0.3f;

    /// <summary> This is the object which will be scaled. It should be a purely visual object
    /// (it should not contain a collider). </summary>
    public GameObject objectToHighlight;

    /// <summary> Are we currently traversing the animation curve? </summary>
    private bool highlighting = false;

    /// <summary> We scale the object based on this default scale. </summary>
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = objectToHighlight.transform.localScale;
    }

    // Whenever we Enable the object, we change highlighting to false.
    // We use it in case we interrupt the Highlight Coroutine.
    void OnEnable()
    {
        highlighting = false;
    }

    // When the mouse enters we make the object grow and play a sound.
    public void OnPointerEnter(PointerEventData eventData)
    {
        objectToHighlight.transform.localScale = Vector3.Scale(objectToHighlight.transform.localScale, Vector3.one * (1 + intensity));

        Debug.Log("Play: Audio Highlight");
        //AudioSystem.Instance.PlayOneShot(SfxType.Select, 0.2f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    // When the mouse exits, if we are not currently animating the object, we scale it back to its original scale.
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!highlighting)
        {
            objectToHighlight.transform.localScale = originalScale;
        }
    }

    //TODO: mover este comentario a funcion Highlight
    // When we click the object we start its scaling animation (if we are not currently running one).
    // We also play a sound.
    public void OnPointerClick(PointerEventData eventData)
    {
        Highlight();
        Debug.Log("Play: Audio OnClick");
        // AudioSystem.Instance.PlayOneShot(SfxType.Whoop, 0.5f);
    }

    //TODO: comment
    public void Highlight ()
    {
        if (!highlighting)
        {
            StartCoroutine(Cor_Highlight());
        }
    }

    /// <summary>
    /// This coroutine scales the object according to the <see cref="Highlighter.scalingCurve"/> in the
    /// span of <see cref="Highlighter.highlightTime"/> seconds.
    /// </summary>
    private IEnumerator Cor_Highlight()
    {
        highlighting = true;

        float lerpTimer = 0;
        while (lerpTimer < 1)
        {
            lerpTimer += Time.unscaledDeltaTime / highlightTime;
            objectToHighlight.transform.localScale = Vector3.Scale(originalScale, Vector3.one * scalingCurve.Evaluate(lerpTimer));
            yield return null;
        }

        objectToHighlight.transform.localScale = originalScale;

        highlighting = false;
    }
}
