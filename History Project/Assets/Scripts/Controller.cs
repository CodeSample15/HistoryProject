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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //public
    [SerializeField] public GameObject TextBox;
    [SerializeField] public Button[] Buttons;
    public Prompter prompter;
    public TextMeshProUGUI infoBoxText;
    public InfoController infoController;

    public Animator fadeAnimation;
    public TextMeshProUGUI failMessage;

    public Animator blackFadeAnimation;
    public Prompter endOfGamePrompter;

    public Animator goHomeAnimation;

    public bool gameIsOver;

    //stats for user to see
    public TextMeshProUGUI StatsDisplay;

    //private
    private List<string> events;
    private List<string[]> options;

    private int currentEvent;
    private int maxEvents;

    private int EmpireSize;
    private int CitizenHappiness;
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
    private bool showingStats;

    void Start()
    {
        initLists();
        maxEvents = events.Count;

        currentEvent = -1;

        EmpireSize = 10; //Starts off at a low value, the user has to grow or shrink it from here
        CitizenHappiness = 100;
        NativeHostility = 5;
        Wealth = 100;

        buttonOnePressed = false;
        buttonTwoPressed = false;
        buttonThreePressed = false;
        buttonFourPressed = false;

        eventStarted = false;
        userEntered = false;
        showingStats = false;

        hideButtons(4);

        gameIsOver = false;
        failMessage.SetText("");
    }

    void Update()
    {
        //for the information markers
        if (Input.GetMouseButtonDown(0) && !infoController.infoShowing)
        {
            infoBoxText.SetText("");
        }

        if (Input.GetMouseButtonDown(0) && gameIsOver && endOfGamePrompter.finished)
        {
            StartCoroutine(goHome());
        }

        if (!gameIsOver)
        {
            //for the events
            if (!eventStarted && currentEvent < maxEvents - 1)
            {
                newEvent();
            }
            else if (!(currentEvent < maxEvents - 1))
            {
                //end sequence here
            }

            if (userEntered)
            {
                if (buttonOnePressed)
                    userInputForEvent(1);

                if (buttonTwoPressed)
                    userInputForEvent(2);

                if (buttonThreePressed)
                    userInputForEvent(3);

                if (buttonFourPressed)
                    userInputForEvent(4);

                userEntered = false;
                eventStarted = false;

                buttonOnePressed = false;
                buttonTwoPressed = false;
                buttonThreePressed = false;
                buttonFourPressed = false;

                hideButtons(4);
            }

            if (eventStarted && prompter.finished)
            {
                showButtons(options[currentEvent].Length);
            }

            if (EmpireSize <= 0)
                GameOver();
            if (CitizenHappiness <= 0)
                GameOver();
            if (NativeHostility >= 150)
                GameOver();
            if (Wealth <= 0)
                GameOver();

            showStats();
        }
    }

    IEnumerator goHome()
    {
        goHomeAnimation.SetTrigger("Transition");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(0);
    }

    private void initLists()
    {
        events = new List<string>();
        options = new List<string[]>();

        int option;

        //intro
        events.Add("Welcome to the Spanish Colonization Simulator!");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //0

        events.Add("This game brings you through the major events during Spain's colonization of the Americas.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //1

        events.Add("Before beginning, please note that there are information markers placed around the map on the screen.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //2

        events.Add("You can click on them to get information about important events that took place in those areas.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //3

        events.Add("You will also see on your screen, different stats. These are the current conditions of the Empire you are trying to grow.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //4

        events.Add("When you are ready to play, click play.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Play!"; //5

        events.Add("Spanish Colonization began when Columbus set ashore in the Bahamas in 1492. You must now make the right decisions to grow the empire.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //6

        //End of intro---------------------------------------------------------------------------------------------------------------------------------------------------

        events.Add("1443: Columbus sails back to Spain. Should you start to build your empire on the land that he found?");
        options.Add(new string[2]);
        option = options.Count - 1;
        options[option][0] = "Yes"; //7
        options[option][1] = "No";

        events.Add("Vasco Núñez de Balboa claims the Pacific Ocean in the name of Spain. What should you do about this?");
        options.Add(new string[4]);
        option = options.Count - 1;
        options[option][0] = "Allow it"; //8
        options[option][1] = "Punish him";
        options[option][2] = "Execute him";
        options[option][3] = "Reward him";

        events.Add("The Spanish complete the conquest of Cuba and establish the town of Havana.");
        options.Add(new string[2]);
        option = options.Count - 1;
        options[option][0] = "Send resources to the Colony (Hernando Cortes)"; //9
        options[option][1] = "Sit back and relax";

        events.Add("The year is 1519. Try to conquer Tenochtitlan?");
        options.Add(new string[2]);
        option = options.Count - 1;
        options[option][0] = "Yes"; //10
        options[option][1] = "No";

        events.Add("1525: The conquistadors begin the process of European emigration to America.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //11

        events.Add("In 1531, Francisco Pizarro leads 168 men, into the territory of the Inca Empire.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //12

        events.Add("Pizarro and his tiny force ambush and massacre the Inca court in Cajamarca, capturing Atahualpa himself alive. What should they do with Atahualpa? (He isn't Christian)");
        options.Add(new string[3]);
        option = options.Count - 1;
        options[option][0] = "Excecute him"; //13
        options[option][1] = "Release him";
        options[option][2] = "Hold him captive";

        events.Add("Manco Inca begins a siege of the Spaniards in Cuzco that lasts for a year. Send resources to help?");
        options.Add(new string[2]);
        option = options.Count - 1;
        options[option][0] = "Yes"; //14
        options[option][1] = "No";

        events.Add("It is now 1537 and the Spanish have full control over Peru...");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //15

        events.Add("The Natives aren't doing well in the current encomiendas of Spanish America. What should you do?");
        options.Add(new string[4]);
        option = options.Count - 1;
        options[option][0] = "Use this to your advantage and take over more land."; //16
        options[option][1] = "Pass laws to protect them.";
        options[option][2] = "Ignore this.";
        options[option][3] = "Give them back some land.";

        events.Add("Rich seams of silver are discoverd at Potosi (modern day Bolivia)!");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Continue..."; //17

        events.Add("La Paz is founded on the trade route between Lima and the newly discovered silver mines. It is now 1550 and there is a Spanish goods being imported and metals being exported from Latin America.");
        options.Add(new string[1]);
        option = options.Count - 1;
        options[option][0] = "Finish and see results..."; //18

        /*
         * Order for adding a new event:
             * events.add("(What's happening in the event.) Example: Oh no! some of your crops are damaged, what should you do?");
             * options.add(new string[(depending on how many options you want 1-4)]);
             * int option = options.Count - 1
             * options[option][0] = "Option 1"
             * options[option][1] = "Option 2"
             * etc...
        */
    }

    private void newEvent()
    {
        currentEvent++;
        prompter.clear();
        prompter.printText(events[currentEvent], 0.03f);

        for(int i=0; i<options[currentEvent].Length; i++)
        {
            Buttons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(options[currentEvent][i]);
        }

        eventStarted = true;
    }

    private void userInputForEvent(int userInput)
    {
        Debug.Log(userInput);
        switch(currentEvent) 
        {
            //intro (cases 0-6 are mainly here to make programming easier on my brain... Not needed except for case 1 and 3)
            case 0:
                break;

            case 1:
                infoController.Show_Markers();
                break;

            case 2:
                break;

            case 3:
                showingStats = true;
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                switch (userInput)
                {
                    case 1:
                        break;
                    case 2:
                        GameOver();
                        break;
                }

                break;

            case 8:
                switch(userInput)
                {
                    case 1:
                        break;

                    case 2:
                        CitizenHappiness -= 5;
                        break;

                    case 3:
                        CitizenHappiness -= 10;
                        break;

                    case 4:
                        CitizenHappiness += 5;
                        Wealth += 5;
                        break;
                }

                break;

            case 9:
                NativeHostility += 10;

                switch (userInput)
                {
                    case 1:
                        EmpireSize += 600;
                        CitizenHappiness += 15;
                        Wealth -= 50;
                        break;

                    case 2:
                        CitizenHappiness -= 20;
                        EmpireSize -= 5;
                        Wealth += 30;
                        break;
                }
                break;

            case 10:
                switch (userInput)
                {
                    case 1:
                        CitizenHappiness += 1;
                        EmpireSize -= 200;
                        NativeHostility += 10;
                        Wealth -= 20;
                        break;

                    case 2:
                        CitizenHappiness -= 20;
                        Wealth -= 30;
                        NativeHostility -= 20;
                        break;

                }

                break;

            case 11:
                EmpireSize += 30;
                CitizenHappiness += 5;
                NativeHostility += 5;
                break;

            case 12:
                NativeHostility += 20;
                break;

            case 13:
                Wealth += 10;

                switch(userInput)
                {
                    case 1:
                        NativeHostility += 40;
                        CitizenHappiness += 5;
                        break;

                    case 2:
                        NativeHostility -= 40;
                        CitizenHappiness -= 50;
                        break;

                    case 3:
                        NativeHostility += 20;
                        CitizenHappiness -= 10;
                        break;
                }

                break;

            case 14:
                switch (userInput)
                {
                    case 1:
                        CitizenHappiness += 10;
                        Wealth -= 20;
                        break;

                    case 2:
                        CitizenHappiness -= 8;
                        EmpireSize -= 90;
                        break;
                }

                break;

            case 15:
                CitizenHappiness += 20;
                NativeHostility += 5;
                EmpireSize += 30;
                Wealth += 10;
                break;

            case 16:
                switch (userInput)
                {
                    case 1:
                        EmpireSize += 40;
                        CitizenHappiness += 5;
                        NativeHostility -= 30;
                        Wealth -= 100;
                        break;

                    case 2:
                        CitizenHappiness -= 5;
                        NativeHostility -= 10;
                        break;

                    case 3:
                        NativeHostility += 10;
                        break;

                    case 4:
                        CitizenHappiness -= 50;
                        Wealth -= 70;
                        NativeHostility -= 60;
                        break;
                }

                break;

            case 17:
                Wealth += 20;
                EmpireSize += 30;
                break;

            case 18:
                GameResults();
                break;
        }
    }

    private void GameOver()
    {
        Debug.Log("gameover");
        fadeAnimation.SetTrigger("Fade"); //start animation
        failMessage.SetText("Game Over! \n You failed to grow the Spanish Empire \nClick to return to home.");
        gameIsOver = true;
    }

    private void GameResults()
    {
        StartCoroutine(DoEndOfGame());
    }

    IEnumerator DoEndOfGame()
    {
        blackFadeAnimation.SetTrigger("Fade");

        yield return new WaitForSeconds(0.8f);

        string resultsString = "";
        int score = 0;

        #region Calculating Score
        if (EmpireSize > 10)
        {
            resultsString += "Your empire grew by " + (EmpireSize - 10) + "! + 25 points!\n\n";
            score += 25;
        }
        else
        {
            resultsString += "Your empire did not grow...\n\n";
        }

        if (CitizenHappiness > 100)
        {
            resultsString += "Your empire's citizens got happier by " + (CitizenHappiness - 100) + "! + 25 points!\n\n";
            score += 25;
        }
        else
        {
            resultsString += "Your empire's citizens did not get happier...\n\n";
        }

        if (NativeHostility < 80)
        {
            resultsString += "The natives' anger towards your colony was less than 80! + 25 points\n\n";
            score += 25;
        }
        else
        {
            resultsString += "The natives became way too angry at your colonies...\n\n";
        }

        if (Wealth > 100)
        {
            resultsString += "You gained +" + (Wealth - 100) + " in your empire's wealth! + 25 points!\n\n";
            score += 25;
        }
        else
        {
            resultsString += "You did not gain any wealth from your empire...\n\n";
        }
        #endregion

        resultsString += "Total points: " + score + " /100!\nClick anywhere to return to the home screen...";
        endOfGamePrompter.printText(resultsString, 0.01f);
        gameIsOver = true;
    }

    private void showStats()
    {
        if (showingStats)
        {
            string statsDisplayString = "";

            statsDisplayString += "Empire size: " + EmpireSize + "\n";
            statsDisplayString += "Citizen happiness: " + CitizenHappiness + "\n";
            statsDisplayString += "Native American threat level: " + NativeHostility + "\n";
            statsDisplayString += "Wealth: " + Wealth;

            StatsDisplay.SetText(statsDisplayString);
        }
        else
        {
            StatsDisplay.SetText("");
        }
    }

    private void hideButtons(int options)
    {
        for(int i=0; i<options; i++)
        {
            if(Buttons[i].transform.position.y < 100)
            {
                Buttons[i].transform.position = new Vector2(Buttons[i].transform.position.x, Buttons[i].transform.position.y + 400);
            }
        }
    }

    private void showButtons(int options)
    {
        for(int i=0; i<options; i++)
        {
            if(Buttons[i].transform.position.y > 100)
            {
                Buttons[i].transform.position = new Vector2(Buttons[i].transform.position.x, Buttons[i].transform.position.y - 400);
            }
        }
    }

    public void ButtonOnePress()
    {
        buttonOnePressed = true;
        userEntered = true;
    }

    public void ButtonTwoPress()
    {
        buttonTwoPressed = true;
        userEntered = true;
    }

    public void ButtonThreePress()
    {
        buttonThreePressed = true;
        userEntered = true;
    }

    public void ButtonFourPress()
    {
        buttonFourPressed = true;
        userEntered = true;
    }
}