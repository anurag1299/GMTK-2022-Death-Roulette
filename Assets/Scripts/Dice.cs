using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 power;
    [SerializeField] private GameObject[] faces;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;

    private GameObject _gameObject;
    private Rigidbody rb;
    private bool fisrtCollision;
    private bool isWaitingForResult;
    private bool isReadyForResult;


    private Vector3 startPosition;
    private Quaternion startRotation;
    private int result = -1;
    public int get_Result()
    {
        return result;
    }


    public delegate void OnDieStopped(int result);
    public event OnDieStopped onDieStopped;

    void Start()
    {
        inputManager.onSpacePressed += ThrowDice;
        _gameObject = this.gameObject;
        fisrtCollision = true;
        startPosition = _gameObject.transform.position;
        startRotation = _gameObject.transform.rotation;
        isWaitingForResult = false;
        isReadyForResult = false;
       
    }


    private void Update()
    {
        
        if(rb != null && Mathf.Abs(rb.velocity.x) <= 0.3f
            && Mathf.Abs(rb.velocity.y) <= 0.3f
            && Mathf.Abs(rb.velocity.z) <= 0.3f
            && Mathf.Abs(rb.angularVelocity.x) <= 0.3f
            && Mathf.Abs(rb.angularVelocity.y) <= 0.3f
            && Mathf.Abs(rb.angularVelocity.z) <= 0.3f
            && !fisrtCollision)
        {
            if (!isWaitingForResult)
            {
                StartCoroutine(waitForResult());
            }
            
        }
    }




    private void ThrowDice()
    {
        Debug.Log("Thrown");
        if (rb == null)
        {
            rb = _gameObject.AddComponent<Rigidbody>( );
        }
        else
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
        rb.AddForce(power, ForceMode.Impulse);
        rb.AddTorque(power*500);
        gameManager.audioManager.Play("dieRoll");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (fisrtCollision)
        {
            rb.AddForce(new Vector3(power.x * Random.Range(2,5),0, power.x * Random.Range(2, 5)), ForceMode.Impulse);
            rb.AddTorque(new Vector3(power.x * Random.Range(100, 500), power.y * Random.Range(100, 500),0));
            fisrtCollision = false;
        }
        else
        {
            rb.AddTorque(new Vector3(power.x * Random.Range(100, 500), power.y * Random.Range(100, 500), 0));
        }
    }

    private IEnumerator waitForResult()
    {
        isWaitingForResult = true;
        yield return new WaitForSeconds(1f);
        isReadyForResult = true;
    }


    public void CalculateResult(Collider other,string name)
    {
    if (isReadyForResult && other.gameObject.tag == "floor")
    {
        switch (name)
        {
            case "face 1":
                result = 6;
                Debug.Log("its " + result);
                break;
            case "face 2":
                result = 5;
                Debug.Log("its " + result);
                break;
            case "face 3":
                result = 4;
                Debug.Log("its " + result);
                break;
            case "face 4":
                result = 3;
                Debug.Log("its " + result);
                break;
            case "face 5":
                result = 2;
                Debug.Log("its " + result);
                break;
            case "face 6":
                result = 1;
                Debug.Log("its " + result);
                break;
            case "":
                break;

        }
        isReadyForResult = false;
        ResetDie();
    }
}


    private void ResetDie()
    {
        Debug.Log("resetted");
        int []rotaitons = { 0, 90, 180, 270 };
        rb.isKinematic = true;
        rb.detectCollisions = false;
        _gameObject.transform.position = startPosition;
        _gameObject.transform.rotation = Quaternion.Euler(new Vector3(rotaitons[Random.Range(0,4)], rotaitons[Random.Range(0, 4)], rotaitons[Random.Range(0, 4)]));
        fisrtCollision = true;
        onDieStopped?.Invoke(result);
        isWaitingForResult = false;
    }

}
