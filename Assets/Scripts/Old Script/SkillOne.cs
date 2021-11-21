using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillOne : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Image bgImage;
    private Image joystickImage;

    public GameObject skillMajuMundur;
    public GameObject skillRangeObject;
    public Light lampu;

    public Vector2 InputDirSkill;

    public float offset = 2f;
    public float minOffset = 0.03f;
    public float offsetSkill = 5f;

    public float magnitudo;
    public float zAxisSkill;

    static int active;

    private GameObject jebakan;
    public GameObject objPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        
        InputDirSkill = Vector2.zero;
        skillRangeObject.GetComponent<Renderer>().enabled = false;
        skillMajuMundur.GetComponent<Renderer>().enabled = false;
        //lampu = GetComponent<Light>();
        lampu.enabled = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirSkill = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
        magnitudo = 0;
        skillRangeObject.GetComponent<Renderer>().enabled = false;
        skillMajuMundur.GetComponent<Renderer>().enabled = false;

        if(active == 1)
        {
            active = 0;
            //aksi skill disini bre
            Vector3 targetPos;
            targetPos = skillMajuMundur.transform.position;

            //buat bola
            //GameObject bola = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            jebakan = GameObject.Instantiate(objPrefabs, targetPos, Quaternion.identity);
            
            //Color randomColor = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            jebakan.transform.position = targetPos;
            //bola.GetComponent<Renderer>().material.color = randomColor;
        }

        lampu.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        skillRangeObject.GetComponent<Renderer>().enabled = true;
        OnDrag(eventData);
        lampu.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 possSkillOne = Vector2.zero;
        float bgimageSizeX = bgImage.rectTransform.sizeDelta.x;
        float bgimageSizeY = bgImage.rectTransform.sizeDelta.y;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, eventData.position, eventData.pressEventCamera, out possSkillOne))
        {
            possSkillOne.x /= bgimageSizeX;
            possSkillOne.y /= bgimageSizeY;

            InputDirSkill = new Vector2(possSkillOne.x, possSkillOne.y);
            InputDirSkill = InputDirSkill.magnitude > 1 ? InputDirSkill.normalized : InputDirSkill;//peringkasan penulisan if

            /* cara baca if diatas
             * if(InputDirSkill.magnitude > 1)
             * { InputDirSkill.normalized; } else { InputDirSkill = InputDirSkill;} 
             */

            joystickImage.rectTransform.anchoredPosition = new Vector2(InputDirSkill.x * (bgimageSizeX / offset), InputDirSkill.y * (bgimageSizeY / offset));
            zAxisSkill = Mathf.Atan2(InputDirSkill.x, InputDirSkill.y) * Mathf.Rad2Deg;

            skillRangeObject.transform.eulerAngles = new Vector3(0, zAxisSkill, 0);

            magnitudo = Vector2.SqrMagnitude(InputDirSkill);
            if(magnitudo >= minOffset)
            {
                skillMajuMundur.GetComponent<Renderer>().enabled = true;
                skillMajuMundur.transform.localPosition = new Vector3(0, 0, offsetSkill * magnitudo);
                active = 1;
            } 
            
            else
            {
                skillMajuMundur.GetComponent<Renderer>().enabled = false;
                active = 0;
            }

            lampu.transform.LookAt(skillMajuMundur.transform.position);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
