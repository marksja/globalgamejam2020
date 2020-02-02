using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderTest : MonoBehaviour
{
    public float heat;
    public float melt;

    public float heatRate;
    public float meltRate;
    public float heatInertia;
    public float meltInertia;
    public float heatInertiaMax;
    public float meltInertiaMax;
    public float heatInertiaMin;
    public float meltInertiaMin;

    public bool isSoldering;
    public bool isHeating;


    public GameObject heatCube;
    public GameObject meltSphere;

    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        heat = 0;
        melt = 0;
        heatRate = 0;
        meltRate = 0;
        heatInertia = heatInertiaMax;
        meltInertia = meltInertiaMax;

        isSoldering = false;
        isHeating = false;
    }

    // Update is called once per frame
    void Update()
    {       
        //Debug.Log(isSoldering);
        if(Input.GetKey(KeyCode.Space) && !isSoldering){
            //Debug.Log("Space is down");
            isSoldering = true;
            if(!isHeating){ 
                //Debug.Log("Heating...");
                isHeating = true;
                StartCoroutine("applyHeat");
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            isSoldering = false;
        }

        heatCube.transform.localScale = new Vector3(heat + 1, heat + 1, heat + 1);
        meltSphere.transform.localScale = new Vector3(melt + 1, melt + 1, melt + 1);
    }

    private IEnumerator applyHeat(){
        // Set inertia rates
        
        while(isHeating){
            if(isSoldering){
                if(heatInertia != heatInertiaMax)
                    heatRate = 0;
                if(meltInertia != meltInertiaMax)
                    meltRate = 0;

                heatInertia = heatInertiaMax;
                meltInertia = meltInertiaMax;
            }
            if(!isSoldering){
                if(heatInertia != heatInertiaMin)
                    heatRate = 0;
                if(meltInertia != meltInertiaMin)
                    meltRate= 0;
                
                heatInertia = heatInertiaMin;
                meltInertia = meltInertiaMin;
            }
            // Calculate new heat/melt rates
            heatRate += heatInertia;
            meltRate += meltInertia;
            // Apply that heat/melt rate to the overall heat/melt values
            heat += heatRate;
            melt += meltRate;

            if (heat < 0) heat = 0;
            if (melt < 0) melt = 0;

            if(heat == 0 && melt == 0){
                isHeating = false;
            }

            yield return new WaitForSeconds(0.01f);
        }

        heatInertia = heatInertiaMax;
        meltInertia = meltInertiaMax;
        heatRate = 0;
        meltRate = 0;
        
        //Debug.Log("Heat: " + heat + " rate: " + heatRate +  ", Melt: " + melt + " rate: " + meltRate + ", isSoldering: " + isSoldering + ", isHeating: " + isHeating);

        yield return null;
    }
}
