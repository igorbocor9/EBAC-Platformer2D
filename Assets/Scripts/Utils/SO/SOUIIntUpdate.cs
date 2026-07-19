using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiText;

    void Start()
    {
        uiText.text = soInt.value.ToString();
    }

    void Update()
    {
        uiText.text = soInt.value.ToString();
    }
}
