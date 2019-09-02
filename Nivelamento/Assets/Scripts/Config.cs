using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;
    bool waitingForKey, pressedKey;

    void Start()
    {
        menuPanel = transform.Find("InputPanel");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;
        pressedKey = false;

        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "WalkButton")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.walk.ToString();
            }
            else if (menuPanel.GetChild(i).name == "StoreButton")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.store.ToString();
            }
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(false);
        }
    }

    void OnGUI()
    {
        /*keyEvent dictates what key our user presses
         * bt using Event.current to detect the current
         * event
         */
        keyEvent = Event.current;
        
        //Executes if a button gets pressed and
        //the user presses a key
        if(waitingForKey)
        {
            if (keyEvent.isKey || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    newKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse0");
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    newKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse1");
                }
                else newKey = keyEvent.keyCode; //Assigns newKey to the key user presses

                pressedKey = false;
                waitingForKey = false;
            }
        }
        
    }
    
    /*Buttons cannot call on Coroutines via OnClick().
     * Instead, we have it call StartAssignment, which will
     * call a coroutine in this script instead, only if we
     * are not already waiting for a key to be pressed.
     */
    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }
    
    //Assigns buttonText to the text component of
    //the button that was pressed
    public void SendText(Text text)
    {
        buttonText = text;
    }
    
    //Used for controlling the flow of our below Coroutine
    IEnumerator WaitForKey()
    {
        while (pressedKey)
            yield return null;
    }
    
    /*AssignKey takes a keyName as a parameter. The
     * keyName is checked in a switch statement. Each
     * case assigns the command that keyName represents
     * to the new key that the user presses, which is grabbed
     * in the OnGUI() function, above.
     */
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        pressedKey = true;
        yield return WaitForKey(); //Executes endlessly until user presses a key
        
        switch (keyName)
        {
            case "walk":
                InputManager.IM.walk = newKey; //Set walk to new keycode
                buttonText.text = InputManager.IM.walk.ToString(); //Set button text to new key
                PlayerPrefs.SetString("walkKey", InputManager.IM.walk.ToString()); //save new key to PlayerPrefs
                break;

            case "store":
                InputManager.IM.store = newKey; //set basic atk to new keycode
                buttonText.text = InputManager.IM.store.ToString(); //set button text to new key
                PlayerPrefs.SetString("storeKey", InputManager.IM.store.ToString()); //save new key to PlayerPrefs
                break;
        }
        yield return null;
    }
}
