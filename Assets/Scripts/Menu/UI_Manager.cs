using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PanelsType
{
    None,
    Main,
    Option,
    Slide
}

public class UI_Manager : MonoBehaviour
{
    public Dictionary<PanelsType, GameObject> panels= new Dictionary<PanelsType, GameObject>();
    public string sceneToLoadOnPlay;

    private void Start()
    {
        MenuPanel[] allPanels = GameObject.FindObjectsOfType<MenuPanel>();

        for (int i = 0; i < allPanels.Length; i++)
        {
            panels.Add(allPanels[i].type, allPanels[i].gameObject);
            panels[allPanels[i].type].SetActive(false);
        }

        Debug.Log(allPanels.Length);
        Debug.Log(panels.Count);
        panels[PanelsType.Main].SetActive(true);

    }

    public void OnPlay()
    {
        SceneManager.LoadScene(sceneToLoadOnPlay);
    }

    public void OnOption()
    {
        panels[PanelsType.Option].SetActive(true);
        panels[PanelsType.Main].SetActive(false);
    }

    public void OnSlide() 
    {
        panels[PanelsType.Main].SetActive(false);
        panels[PanelsType.Slide].SetActive(true);
    }

    public void GoToMain()
    {
        panels[PanelsType.Main].SetActive(true);
        panels[PanelsType.Slide].SetActive(false);
    }
}
