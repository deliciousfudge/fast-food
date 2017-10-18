using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;        
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public CameraControl m_CameraControl;   
    public Text m_MessageText;              
    public GameObject m_BurgerPrefab;         
    public BurgerManager[] m_Burgers;           


    private int m_RoundNumber;              
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;       
/*    private BurgerManager m_RoundWinner;
    private BurgerManager m_GameWinner;       
*/

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllBurgers();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllBurgers()
    {
        for (int i = 0; i < m_Burgers.Length; i++)
        {
            m_Burgers[i].m_Instance =
                Instantiate(m_BurgerPrefab, m_Burgers[i].m_SpawnPoint.position, m_Burgers[i].m_SpawnPoint.rotation) as GameObject;
            m_Burgers[i].m_PlayerNumber = i + 1;
            m_Burgers[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Burgers.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Burgers[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

/*        if (m_GameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
*/    }


    private IEnumerator RoundStarting()
    {
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        yield return null;
    }


    private IEnumerator RoundEnding()
    {
        yield return m_EndWait;
    }


    private bool OneBurgerLeft()
    {
        int numBurgersLeft = 0;

        for (int i = 0; i < m_Burgers.Length; i++)
        {
            if (m_Burgers[i].m_Instance.activeSelf)
                numBurgersLeft++;
        }

        return numBurgersLeft <= 1;
    }

/*
    private BurgerManager GetRoundWinner()
    {
        for (int i = 0; i < m_Burgers.Length; i++)
        {
            if (m_Burgers[i].m_Instance.activeSelf)
                return m_Burgers[i];
        }

        return null;
    }


    private BurgerManager GetGameWinner()
    {
        for (int i = 0; i < m_Burgers.Length; i++)
        {
            if (m_Burgers[i].m_Wins == m_NumRoundsToWin)
                return m_Burgers[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Burgers.Length; i++)
        {
            message += m_Burgers[i].m_ColoredPlayerText + ": " + m_Burgers[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }
*/

    private void ResetAllBurgers()
    {
        for (int i = 0; i < m_Burgers.Length; i++)
        {
            m_Burgers[i].Reset();
        }
    }


    private void EnableBurgerControl()
    {
        for (int i = 0; i < m_Burgers.Length; i++)
        {
            m_Burgers[i].EnableControl();
        }
    }


    private void DisableBurgerControl()
    {
        for (int i = 0; i < m_Burgers.Length; i++)
        {
            m_Burgers[i].DisableControl();
        }
    }
}