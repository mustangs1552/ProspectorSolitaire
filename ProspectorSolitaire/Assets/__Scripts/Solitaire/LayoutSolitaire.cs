// (Unity3D) New monobehaviour script that includes regions for common sections, and supports debugging.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SolSlotDef
{
    public float x = 0;
    public float y = 0;
    public bool faceUp = false;
    public string layerName = "Default";
    public int layerID = 0;
    public int id = 0;
    public int hiddenBy = 0;
    public string type = "slot";
    public Vector2 stagger = Vector2.zero;
    public Vector3 pos = Vector3.zero;
}

public class LayoutSolitaire : MonoBehaviour
{
    #region GlobalVareables
    #region DefaultVareables
    public bool isDebug = false;
    private string debugScriptName = "LayoutSolitaire";
    #endregion

    #region Static

    #endregion

    #region Public
    public PT_XMLReader xmlr = null;
    public PT_XMLHashtable xml = null;
    public Vector2 multiplier = Vector2.zero;

    public List<SolSlotDef> slotDefs = new List<SolSlotDef>();
    public SolSlotDef drawPile = null;
    public SolSlotDef discardPile = null;
    public SolSlotDef target = null;
    #endregion

    #region Private

    #endregion
    #endregion

    #region CustomFunction
    #region Static

    #endregion

    #region Public

    #endregion

    #region Private

    #endregion

    #region Debug
    private void PrintDebugMsg(string msg)
    {
        if (isDebug) Debug.Log(debugScriptName + "(" + this.gameObject.name + "): " + msg);
    }
    private void PrintWarningDebugMsg(string msg)
    {
        Debug.LogWarning(debugScriptName + "(" + this.gameObject.name + "): " + msg);
    }
    private void PrintErrorDebugMsg(string msg)
    {
        Debug.LogError(debugScriptName + "(" + this.gameObject.name + "): " + msg);
    }
    #endregion

    #region Getters_Setters

    #endregion
    #endregion

    #region UnityFunctions

    #endregion

    #region Start_Update
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        PrintDebugMsg("Loaded.");
    }
    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start()
    {

    }
    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {

    }
    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {

    }
    // LateUpdate is called every frame after all other update functions, if the Behaviour is enabled.
    void LateUpdate()
    {

    }
    #endregion
}