using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    public PlaceableGrid bag;
    public PlaceableGrid phone;
    public PlaceableGrid wiretapGrid;

    public int numBrokenParts;
    public List<GameObject> partsList;
    public GameObject bugToSpawn;

    public List<CircuitLocation> locationInfo;

    List<CircuitPiece> circuitPieces;

    // Start is called before the first frame update
    void Start()
    {
        circuitPieces = new List<CircuitPiece>();
    }
    
    [ContextMenu("Create Puzzle")]
    public void CreatePuzzle()
    {
        Shuffle(ref partsList);
        int partListIndex = 0;
        for(int i = 0; i < phone.underlyingGrid.GetNumGridLocations(); ++i)
        {
            if(partListIndex >= partsList.Count)
            {
                partListIndex = partListIndex % partsList.Count;
            }
            
            while(locationInfo.Count > i && locationInfo[i].disallowedParts.Contains(partListIndex+1))
            {
                partListIndex++;
                if(partListIndex >= partsList.Count)
                {
                    partListIndex = partListIndex % partsList.Count;
                }
            }
            
            //Spawn a correct part and a missing part at each location
            GameObject part = Instantiate(partsList[partListIndex], Vector3.back * 0.4f, Quaternion.identity);
            GameObject placement = Instantiate(partsList[partListIndex], Vector3.back * 0.3f, Quaternion.identity);

            CircuitPiece partPiece = part.GetComponent<CircuitPiece>();
            CircuitPiece placementPiece = placement.GetComponent<CircuitPiece>();

            partPiece.type = GrabbableObjectType.Part;
            partPiece.disableGrabbing = true;
            placementPiece.type = GrabbableObjectType.Missing;
            placementPiece.disableGrabbing = true;

            //Place them both on the grid
            phone.PlaceObjectAt<CircuitPiece>(partPiece, i);
            phone.PlaceObjectAt<CircuitPiece>(placementPiece, i);
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
                GameObject correctPart = Instantiate(circuitPieces[j].gameObject, Vector3.back * 0.4f, Quaternion.identity);
                CircuitPiece piece = correctPart.GetComponent<CircuitPiece>();
                piece.type = GrabbableObjectType.Part;
                piece.SetGameObjectSprite();
                //TODO: Replace this with placing it on the table
                bag.PlaceObjectAt<CircuitPiece>(piece, j);
            }
        }

        GameObject bug = Instantiate(bugToSpawn, Vector3.back * 0.4f, Quaternion.identity);
        GameObject bugHolder = Instantiate(bugToSpawn, Vector3.back * 0.4f, Quaternion.identity);
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

        //TODO: Replace this with placing it on the table
        bag.PlaceObjectAt<CircuitPiece>(bugGrabbable, j);
        wiretapGrid.PlaceObjectAt<CircuitPiece>(bugHolderGrabbable, 0);
    }

    public void AttemptToFinishPuzzle()
    {
        if(VerifyPuzzle())
        {
            Debug.Log("Yep");
        }
        else
        {
            Debug.Log("Nope");
        }
    }

    public bool VerifyPuzzle()
    {
        int numBroken = phone.GetNumberOfBrokenObjectsInGrid();
        int numCorrect = phone.GetNumberOfCorrectObjectsInGrid();
        int numBugs = wiretapGrid.GetNumberOfBugsInGrid();

        if(numBroken > 0)
        {
            return false;
        }

        if(numCorrect != circuitPieces.Count)
        {
            return false;
        }

        if(numBugs != 1)
        {
            return false;
        }

        return true;
    }

    public void Shuffle<T>(ref List<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}

[System.Serializable]
public class CircuitLocation
{
    public List<int> disallowedParts;
}


