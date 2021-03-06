using UnityEngine;

public class Render3D : MonoBehaviour
{
    [SerializeField] private Transform screen1 = null;
    [SerializeField] private Transform screen2 = null;
    [SerializeField] private Transform sprite1 = null;
    [SerializeField] private Transform sprite2 = null;
    [SerializeField] private Transform scroll = null;
    [SerializeField] private TextChild textController = null;
    [SerializeField]
    private GameObject load = null;
    [SerializeField] private float speed = 1;
    private bool play = false;
    private float actualAspect = 0.75f;
    private float viewHeight = 0.22f;
    public Camera camera1 = null;
    public Camera camera2 = null;
    private bool cam1Left = false;
    private float y = 0;

    public bool Play
    {
        set { play = value; }
    }

    public bool Cam1Left
    {
        get { return cam1Left;}
        set { cam1Left = value;}
    }

    private void Start()
    {
        actualAspect = (float)Screen.height / (float)Screen.width;
        transform.localScale = new Vector3(16/(actualAspect *9), 16/(actualAspect *9), 1);
        viewHeight = 0.22f;// *1.7777f / actualAspect;
        screen1.localScale = new Vector3(screen1.localScale.x, screen1.localScale.y, screen1.localScale.z * actualAspect / 1.7777f);
        screen2.localScale = new Vector3(screen2.localScale.x, screen2.localScale.y, screen2.localScale.z * actualAspect / 1.7777f);
        //screen1.localPosition = new Vector3(screen1.localPosition.x, screen1.localPosition.y, screen1.localPosition.z);
    }
    
    private void Update()
    {
        y = (scroll.localPosition.y - 65) / (1150 / (1.7777f / actualAspect));
        
        if (play)//Анимация
        {
            load.SetActive(false);
            screen1.localPosition = new Vector3(screen1.localPosition.x - Time.deltaTime * speed, screen1.localPosition.y ,
                                                screen1.localPosition.z);

            screen2.localPosition = new Vector3(screen2.localPosition.x - Time.deltaTime * speed, screen2.localPosition.y ,
                                                screen2.localPosition.z);
            

            if (screen1.localPosition.x < -1.13f)
            {
                screen1.localPosition = new Vector3(1.13f, screen1.localPosition.y, screen1.localPosition.z);
                screen2.localPosition = new Vector3(0, screen1.localPosition.y, screen1.localPosition.z);
                play = false;
                textController.ButtonsActivate(true);
                textController.UnloadPref();
                textController.NumQuestion++;
                camera1.rect = (new Rect(1.04f, y + 0.59f, 0.92f, viewHeight));
                camera2.rect = (new Rect(0.04f, y + 0.59f, 0.92f, viewHeight));
            }

            if (screen2.localPosition.x < -1.13f)
            {
                screen2.localPosition = new Vector3(1.13f, screen2.localPosition.y, screen2.localPosition.z);
                screen1.localPosition = new Vector3(0, screen1.localPosition.y, screen1.localPosition.z);
                textController.UnloadPref();
                play = false;
                textController.ButtonsActivate(true);
                textController.NumQuestion++;
                camera2.rect = (new Rect(1.04f, y + 0.59f, 0.92f, viewHeight));
                camera1.rect = (new Rect(0.04f, y + 0.59f, 0.92f, viewHeight));
            }
        }
        screen1.localPosition = new Vector3(screen1.localPosition.x, scroll.localPosition.y / 576 - 0.11f /*+ (1.77778f - actualAspect) * 0.11f*/, screen1.localPosition.z);
        screen2.localPosition = new Vector3(screen2.localPosition.x, scroll.localPosition.y / 576 - 0.11f /*+ (1.77778f - actualAspect) * 0.11f*/, screen2.localPosition.z);

        sprite1.localPosition = new Vector3(screen1.localPosition.x * 620 / 1.13f, scroll.localPosition.y - 65,
                                                sprite1.localPosition.z);
        sprite2.localPosition = new Vector3(screen2.localPosition.x * 620 / 1.13f, scroll.localPosition.y - 65,
                                            sprite2.localPosition.z);
        if (camera1 != null)
            camera1.rect = (new Rect(screen1.localPosition.x / 1.13f + 0.04f, y + 0.59f, 0.92f, viewHeight));
        if (camera2 != null)
            camera2.rect = (new Rect(screen2.localPosition.x / 1.13f + 0.04f, y + 0.59f, 0.92f, viewHeight));
        
    }
}
