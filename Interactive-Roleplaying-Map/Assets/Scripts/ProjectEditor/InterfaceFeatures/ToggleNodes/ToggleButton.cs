using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class ToggleButton : MonoBehaviour
{
    public Color32 ColorClicked;
    public Color32 ColorUnclicked;
    public Image ChildImage;
    public EnumeratedColors Colors;
    public string EnumType;

    private Button button;
    private Image image;

    protected Action<Type, int, bool> onClickAction;
    protected bool isClicked = true;
    protected int value;
    protected Type toggleType;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        toggleType = Type.GetType(EnumType);
        ChildImage.color = Colors.GetColor(value);
        button.onClick.AddListener(OnClick);
    }

    public virtual void Initialize(Action<Type, int, bool> onClick, int value)
    {
        this.onClickAction = onClick;
        this.value = value;
    }

    protected virtual void OnClick()
    {
        isClicked = !isClicked;
        image.color = isClicked ? ColorClicked : ColorUnclicked;
        onClickAction.Invoke(toggleType, value, isClicked);
    }
}
