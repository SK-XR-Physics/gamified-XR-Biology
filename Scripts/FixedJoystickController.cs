using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// -- For a Joystick-based 2D Movement but can be applicable in 3D in some way -- //
/* The Class derives from UnityEngine's EventSystem Namespace and includes deriving from
 *  IDragHandler, IPointerHandler, IPointerDownHandler which just reads user input
 */
public class FixedJoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private float joystickHandleLimit;
    private Image HandleLimit;
    private Image Handle;

    private Vector3 direction;
    public Vector3 Direction { get { return direction; } }

    private void Start()
    {
        var imgs = GetComponentsInChildren<Image>();
        HandleLimit = imgs[0];
        Handle = imgs[1];
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(HandleLimit.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / HandleLimit.rectTransform.sizeDelta.x);
            pos.y = (pos.y / HandleLimit.rectTransform.sizeDelta.y);

            //Pivot might be giving an offset, adjust here
            //Vector2 refPivot = new Vector2(0.5f, 0.5f);
            Vector2 p = HandleLimit.rectTransform.pivot;
            pos.x += p.x - 0.5f;
            pos.y += p.y - 0.5f;

            //Clamp values
            float x = Mathf.Clamp(pos.x, -1, 1);
            float y = Mathf.Clamp(pos.y, -1, 1);

            direction = new Vector3(x, 0, y).normalized;
            //Debug.Log(direction);

            //Also move the visuals to reflect direction
            Handle.rectTransform.anchoredPosition = new Vector3(direction.x * joystickHandleLimit, direction.z * joystickHandleLimit);
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        direction = default(Vector3);
        Handle.rectTransform.anchoredPosition = default(Vector3);
    }
}