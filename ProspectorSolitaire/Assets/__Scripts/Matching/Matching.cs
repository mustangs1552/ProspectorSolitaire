// (Unity3D) New monobehaviour script that includes regions for common sections, and supports debugging.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState
{
    GroupOne,
    GroupTwo
}

public enum TurnPhaseMatching
{
    PlayerOne,
    PlayerTwo
}

public class Matching : MonoBehaviour
{
    #region GlobalVareables
    #region DefaultVareables
    public bool isDebug = false;
    private string debugScriptName = "Prospector";
    #endregion

    #region Static
    public static Matching S;
    #endregion

    #region Public
    public Deck deck;
    public TextAsset deckXML;

    public MatchingLayout layout;
    public TextAsset layoutXML;
    public Vector3 layoutCenter;
    public float xOffset = 3;
    public float yOffset = -2.5f;
    public Transform layoutAnchor;

    public List<CardMatching> cmDeck;
    public List<CardMatching> cardGroupOne;
    public List<CardMatching> cardGroupTwo;
    public List<CardMatching> playerOneMatches;
    public List<CardMatching> playerTwoMatches;

    public GameState state = GameState.GroupOne;
    public TurnPhaseMatching turnState = TurnPhaseMatching.PlayerTwo;
    #endregion

    #region Private

    #endregion
    #endregion

    #region CustomFunction
    #region Static

    #endregion

    #region Public
    public void CardClicked(CardMatching cd)
    {
        switch(cd.state)
        {
            case CardStateMatching.GroupOne:
                break;
            case CardStateMatching.GroupTwo:
                break;
            case CardStateMatching.Matched:
                break;
        }
    }

    public bool CardMatch(CardMatching c0, CardMatching c1)
    {
        if (!c0.FaceUp || !c1.FaceUp) return false;
        
        if (c0.rank == c1.rank) return true;
        if (c0.suit == c1.suit) return true;

        return false;
    }
    #endregion

    #region Private
    private List<CardMatching> ConvertListCardsToListCardMatching(List<Card> lCD)
    {
        List<CardMatching> lCP = new List<CardMatching>();
        CardMatching tCP;
        foreach(Card tCD in lCD)
        {
            tCP = tCD as CardMatching;
            lCP.Add(tCP);
        }
        return lCP;
    }

    private void LayoutGame()
    {
        if(layoutAnchor == null)
        {
            GameObject tGO = new GameObject("layoutAnchor");
            layoutAnchor = tGO.transform;
            layoutAnchor.transform.position = layoutCenter;
        }

        CardMatching cm;
        /*
        foreach(SlotDef tSD in layout.slotDefs)
        {
            cp = Draw();
            cp.FaceUp = tSD.faceUp;
            cp.transform.parent = layoutAnchor;
            cp.transform.localPosition = new Vector3(layout.multiplier.x * tSD.x, layout.multiplier.y * tSD.y, -tSD.layerID);
            cp.layoutID = tSD.id;
            cp.slotDef = tSD;
            cp.state = CardState.tableau;

            cp.SetSortingLayerName(tSD.layerName);

            tableau.Add(cp);
        }

        foreach(CardMatching tCP in tableau)
        {
            foreach(int hid in tCP.slotDef.hiddenBy)
            {
                cp = FindCardByLayoutID(hid);
                tCP.hiddenBy.Add(cp);
            }
        }*/

        int i = 0;
        for(i = 0; i < layout.cardGroupOne.Count; i++)
        {
            cardGroupOne.Add(cmDeck[i]);
            cmDeck.Remove(cmDeck[i]);
            foreach(CardMatching tCM in cmDeck)
            {
                if(CardMatch(tCM, cardGroupOne[cardGroupOne.Count - 1]))
                {
                    cardGroupOne.Add(tCM);
                    cmDeck.Remove(tCM);
                    break;
                }
            }
        }
        for (int ii = i; ii < layout.cardGroupTwo.Count; ii++)
        {
            cardGroupTwo.Add(cmDeck[ii]);
            cmDeck.Remove(cmDeck[ii]);
            foreach (CardMatching tCM in cmDeck)
            {
                if (CardMatch(tCM, cardGroupTwo[cardGroupTwo.Count - 1]))
                {
                    cardGroupTwo.Add(tCM);
                    cmDeck.Remove(tCM);
                    break;
                }
            }
        }
    }

    private void MoveToMatches(CardMatching cd)
    {
        cd.transform.parent = layoutAnchor;

        if (cd.state != CardStateMatching.Matched)
        {
            if (turnState == TurnPhaseMatching.PlayerOne)
            {
                playerOneMatches.Add(cd);
                cd.transform.localPosition = new Vector3(layout.playerOneMatches.x, layout.playerOneMatches.y, -layout.playerOneMatches.layerID + .5f);
                cd.SetSortingLayerName(layout.playerOneMatches.layerName);
                cd.SetSortOrder(-100 + playerOneMatches.Count);
            }
            else
            {
                playerTwoMatches.Add(cd);
                cd.transform.localPosition = new Vector3(layout.playerTwoMatches.x, layout.playerTwoMatches.y, -layout.playerTwoMatches.layerID + .5f);
                cd.SetSortingLayerName(layout.playerTwoMatches.layerName);
                cd.SetSortOrder(-100 + playerTwoMatches.Count);
            }

            if (cd.state == CardStateMatching.GroupOne) cardGroupOne.Remove(cd);
            else cardGroupTwo.Remove(cd);

            cd.state = CardStateMatching.Matched;
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
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

        S = this;
    }
    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start()
    {
        deck = GetComponent<Deck>();
        deck.InitDeck(deckXML.text);
        Deck.Shuffle(ref deck.cards);

        layout = GetComponent<MatchingLayout>();
        layout.ReadLayout(layoutXML.text);

        cmDeck = ConvertListCardsToListCardMatching(deck.cards);
        LayoutGame();
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