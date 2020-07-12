using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class RarityToggleButton : MonoBehaviour
{
    public Color32 ColorClicked;
    public Color32 ColorUnclicked;
    public Image ChildImage;

    private Button button;
    private Image image;
    private bool isClicked = true;
    private int rarity;
    private Action<int, bool> onClickAction;


    public void Initialize(Action<int, bool> onClick, int rarity)
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(OnClick);

        onClickAction = onClick;
        this.rarity = rarity;
        ChildImage.color = RarityColors.GetColor(rarity);
    }

    private void OnClick()
    {
        isClicked = !isClicked;
        image.color = isClicked ? ColorClicked : ColorUnclicked;
        onClickAction.Invoke(rarity, isClicked);
    }
}
