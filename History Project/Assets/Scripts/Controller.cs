/*
 *Author: Luke Crimi
 *Controller.cs
 *10/19/2020
 *
 *In this game, you play as the king of Spain and you make decisions for your growing empire. Your decisions affect the way your empire grows, and how the people living in your empire
 *react to your leadership.
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //public
    [SerializeField] public GameObject TextBox;
    [SerializeField] public Button[] Buttons;
    public Prompter prompter;
    public TextMeshProUGUI infoBoxText;
    public InfoController infoController;

    public int Year;

    //private
    private List<string> events;
    private List<string[]> options;

    private int currentEvent;

    private int EmpireSize;
    private int CitizenHappiness;
    private int Population;
    private int ColonyStrength;
    private int NativeHostility;
    private int Wealth;

    //button logic
    private bool buttonOnePressed;
    private bool buttonTwoPressed;
    private bool buttonThreePressed;
    private bool buttonFourPressed;

    //update logic
    private bool eventStarted;
    private bool userEntered;

    void Awake()
    {
        initLists();

        currentEvent = -1;

        EmpireSize = 10; //Starts off at a low value, the user has to grow or shrink it from here
        CitizenHappiness = 100;
        Population = 0;
        ColonyStrength = 1;
        NativeHostility = 5;
        Wealth = 0;

        buttonOnePressed = false;
        buttonTwoPressed = false;
        buttonThreePressed = false;
        buttonFourPressed = false;

        eventStarted = false;
        userEntered = false;

        hideButtons(4);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !infoController.infoShowing)
        {
            infoBoxText.SetText("");
        }
    }

    private void initLists()
    {
        events = new List<string>();
        options = new List<string[]>();

        /*
         * Order for adding a new event:
             * events.add("(What's happening in the event.) Example: Oh no! some of your crops are damaged, what should you do?");
             * options.add(new string[(depending on how many options you want 1-4)]);
             * option = options.Count - 1
             * options[option][0] = "Option 1"
             * options[option][1] = "Option 2"
             * etc...
        */
    }

    private void newEvent()
    {
        currentEvent++;
        prompter.printText(events[currentEvent], 0.01f);
    }

    private void userInputForEvent(int userInput)
    {
        switch(currentEvent) 
        {
            case 0:
                switch(userInput)
                {
                    case 0:
                        //change variable here
                        break;
                    case 1:
                        break;
                }

                break;
            

        }
    }

    private void hideButtons(int options)
    {
        for(int i=0; i<options; i++)
        {
            if(Buttons[i].transform.position.y < 300)
            {
                Buttons[i].transform.position = new Vector2(Buttons[i].transform.position.x, Buttons[i].transform.position.y + 500);
            }
        }
    }

    private void showButtons(int options)
    {
        for(int i=0; i<options; i++)
        {
            if(Buttons[i].transform.position.y > 300)
            {
                Buttons[i].transform.position = new Vector2(Buttons[i].transform.position.x, Buttons[i].transform.position.y - 500);
            }
        }
    }

    public void ButtonOnePress()
    {
        buttonOnePressed = true;
    }

    public void ButtonTwoPress()
    {
        buttonTwoPressed = true;
    }

    public void ButtonThreePress()
    {
        buttonThreePressed = true;
    }

    public void ButtonFourPress()
    {
        buttonFourPressed = true;
    }
}