using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.IO;

public class RepairManager : MonoBehaviour
{
    public PlaceableGrid bag;
    public PlaceableGrid phone;
    public PlaceableGrid wiretapGrid;

    public int numBrokenParts;
    public List<GameObject> partsList;
    public List<string> partsListS;
    public GameObject bugToSpawn;

    public List<CircuitLocation> locationInfo;

    public UnityEngine.Events.UnityEvent onPuzzleComplete;
    bool puzzleComplete = false;

    List<CircuitPiece> circuitPieces;

    public GameObject firstInstruction, secondInstruction, finalInstruction;

    public StudioEventEmitter pickup, place;

    // Start is called before the first frame update
    void Start()
    {
        circuitPieces = new List<CircuitPiece>();

        
    }
    
    void Update()
    {
        AttemptToFinishPuzzle();
    }

    [ContextMenu("Create Puzzle")]
    public void CreatePuzzle()
    {
        Shuffle(ref partsListS);
        int partListIndex = 0;

        Vector3 tempscaleholderbecausebullshit;

        Debug.Log("GetNumGridLocations: " + phone.underlyingGrid.GetNumGridLocations());

        for(int i = 0; i < phone.underlyingGrid.GetNumGridLocations(); ++i)
        {
            if(partListIndex >= partsListS.Count)
            {
                partListIndex = partListIndex % partsListS.Count;
            }
            
            while(locationInfo.Count > i && locationInfo[i].disallowedParts.Contains(partListIndex+1))
            {
                partListIndex++;
                if(partListIndex >= partsListS.Count)
                {
                    partListIndex = partListIndex % partsListS.Count;
                }
            }

            //Spawn a correct part and a missing part at each location
            //GameObject part = Instantiate(partsList[partListIndex], Vector3.back * 0.4f, Quaternion.identity);
            Debug.Log(partsListS[partListIndex]);
            GameObject part = Instantiate(Resources.Load(partsListS[partListIndex]) as GameObject, Vector3.back * 0.4f, Quaternion.identity);
            if(part == null)
                Debug.Log("Part " + partsList[partListIndex] + " failed to instantiate.");
            //GameObject placement = Instantiate(partsList[partListIndex], Vector3.back * 0.3f, Quaternion.identity);
            GameObject placement = Instantiate(Resources.Load(partsListS[partListIndex]) as GameObject, Vector3.back * 0.3f, Quaternion.identity);

            CircuitPiece partPiece = part.GetComponent<CircuitPiece>();
            CircuitPiece placementPiece = placement.GetComponent<CircuitPiece>();

            partPiece.type = GrabbableObjectType.Part;
            partPiece.disableGrabbing = true;
            placementPiece.type = GrabbableObjectType.Missing;
            placementPiece.disableGrabbing = true;

            //Place them both on the grid
            tempscaleholderbecausebullshit = partPiece.transform.localScale;
            phone.PlaceObjectAt<CircuitPiece>(partPiece, i);
            partPiece.transform.localPosition += Vector3.back * 0.4f;
            partPiece.transform.localScale = tempscaleholderbecausebullshit;

            tempscaleholderbecausebullshit = placementPiece.transform.localScale;
            phone.PlaceObjectAt<CircuitPiece>(placementPiece, i);
            placementPiece.transform.localPosition += Vector3.back * 0.4f;
            placementPiece.transform.localScale = tempscaleholderbecausebullshit;

            partPiece.SetGameObjectSprite();
            placementPiece.SetGameObjectSprite();

            circuitPieces.Add(partPiece);

            partListIndex++;
        }
        
        Shuffle(ref circuitPieces);
        int j = 0;
        for(; j < numBrokenParts; ++j)
        {
            if(j < circuitPieces.Count)
            {
                //Replace the part here with a broken one
                circuitPieces[j].type = GrabbableObjectType.Broken;
                circuitPieces[j].disableGrabbing = false;
                circuitPieces[j].SetGameObjectSprite();

                //And spawn a correct one in the bag
                GameObject correctPart = Instantiate(circuitPieces[j].gameObject, Vector3.zero, Quaternion.identity);
                CircuitPiece piece = correctPart.GetComponent<CircuitPiece>();
                piece.type = GrabbableObjectType.Part;
                piece.SetGameObjectSprite();
                //TODO: Replace this with placing it on the table
                tempscaleholderbecausebullshit = piece.transform.localScale;
                bag.PlaceObjectAt<CircuitPiece>(piece, j);
                piece.transform.localPosition += Vector3.back * 0.4f;
                piece.transform.localScale = tempscaleholderbecausebullshit;
            }
        }

        GameObject bug = Instantiate(bugToSpawn, Vector3.zero, Quaternion.identity);
        GameObject bugHolder = Instantiate(bugToSpawn, Vector3.zero, Quaternion.identity);
        CircuitPiece bugGrabbable = bug.GetComponent<CircuitPiece>();
        CircuitPiece bugHolderGrabbable = bugHolder.GetComponent<CircuitPiece>();

        bugHolderGrabbable.disableGrabbing = true;
        bugHolderGrabbable.partType = 0;
        bugHolderGrabbable.type = GrabbableObjectType.Missing;
        bugHolderGrabbable.SetGameObjectSprite();

        bugGrabbable.disableGrabbing = false;
        bugGrabbable.partType = 0;
        bugGrabbable.type = GrabbableObjectType.Part;
        bugGrabbable.SetGameObjectSprite();

        tempscaleholderbecausebullshit = bugGrabbable.transform.localScale;
        bag.PlaceObjectAt<CircuitPiece>(bugGrabbable, j);
        bugGrabbable.transform.localPosition += Vector3.back * 0.4f;
        bugGrabbable.transform.localScale = tempscaleholderbecausebullshit;

        tempscaleholderbecausebullshit = bugHolderGrabbable.transform.localScale;
        wiretapGrid.PlaceObjectAt<CircuitPiece>(bugHolderGrabbable, 0);
        bugHolderGrabbable.transform.localPosition += Vector3.back * 0.4f;
        bugHolderGrabbable.transform.localScale = tempscaleholderbecausebullshit;
    }

    public void AttemptToFinishPuzzle()
    {
        if(!puzzleComplete && VerifyPuzzle())
        {
            Debug.Log("Yay");
            onPuzzleComplete.Invoke();
            puzzleComplete = true;
        }
    }

    public bool VerifyPuzzle()
    {
        int numBroken = phone.GetNumberOfBrokenObjectsInGrid();
        int numCorrect = phone.GetNumberOfCorrectObjectsInGrid();
        int numBugs = wiretapGrid.GetNumberOfCorrectObjectsInGrid();

        if(numBroken > 0)
        {
            //Debug.Log("Still broken");
            firstInstruction.SetActive(true);
            secondInstruction.SetActive(false);
            finalInstruction.SetActive(false);
            return false;
        }

        if(numCorrect != circuitPieces.Count)
        {
            //Debug.Log("Circuit pieces missing");
            firstInstruction.SetActive(true);
            secondInstruction.SetActive(false);
            finalInstruction.SetActive(false);
            return false;
        }

        if(numBugs != 1)
        {
            //Debug.Log("Missing bugs");
            firstInstruction.SetActive(false);
            secondInstruction.SetActive(true);
            finalInstruction.SetActive(false);
            return false;
        }

        firstInstruction.SetActive(false);
        secondInstruction.SetActive(false);
        finalInstruction.SetActive(true);

        return true;
    }

    public void Shuffle<T>(ref List<T> list)  
    {  
        if(list.Count == null)
            Debug.LogError("No data in list.Count");
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }

    public void playPickup(){
        pickup.Play();
    }

    public void playPlace(){
        place.Play();
    }
}

[System.Serializable]
public class CircuitLocation
{
    public List<int> disallowedParts;
}


