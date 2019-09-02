using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // singleton
    public static InputManager IM;

    public KeyCode walk { get; set; }
    public KeyCode store { get; set; }

    void Awake()
    {
        // singleton pattern
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }
        
        /*Assign each keycode when the game starts.
         * Loads data from PlayerPrefs so if a user quits the game,
         * their bindings are loaded next time. Default values
         * are assigned to each Keycode via the second parameter
         * of the GetString() function
         */
        walk = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("walkKey", "Mouse1"));
        store = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("storeKey", "P"));
    }
}
