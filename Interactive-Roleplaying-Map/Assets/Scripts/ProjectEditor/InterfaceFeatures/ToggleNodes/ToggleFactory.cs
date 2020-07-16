using System;
using UnityEngine;

public class ToggleFactory : MonoBehaviour
{
    public VisualNodeFilter VisualNodeFilter;
    public string[] ToggleButtonTypes;
    public ToggleButton[] ToggleButtonPrefabs;
    public Transform ToggleButtonContainer;

    // Start is called before the first frame update
    private void Start()
    {
        for(int i = 0; i < ToggleButtonTypes.Length; i++)
        {
            SpawnToggleButtons(ToggleButtonTypes[i], ToggleButtonPrefabs[i]);
        }
    }

    private void SpawnToggleButtons(string serializedType, ToggleButton prefab)
    {
        Type type = Type.GetType(serializedType);
        int[] values = (int[]) Enum.GetValues(type);

        for (int i = 0; i < values.Length; i++)
        {
            ToggleButton button = Instantiate(prefab, ToggleButtonContainer);
            button.Initialize(VisualNodeFilter.Filter, i);
        }
    }
}
