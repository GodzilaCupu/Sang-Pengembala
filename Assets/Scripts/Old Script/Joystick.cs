using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image bgImage;
    private Image joyStickImage;

    private Animator playerAnim;

    public Vector2 inputDir;

    public float offset = 2f;

    public GameObject objectGerak;
    public GameObject objectPutar;
    public float speedMax = 5f;
    public float zAxis;

    
    // Start is called before the first frame update
    void Start()
    {
        bgImage = GetComponent<Image> ();
        joyStickImage = transform.GetChild(0).GetComponent<Image>();
        playerAnim = GameObject.Find("Character").GetComponent<Animator>();
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        float bgImageSizeX = bgImage.rectTransform.sizeDelta.x;
        float bgImageSizeY = bgImage.rectTransform.sizeDelta.y;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x /= bgImageSizeX;
            pos.y /= bgImageSizeY;
            inputDir = new Vector2(pos.x, pos.y);
            inputDir = inputDir.magnitude > 1 ? inputDir.normalized : inputDir;
            joyStickImage.rectTransform.anchoredPosition = new Vector2(inputDir.x * (bgImageSizeX / offset), inputDir.y * (bgImageSizeY / offset));

            //Objek putar
            zAxis = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            objectPutar.transform.eulerAngles = new Vector3(0, zAxis, 0);
            playerAnim.SetBool("Walk", true);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDir = Vector2.zero;
        joyStickImage.rectTransform.anchoredPosition = Vector2.zero;
        playerAnim.SetBool("Walk", false);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        objectGerak.transform.Translate(inputDir.x * speedMax * Time.deltaTime, 0, inputDir.y * speedMax * Time.deltaTime);
    }

}
