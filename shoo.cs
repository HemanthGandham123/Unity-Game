using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class shoo : MonoBehaviour
{
	public char m_PlayerNumber;              // Used to identify the different players.
	public Rigidbody m_bullet;                   // Prefab of the bullet.
	public Transform m_FireTransform;           // A child of the player where the bulletss are spawned.

	public float m_MinLaunchForce = 15f;        // The force given to the bullet if the fire button is not held.
	public float m_MaxLaunchForce = 30f;        // The force given to the bullet if the fire button is held for the max charge time.
	public float m_MaxChargeTime = 0.75f;       // How long the bullet can charge for before it is fired at max force.


	private string m_FireButton;  
	private Rigidbody rb;
	public bool m_Stopped;
	private string m_StopButton;// The input axis that is used for launching bullets.

	private bool m_Fired;                       // Whether or not the bullet has been launched with this button press.
    Queue bullet = new Queue();



	private void Start ()
	{
        // The fire axis is based on the player number.
        m_PlayerNumber = gameObject.name[gameObject.name.Length - 1];
        m_FireButton = "Fire" + m_PlayerNumber;
		m_StopButton = "BulletStop" + m_PlayerNumber;
		Score.Instance.createPlayer (gameObject.name);
		Debug.Log ("From attacker script: " + Score.Instance.getAllPayers().Count);
	}


	private void Update ()
	{
		
		if (Input.GetButtonDown (m_FireButton)) {
			m_Fired = false;
		}
		// Otherwise, if the fire button is being held and the bullet hasn't been launched yet...
		else if (Input.GetButton (m_FireButton) && !m_Fired) {
		}
		// Otherwise, if the fire button is released and the bullet hasn't been launched yet...
		else if (Input.GetButtonUp (m_FireButton) && !m_Fired) {
			// ... launch the bullet.
			Fire ();
		}
	} void FixedUpdate(){
		if (Input.GetButtonDown (m_StopButton)) {
			m_Stopped = false;
		} else if (Input.GetButtonUp (m_StopButton) && !m_Stopped) {
            StopBullet();
		}
	}

    private void StopBullet()
    {
        rb = bullet.Dequeue() as Rigidbody;
        if (rb.gameObject.activeSelf)
        {
            Vector3 mov = new Vector3 (0.0f, 0.0f, 0.0f);
            rb.velocity = mov;
            m_Stopped = true;
            rb.tag = "vamsi";
            rb.gameObject.layer = 9;
        }
        else
        {
            Destroy(rb.gameObject);
            StopBullet();
        }
    }

	private void Fire ()
	{

		// Create an instance of the bullet and store a reference to it's rigidbody.
		rb = Instantiate (m_bullet, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        bullet.Enqueue(rb);

		// Set the bullet's velocity to the launch force in the fire position's forward direction.
		rb.velocity =30 * m_FireTransform.forward; ;

		Score.Instance.setAttackerScore (gameObject.name, true, false);
		Debug.Log ("No of bullets fired: " + Score.Instance.createPlayer (gameObject.name).attackerStats.NoOfBulletsFired);
		Debug.Log ("Total players: " + Score.Instance.getAllPayers().Count);
	}
}