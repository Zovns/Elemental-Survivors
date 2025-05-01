using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private RectTransform bar;
    private RectTransform backgroundRect;
    [Range(0f, 1f)]
    private float size;
    public float Size
    {
        get { return size; }
        set {
            size = value;
            Debug.Log( value );
            //bar.localScale = new Vector3(size, 1, 1);
            
            double backgroundWidth = backgroundRect.sizeDelta.x;
            double decreasedBackgroundWidth = backgroundWidth - backgroundWidth * .1;
            bar.sizeDelta =  new Vector2( value * (float)decreasedBackgroundWidth,bar.sizeDelta.y);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        backgroundRect = GetComponent<RectTransform>();
    }
    void Start()
    {
        Size = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
