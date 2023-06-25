using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class HeightAdaptaion : MonoBehaviour
{
    public Slider ySlider;
    public Slider ySlider2;
    public float minY = 0.4f;
    public float maxY = 1.2f;

    private void Start()
    {
        maxY = maxY+ transform.position.y;
        minY = (minY + transform.position.y)*-1;
        ySlider.maxValue = maxY;
        ySlider2.maxValue = maxY;
        ySlider2.minValue = minY;
        ySlider.minValue = minY;
        ySlider2.value = transform.position.y;
        // Set initial Y position based on the slider value
        ySlider.onValueChanged.AddListener(OnSliderValueChanged);
        ySlider2.onValueChanged.AddListener(OnSliderValueChanged2);
    }

    private void OnSliderValueChanged(float value)
    {
        Debug.Log(value);
        // Map the slider value to the desired Y position range
        //float newY = Mathf.Lerp(minY, maxY, value);

        // Update the Y position of the game object
        Vector3 newPosition = transform.position;
        newPosition.y = value;
        transform.position = newPosition;
    }
    private void OnSliderValueChanged2(float value)
    {
        // Map the slider value to the desired Y position range
        //float newY = Mathf.Lerp(minY, maxY, value);

        // Update the Y position of the game object
        Vector3 newPosition = transform.position;
        newPosition.y = value;
        transform.position = newPosition;
    }

    public void onMinusPressed()
    {
        ySlider.value = ySlider.value - 0.01f;
        ySlider2.value = ySlider2.value - 0.01f;
    }
    public void onPlusPressed()
    {
        ySlider.value = ySlider.value + 0.01f;
        ySlider2.value = ySlider2.value + 0.01f;
    }
}
