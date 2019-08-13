using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonioScript : MonoBehaviour
{
    public float speed = 10;
    public float TimeBetweenActions = 2f;

    private float _timer = 0f;

    public TonioActions CurrentAction;

    Coroutine ActionSelector;

    public enum TonioActions
    {
        Idle,
        Moving,
        Eating,
        Drinking
    }

    bool grounded = false;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Starting");
        if (ActionSelector == null)
        {
            ActionSelector = StartCoroutine(SelectRandomAction());
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Ending");
        if (ActionSelector != null)
        {
            StopCoroutine(ActionSelector);
            ActionSelector = null;
        }
    }

    void Update()
    {
        DoAction();
    }

    private IEnumerator SelectRandomAction()
    {
        while (true)
        {
            Array values = Enum.GetValues(typeof(TonioActions));
            CurrentAction = (TonioActions)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            Debug.Log(CurrentAction.ToString());
            yield return new WaitForSeconds(2);
        }
    }

    private void DoAction()
    {
        switch (CurrentAction)
        {
            case TonioActions.Idle:
                break;
            case TonioActions.Moving:
                transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(0, 2), transform.position.y, transform.position.z + UnityEngine.Random.Range(0, 2));
                break;
            case TonioActions.Eating:
                break;
            case TonioActions.Drinking:
                break;
            default:
                break;
        }
    }
}
