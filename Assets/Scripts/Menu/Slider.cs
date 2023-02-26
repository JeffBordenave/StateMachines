using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public GameObject[] slides;
    private int currentSlide = 0;

    private void Start()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);
        }

        currentSlide= 0;
        slides[currentSlide].SetActive(true);
    }

    public void OnNext()
    {
        slides[currentSlide].SetActive(false);
        currentSlide++;
        if(currentSlide >= slides.Length) currentSlide= 0;
        slides[currentSlide].SetActive(true);
    }
}
