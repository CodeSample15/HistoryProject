using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoBox : MonoBehaviour
{
    //public
    [SerializeField] public TextMeshProUGUI TextBox;
    public int type;

    //private
    private List<string> infoList;
    private List<Vector2> locations;

    void Awake()
    {
        init_info_data();
        TextBox.SetText(infoList[type]);
    }

    private void init_info_data()
    {
        infoList = new List<string>();
        infoList.Add(""); //Leave this first one here for info boxes that are empty
        infoList.Add(""); //Add in research information about different parts of the Spanish Empire here--------------------------------------------------------------------------------------
    }
}