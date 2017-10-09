using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
	private float InitialTime;

	public char m_PlayerNumber;              // Used to identify which player belongs to which player.  This is set by this player's manager.
	public float m_Speed = 12f;                 // How fast the player moves forward and back.
	public float m_TurnSpeed = 180f;            // How fast the player turns in degrees per second.
	public float m_radius=5f;
	public LayerMask m_layermask;
	public GameObject m_area;
	public Transform m_ObsTransform;
	private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
	private string m_TurnAxisName;              // The name of the input axis for turning.
	private Rigidbody m_Rigidbody;              // Reference used to move the player.
	private float m_MovementInputValue;         // The current value of the movement input.
	private float m_TurnInputValue;             // The current value of the turn input.
	private bool trapped = false;
	//for testing:
	public bool b1 = false;
	public bool b2 = false;
	public bool b3 = false;
	public bool b4 = false;
	public int numbull;
	public GameObject myObject;
	private GameObject obstacle;
	private GameObject obstacle1,obstacle2,obstacle3;
	public Material myMaterial;
	private Vector3[] vert;

	private void Awake ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();

	}

	private void LoadNextScene() {
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene(scene.buildIndex + 1);
	}

	private void OnEnable ()
	{
		// When the player is turned on, make sure it's not kinematic.
		m_Rigidbody.isKinematic = false;

		// Also reset the input values.
		m_MovementInputValue = 0f;
		m_TurnInputValue = 0f;
	}

	private void OnDisable ()
	{
		// When the player is turned off, set it to kinematic so it stops moving.
		m_Rigidbody.isKinematic = true;
	}

	private void Start ()
	{
		// The axes names are based on player number.
		m_PlayerNumber = gameObject.name[gameObject.name.Length - 1];
		m_MovementAxisName = "Vertical" + m_PlayerNumber;
		m_TurnAxisName = "Horizontal" + m_PlayerNumber;

		Score.Instance.createPlayer (gameObject.name);
		InitialTime = Time.time;
		Debug.Log ("From defender script: " + Score.Instance.getAllPayers().Count);
	}

	private void Update ()
	{
		// Store the value of both input axes.
		m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);
		Freeze();
	}


    private void Freeze()
    {
        Collider[] FrozenBullets = Physics.OverlapSphere(transform.position, 100, 1 << 9);
        int i, j, k, l,n = FrozenBullets.Length;
        numbull = n;
        Vector3[] Side;
        Vector3[] relPlayer;
        relPlayer = new Vector3[4];
        Side = new Vector3[4];
        if (n > 3)
        {
            for (i = 0; i < n - 3; i++)
            {
                for (j=i+1;j<n - 2; j++)
                {
                    for (k = j + 1; k < n - 1; k++)
                    {
                        for (l = k + 1;l < n; l++)
                        {
                            Side[0] = FrozenBullets[i].transform.position - FrozenBullets[j].transform.position;
                            Side[1] = FrozenBullets[j].transform.position - FrozenBullets[k].transform.position;
                            Side[2] = FrozenBullets[k].transform.position - FrozenBullets[l].transform.position;
                            Side[3] = FrozenBullets[l].transform.position - FrozenBullets[i].transform.position;
                            relPlayer[0] = transform.position - FrozenBullets[j].transform.position;
                            relPlayer[1] = transform.position - FrozenBullets[k].transform.position;
                            relPlayer[2] = transform.position - FrozenBullets[l].transform.position;
                            relPlayer[3] = transform.position - FrozenBullets[i].transform.position;
                            if (b1 = CheckInAngle(Side[0],Side[1],relPlayer[0]) && (b2 = CheckInAngle(Side[1], Side[2], relPlayer[1])) && (b3 = CheckInAngle(Side[2], Side[3], relPlayer[2])) && ( b4 = CheckInAngle(Side[3], Side[0], relPlayer[3])))
                            {
                                  trapped = true;
                                //Destroy(gameObject);
								obstacle = Instantiate (m_area, m_ObsTransform.position, m_ObsTransform.rotation) as GameObject;

								obstacle.transform.position = (FrozenBullets [i].transform.position + FrozenBullets [k].transform.position) / 2;
								obstacle.transform.localScale = new Vector3 (2,2,2);
								obstacle1 = Instantiate (m_area, m_ObsTransform.position, m_ObsTransform.rotation) as GameObject;
								obstacle1.transform.position = (FrozenBullets [l].transform.position + FrozenBullets [j].transform.position) / 2;
								obstacle1.transform.localScale = new Vector3 (2,2,2);
								GameObject parent = new GameObject();
								Vector3 p = new Vector3 (0, 0, 0);
								/*parent.transform.position = p;
								parent.transform.rotation = Quaternion.LookRotation (Side [0], Vector3.up);

								obstacle.transform.parent = parent.transform;
								parent.transform.position=(FrozenBullets[i].transform.position + FrozenBullets[j].transform.position)/2;
								parent.transform.localScale = new Vector3 (Side [0].x, 1, Side [0].z);

								obstacle1 = Instantiate (m_area, m_ObsTransform.position, m_ObsTransform.rotation) as GameObject;
								obstacle1.transform.localScale = new Vector3 (1, 1, 1);
								obstacle1.transform.position = new Vector3 (0, 0, 0);
								parent.transform.rotation = Quaternion.LookRotation (Side [1], Vector3.up);

								obstacle1.transform.parent = parent.transform;
								parent.transform.position=(FrozenBullets[j].transform.position + FrozenBullets[k].transform.position)/2;
								parent.transform.localScale = new Vector3 (Side [1].x, 1, Side [1].z);

								obstacle2 = Instantiate (m_area, m_ObsTransform.position, m_ObsTransform.rotation) as GameObject;
								obstacle2.transform.localScale = new Vector3 (1, 1, 1);
								obstacle2.transform.position = new Vector3 (0, 0, 0);
								parent.transform.rotation = Quaternion.LookRotation (Side [2], Vector3.up);

								obstacle2.transform.parent = parent.transform;
								parent.transform.position=(FrozenBullets[k].transform.position + FrozenBullets[l].transform.position)/2;
								parent.transform.localScale = new Vector3 (Side [2].x, 1, Side [2].z);

								obstacle3 = Instantiate (m_area, m_ObsTransform.position, m_ObsTransform.rotation) as GameObject;
								obstacle3.transform.localScale = new Vector3 (1, 1, 1);
								obstacle3.transform.position = new Vector3 (0, 0, 0);
								parent.transform.rotation = Quaternion.LookRotation (Side [3], Vector3.up);

								obstacle3.transform.parent = parent.transform;
								parent.transform.position=(FrozenBullets[i].transform.position + FrozenBullets[l].transform.position)/2;
								parent.transform.localScale = new Vector3 (Side [3].x, 1, Side [3].z);*/
								goto v;

                               // if (m_PlayerNumber == '1') {
									//SceneManager.LoadScene ("sc2");
							//	}
                            }
                        }
                    }
                }
            }
        }
		v:
			int ds;
    }

	private bool CheckInAngle(Vector3 Side1, Vector3 Side2, Vector3 relPlayer)
	{
		if (Vector3.Cross(Side1, relPlayer).y * Vector3.Cross(relPlayer, Side2).y < 0)
			return true;
		else
			return false;
	}

	private void FixedUpdate ()
	{
		// Adjust the rigidbodies position and orientation in FixedUpdate.
		if (!trapped)
		{
			Move();
			Turn();
		}
	}


	private void Move ()
	{
		// Create a vector in the direction the player is facing with a magnitude based on the input, speed and the time between frames.
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}


	private void Turn ()
	{
		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
	}
	private IEnumerator OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("vamsi")) {
			Destroy (other.gameObject);
			Score.Instance.setDefenderScore (gameObject.name, true, false, 0.0f);
			Debug.Log ("A bullet Eaten!");
		} else if (other.gameObject.CompareTag ("geetu")) {
			Destroy (gameObject);
            //obstacle = Instantiate (m_area, m_ObsTransform.position, m_ObsTransform.rotation) as GameObject;

			//GameObject cylinder=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			//cylinder.transform.position = gameObject.transform.position;
			//cylinder.transform.localScale = new Vector3 (1, 4, 2);
			//cylinder.AddComponent<bulletabsorb>();
            Destroy(other.gameObject);
			Score.Instance.setDefenderScore (gameObject.name, false, true, Time.time - InitialTime);
			LoadNextScene ();
		} else if (other.gameObject.CompareTag ("Attacker"))
        {
            Destroy (other.gameObject);
			Score.Instance.setDefenderScore (gameObject.name, false, false, Time.time - InitialTime);
			Score.Instance.setAttackerScore (other.gameObject.name,false,true);

            yield return new WaitForSeconds(3);
			LoadNextScene ();
        }
	}
}
