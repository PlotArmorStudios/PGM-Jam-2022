using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchMeter : MonoBehaviour
{
    [SerializeField] private Image torchMeter;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private Color blinkOnColor;
    [SerializeField] private Color blinkOffColor;
    [SerializeField] private float blinkSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        torchMeter = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        torchMeter.fillAmount = Torch.TorchVolumeWeight;
        if (Torch.TorchVolumeWeight > Torch.TorchFractionToAttack)
        {
            Color lerpedColor = Color.Lerp(endColor, startColor, (1 / (1 - Torch.TorchFractionToAttack))
                * (Torch.TorchVolumeWeight - Torch.TorchFractionToAttack));
            torchMeter.color = lerpedColor;
        }
        else
        {
            torchMeter.color = Color.Lerp(blinkOnColor, blinkOffColor, Mathf.PingPong(Time.time * blinkSpeed, 1));
        }
    }
}
