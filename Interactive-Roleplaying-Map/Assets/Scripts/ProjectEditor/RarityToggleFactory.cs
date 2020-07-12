using System;
using UnityEngine;

public class RarityToggleFactory : MonoBehaviour
{
    public VisualNodeFilter VisualNodeFilter;
    public RarityToggleButton ToggleButtonPrefab;
    public Transform ToggleButtonContainer;

    // Start is called before the first frame update
    private void Start()
    {
        int[] rarities = (int[]) Enum.GetValues(typeof(NodeRarity));

        foreach(int r in rarities)
        {
            RarityToggleButton button = Instantiate(ToggleButtonPrefab, ToggleButtonContainer);
            button.Initialize(VisualNodeFilter.FilterRarity, r);
        }
    }
}
