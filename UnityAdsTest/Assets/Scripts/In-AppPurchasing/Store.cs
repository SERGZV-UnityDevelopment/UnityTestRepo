using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    [SerializeField] private int _coins;
    [SerializeField] private Button _nonConsumableButton;
    [SerializeField] private TextMeshProUGUI _buttonText;
    
    private void Start()
    {
        text.text = _coins.ToString();
    }

    public void OnReusablePurchaseButton()
    {
        _coins += 10;
        text.text = _coins.ToString();
    }

    public void OnOneTimePurchaseButton()
    {
        _nonConsumableButton.interactable = false;
        _buttonText.text = "Купленно";
    }
}
