using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    List<List<Vector3>> Grid;
    List<List<Cell>> CellGrid;
    ObjectPoints PointsOnThePlane;
    int MazeWidth = 11;
    int MazeHeight = 11;
    readonly int planeHeight = 11;
    readonly int planeWidth = 11;
    GameObject BorderParent;//Empty parent object to keep borders transform
    RecursiveBacktracker RB;
    GenerateMaze generateMaze;


    private int currCollectables = 0;
    public int CurrCollectables
    {
        get { return currCollectables; }
        set { currCollectables = value; }
    }
    //private Text objectsRemaining;
    //public Text ObjectsRemaining
    //{
    //    get
    //    {
    //        if (objectsRemaining == null)
    //            objectsRemaining.text = string.Empty;
    //        return objectsRemaining;
    //    }
    //    set { objectsRemaining = value; }
    //}
    public Text objectsRemaining;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    // Start is called before the first frame update
    void Start()
    {
        ShowCam1();
        UpdateUI();
        BorderParent = new GameObject("BorderParent");
        RB = new RecursiveBacktracker();
        generateMaze = new GenerateMaze();
        //generateMaze.CreateMaze();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            ShowCam1();
        else if (Input.GetKeyDown(KeyCode.Keypad2))
            ShowCam2();
        else if (Input.GetKeyDown(KeyCode.Keypad3))
            ShowCam3();
        else if (Input.GetKeyDown(KeyCode.Keypad4))
            ShowCam4();
    }

    public void UpdateUI()
    {
        objectsRemaining.text = "Objects left: " + CurrCollectables.ToString();
    }

    public void ShowCam1()
    {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = false;
    }
    
    public void ShowCam2()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        cam3.enabled = false;
        cam4.enabled = false;
    }
    
    public void ShowCam3()
    {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = true;
        cam4.enabled = false;
    }

    public void ShowCam4()
    {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = true;
    }
}
