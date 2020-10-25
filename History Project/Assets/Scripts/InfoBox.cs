using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    //public
    [SerializeField] public InfoController infoController;
    [SerializeField] public TextMeshProUGUI TextBox;
    public int type;

    //private
    private Button button;

    private List<string> infoList;
    private List<Vector2> locations;

    void Start()
    {
        init_info_data();
        init_marker_locations();
        TextBox.SetText(infoList[type]);

        transform.position = locations[type];
        button = GetComponent<Button>();
        button.onClick.AddListener(getInfo);

        TextBox.SetText("");
    }

    public void Update()
    {
        Debug.Log(transform.position.x + "    " + transform.position.y);
    }

    private void init_info_data()
    {
        infoList = new List<string>();
        infoList.Add("This is a test"); //Pacific ocean
    }

    private void init_marker_locations()
    {
        locations = new List<Vector2>();

        locations.Add(new Vector2(-381f, 150f)); //Pacific ocean
    }

    public void getInfo()
    {

        TextBox.SetText(infoList[type]);
        TextBox.gameObject.transform.position = new Vector2(locations[type].x, locations[type].y + 5);
    }

    IEnumerator showingInfo()
    {
        infoController.infoShowing = true;

        yield return new WaitForSeconds(0.1f);

        infoController.infoShowing = false;
    }
}