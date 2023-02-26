using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject winText = default;
              
    private void Start()
    {
        Instance = this;
        winText.SetActive(false);

        EnemySurveillance.OnKill += CheckWin;
        Win();
    }

    private void CheckWin()
    {
        print("la team a changé");

        List<GameObject> robotList = new List<GameObject>();
        robotList = GameObject.FindGameObjectsWithTag("Robot").ToList<GameObject>();

        for (int i = 1; i < robotList.Count; i++)
        {
            if(robotList[i].GetComponent<EnemyBehavior>().team == robotList[i - 1].GetComponent<EnemyBehavior>().team)
            {
                if (i == robotList.Count - 1) Win();
            }
            else
            {
                break;
            }
        }
    }

    private void Win()
    {
        winText.SetActive(true);
        TextMeshProUGUI text;

        if(winText.TryGetComponent<TextMeshProUGUI>(out text))
        {
            print("yey");
            text.SetText("La Team Des Gentils Gagne !");
        }

        EnemySurveillance.OnKill -= CheckWin;
    }
}