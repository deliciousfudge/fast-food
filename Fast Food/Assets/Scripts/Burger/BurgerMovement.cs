using UnityEngine;

public class BurgerMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_StartingSpeed = 24f;
    public float m_TurnSpeed = 90f;
    //public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;
    public float m_BoostDuration = 40.0f;
    public string[] m_PickupItems = { "Fry", "Avo", "Ket", "Mush", "Soda" };
    public GameObject FryLauncher;
    public GameObject Fry;
    public GameObject AvoLauncher;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;
    private float m_CurrentSpeed;
    private string m_PickupEquipped = null;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        SetCurrentSpeed(m_StartingSpeed); 
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
    }

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        switch(m_PickupEquipped)
        {
            case "FryLauncher":
            {
                break;
            }
            default:break;
        }
    }

    private void SetCurrentSpeed(float _iSpeed)
    {
        m_CurrentSpeed = _iSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Add features later
        if (collision.gameObject.tag == "SpeedPad")
        {
            m_BoostDuration = 50.0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
     

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") && m_PickupEquipped == null)
        {
            other.gameObject.SetActive(false);
            int iRand = (int)Random.Range(0, 2);
            m_PickupEquipped = m_PickupItems[iRand];
            Debug.Log("You equipped " + m_PickupEquipped);

            switch (m_PickupEquipped)
            {
                case "Fry":
                {
                    GameObject fryLauncher = Instantiate(FryLauncher) as GameObject;
                    GameObject fryOne = Instantiate(Fry) as GameObject;
                    GameObject fryTwo = Instantiate(Fry) as GameObject;
                    GameObject fryThree = Instantiate(Fry) as GameObject;

                    fryLauncher.transform.SetParent(gameObject.transform);
                    fryLauncher.transform.localPosition = new Vector3(-1.5f, 1.5f, 0f);
                    fryLauncher.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);
                    
                    fryOne.transform.SetParent(gameObject.transform);
                    fryOne.transform.localPosition = new Vector3(-1.55f, 1.3f, 0.1f);
                    fryOne.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                    fryTwo.transform.SetParent(gameObject.transform);
                    fryTwo.transform.localPosition = new Vector3(-1.55f, 1.5f, 0f);
                    fryTwo.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                    fryThree.transform.SetParent(gameObject.transform);
                    fryThree.transform.localPosition = new Vector3(-1.55f, 1.7f, 0.2f);
                    fryThree.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                    break;
                }
                case "Avo":
                    {
                        GameObject avoLauncher = Instantiate(AvoLauncher) as GameObject;

                        avoLauncher.transform.SetParent(gameObject.transform);
                        avoLauncher.transform.localPosition = new Vector3(-1.5f, 1.5f, 0f);
                        avoLauncher.transform.localRotation = Quaternion.Euler(180f, 0f, 90f);

                        break;
                    }

                default:break;
            }
        }
    }

    private void FixedUpdate()
    {
        // Move and turn the cart.
        Move();
        Turn();
    }


    private void Move()
    {
        if (m_BoostDuration > 0)
        {
            m_CurrentSpeed = m_StartingSpeed * Mathf.Log(m_BoostDuration);
            m_BoostDuration--;
        }
        else
        {
            m_CurrentSpeed = m_StartingSpeed;
        }

        // Adjust the position of the burger based on the player's input.
        Vector3 movement = transform.forward * m_MovementInputValue * m_CurrentSpeed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

        GameObject[] wheels = GameObject.FindGameObjectsWithTag("Wheel");
        
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        }
    }


    private void Turn()
    {
        // Adjust the rotation of the burger based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}