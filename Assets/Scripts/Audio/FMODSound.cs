using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODSound : MonoBehaviour
{
    private StudioEventEmitter sound;
    public ParamRef[] param;
    
    
    // Start is awake
    void Awake()
    {
        sound = GetComponent<StudioEventEmitter>();

        if(sound){
            param = sound.Params; // dump parameters here initially
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeParameter(string name, int value){
        sound.SetParameter(name, value);
    }

    public void Play(){
        if(!sound.IsPlaying())
            sound.Play();
    }

    public void Stop(){
        sound.Stop();
    }
}
