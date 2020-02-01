using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    public PlaceableGrid bag;
    public PlaceableGrid phone;
    public PlaceableGrid trash;

    public bool randomPhoneLayout; 

    public int trashAmount;
    public List<GameObject> trashObjectsToSpawn;
    public int partsAmount;
    public List<GameObject> partsToSpawn;
    public GameObject bugToSpawn;


    List<Vector2> correctPartLocations;
    Vector2 correctBugLocation;


    // Start is called before the first frame update
    void Start()
    {
        correctPartLocations = new List<Vector2>();
    }
    
    [ContextMenu("Create Puzzle")]
    public void CreatePuzzle()
    {
        
        for(int i =0; i < trashAmount; ++i)
        {
            int trashToSpawn = Random.Range(0, trashObjectsToSpawn.Count);
            GameObject trash = Instantiate(trashObjectsToSpawn[trashToSpawn], Vector3.zero, Quaternion.identity);
            GrabbableObject trashGrabbable = trash.GetComponent<GrabbableObject>();

            if(trashGrabbable == null)
            {
                trashGrabbable = trash.AddComponent<GrabbableObject>();
            }

            trashGrabbable.type = GrabbableObjectType.Trash;
            phone.PlaceObject<GrabbableObject>(trashGrabbable, true);
        }

        for(int  i = 0; i < partsAmount; ++i)
        {
            int partToSpawn = Random.Range(0, partsToSpawn.Count);
            GameObject part = Instantiate(partsToSpawn[partToSpawn], Vector3.zero, Quaternion.identity);
            GrabbableObject partGrabbable = part.GetComponent<GrabbableObject>();

            if(partGrabbable == null)
            {
                partGrabbable = part.AddComponent<GrabbableObject>();
            }

            partGrabbable.type = GrabbableObjectType.Part;
            bag.PlaceObject<GrabbableObject>(partGrabbable, true);
            correctPartLocations.Add(Vector2.one);
        }

        GameObject bug = Instantiate(bugToSpawn, Vector3.zero, Quaternion.identity);
        GrabbableObject bugGrabbable = bug.GetComponent<GrabbableObject>();

        if(bugGrabbable == null)
        {
            bugGrabbable = bug.AddComponent<GrabbableObject>();
        }

        bugGrabbable.type = GrabbableObjectType.Bug;
        bag.PlaceObject<GrabbableObject>(bugGrabbable, true);

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
        int numBugs = phone.GetNumberOfBugsInGrid();

        if(numBroken > 0)
        {
            return false;
        }

        if(numCorrect != correctPartLocations.Count)
        {
            return false;
        }

        if(numBugs != 1)
        {
            return false;
        }

        return true;
    }
}
