using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep3 : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string selectsound;
    public FMOD.Studio.EventInstance soundevent;

    //To decide which button to play the sound
    public KeyCode presstoplaysound;

    // Start is called before the first frame update
    void Start()
    {
        //intialising the soundevent
        soundevent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
    }

    // Update is called once per frame
    void Update()
    {
        //this listens to the object transform position and rigidbody
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundevent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        Playsound();
    }

    void Playsound()
    {
        //if statement to play the sound when pressing the button
        if (Input.GetKey(presstoplaysound))
        {
            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            soundevent.getPlaybackState(out fmodPbState);
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundevent.start();
            }
        }
        if (Input.GetKeyUp(presstoplaysound))
        {
            soundevent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

}