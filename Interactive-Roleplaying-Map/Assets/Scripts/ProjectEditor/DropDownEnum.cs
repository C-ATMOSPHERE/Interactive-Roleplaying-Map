using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DropDownEnum : MonoBehaviour
{
    public string Enumerator;

    // Start is called before the first frame update
    private void Start()
    {
        Dropdown dropdown = GetComponent<Dropdown>();

        Type t = Type.GetType(Enumerator);
        Array values = Enum.GetValues(t);

        dropdown.options.Clear();

        foreach(var element in values)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(element.ToString());
            dropdown.options.Add(optionData);
        }
    }
}
