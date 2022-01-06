using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickScript : MonoBehaviour
{
    [Header("Character Component")]
    [SerializeField] private CharacterController charControl;
    [SerializeField] private Animator charAnim;

    [Header("JoyStick Component")]
    [SerializeField] private float offset = 2f;
    
    private Image bgJoyStick;
    private Image dragJoyStick;

    private Vector2 inputDirection;

    [Header("Walking Component")]
    [SerializeField] private float charSpeed;
    [SerializeField] private float smothingTurn = 1f;
    private float smohtingVelocityTurn;

    [SerializeField] private bool isWalking;

    private void Start()
    {
        bgJoyStick = this.GetComponent<Image>();
        dragJoyStick = transform.GetChild(0).GetComponent<Image>();
    }

    void OnDrag(PointerEventData eventData)
    {
        Vector2 _pos = Vector2.zero;
        float _bgJoyStickSizeX = bgJoyStick.rectTransform.sizeDelta.x;
        float _bgJoyStickSizeY = bgJoyStick.rectTransform.sizeDelta.y;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgJoyStick.rectTransform,eventData.position,eventData.pressEventCamera,out _pos))
        {
            _pos.x /= _bgJoyStickSizeX;
            _pos.y /= _bgJoyStickSizeY;

            inputDirection = new Vector2(_pos.x, _pos.y);

            inputDirection = inputDirection.magnitude > 1 ? inputDirection.normalized : inputDirection;

            dragJoyStick.rectTransform.anchoredPosition = new Vector2(inputDirection.x * (_bgJoyStickSizeX / offset), inputDirection.y * (_bgJoyStickSizeY / offset));


        }
    }
}
