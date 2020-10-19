using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //public
    [SerializeField] public GameObject TextBox;
    [SerializeField] public Button InputOne;
    [SerializeField] public Button InputTwo;
    [SerializeField] public Button InputThree;
    [SerializeField] public Button InputFour;

    //private
    private List<string[]> options;

    private int EmpireSize;
    private int CitizenHappiness;
    private int EnglishHostility;

    void Awake()
    {
        

        EmpireSize = 10; //Starts off at a low value, the user has to grow or shrink it from here
        CitizenHappiness = 100;
        EnglishHostility = 0;
    }

    private void initOptions()
    {

    }
}
