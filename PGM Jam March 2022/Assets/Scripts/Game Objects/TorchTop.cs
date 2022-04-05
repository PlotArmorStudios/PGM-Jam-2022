using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchTop : MonoBehaviour
{
    [SerializeField] private Material torchMaterial;
    [SerializeField] private MeshRenderer objectToChange;
    [SerializeField] private Color startColor;
    [SerializeField] private float startIntensity = 1.016925f;
    [SerializeField] private float endIntensity = 0f;
    [SerializeField] private float blinkOnIntensity = 0.5f;
    [SerializeField] private float blinkOffIntensity = 0f;

    private bool isBlinking = false;
    [SerializeField] private float minTimeBetweenBlink = 0.2f;
    [SerializeField] private float maxTimeBetweenBlink = 0.7f;
    [SerializeField] private float minOnTime = 0.1f;
    [SerializeField] private float maxOnTime = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        objectToChange = GetComponent<MeshRenderer>();
        torchMaterial = objectToChange.material;
        startColor = torchMaterial.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        if (Torch.TorchVolumeWeight > Torch.TorchFractionToAttack)
        {
            isBlinking = false;
            torchMaterial.SetColor("_EmissionColor", startColor * (startIntensity - endIntensity) * Torch.TorchVolumeWeight);
        }
        else if (isBlinking)
        {
            return;
        }
        else
        {
            StartCoroutine(TorchBlink());
        }
    }

    private IEnumerator TorchBlink()
    {
        isBlinking = true;
        while (isBlinking)
        {
            float randomTimeBetweenBlink = Random.Range(minTimeBetweenBlink, maxTimeBetweenBlink);
            torchMaterial.SetColor("_EmissionColor", startColor * blinkOffIntensity);
            yield return new WaitForSeconds(randomTimeBetweenBlink);

            torchMaterial.SetColor("_EmissionColor", startColor * blinkOnIntensity);
            float randomTimeOn = Random.Range(minOnTime, maxOnTime);
            yield return new WaitForSeconds(randomTimeOn);
        }
    }
}
