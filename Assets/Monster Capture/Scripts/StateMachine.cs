using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //-------------Varibles----------------------\\

    public GameObject player; 





    //------------------States------------------\\

    public State state;

    public enum State
    {
        Patrol,
        Chasing,
        Attack
    }

    private void Start()
    {
        NextState();
    }

    void NextState()
    {
        switch (state)
        {
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Chasing:
                StartCoroutine(ChasingState());
                break;
            case State.Attack:
                break;
            default:
                break;
        }
    }


    //------------------------Enumerators--------------------------\\

    #region Enums
    #endregion

    IEnumerator PatrolState()
    {
        Debug.Log("Enter Patrol State");
        while(state == State.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f, 50f * Time.deltaTime, 0f);

            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();

            float dotResult = Vector3.Dot(directionToPlayer, transform.forward);

            if(dotResult >= 0.95)
            {
                state = State.Chasing;
            }


            yield return null; // Waits for a frame

        }
        Debug.Log("Exit Patrol State");
        NextState();
    }

    IEnumerator ChasingState()
    {
        Debug.Log("Enter Chasing State");
        while (state == State.Chasing)
        {
            float wave = Mathf.Sin(Time.time * 20f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 20f) * 0.1f + 1f;
            transform.localScale = new Vector3(wave, wave, wave);

            yield return null; // Waits for a frame

        }
        Debug.Log("Exit Chasing State");
        NextState();
    }
}
