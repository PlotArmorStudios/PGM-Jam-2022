using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchTop : MonoBehaviour
{
    [SerializeField] private Material torchMaterial;
    [SerializeField] private Renderer objectToChange;
    [SerializeField] private Color startColor;
    [SerializeField] private float startIntensity = 1.016925f;
    [SerializeField] private float endIntensity = 0f;
    [SerializeField] private float blinkOnIntensity = 0.5f;
    [SerializeField] private float blinkOffIntensity = 0f;
    [SerializeField] private float blinkSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        torchMaterial = objectToChange.GetComponent<Renderer>().material;
        startColor = torchMaterial.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        if (Torch.TorchVolumeWeight > Torch.TorchFractionToAttack)
        {
            torchMaterial.SetColor("_EmissionColor", startColor * (startIntensity - endIntensity) * Torch.TorchVolumeWeight);
        }
        else
        {
            torchMaterial.SetColor("_EmissionColor", startColor * Mathf.Lerp(blinkOffIntensity, blinkOnIntensity, Mathf.PingPong(Time.time * blinkSpeed, 1)));
        }
    }
}
