using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
	public float speed = 40.0f;
	public Transform target;

	private Rigidbody rbEnemy;
	private GameObject Orga;
	private GameObject Mecha;
	private bool captured = false;

	void Start()
	{
		rbEnemy = this.GetComponent<Rigidbody>();
		Mecha = GameObject.FindGameObjectWithTag("Mecha");
		Orga = GameObject.FindGameObjectWithTag("Orga");
		target = this.transform;
	}

	void Update()
	{
		RaycastHit hit0;
		RaycastHit hit1;
		Ray hitRay0 = new Ray(transform.position, Orga.transform.position - transform.position);
		Ray hitRay1 = new Ray(transform.position, Mecha.transform.position - transform.position);

		if(!captured)
		{
			if(Physics.Raycast(hitRay1, out hit1))
			{
				if(hit1.collider.tag != "Wall")
				{
						if(Orga.GetComponent<Creator>().ammo < Mecha.GetComponent<Creator>().ammo)
						{
							target = Orga.transform;
						}
						else if(Mecha.GetComponent<Creator>().ammo < Orga.GetComponent<Creator>().ammo)
						{
							target = Mecha.transform;
						}
						else
						{
							if(Mathf.Floor(Vector3.Distance(transform.position, Mecha.transform.position)/100) < Mathf.Floor(Vector3.Distance(transform.position, Orga.transform.position)/100))
							{
								target = Mecha.transform;
							}

							if(Mathf.Floor(Vector3.Distance(transform.position, Orga.transform.position)/100) < Mathf.Floor(Vector3.Distance(transform.position, Mecha.transform.position)/100))
							{
								target = Orga.transform;
							}

							if(Mathf.Floor(Vector3.Distance(transform.position, Mecha.transform.position)/100) == Mathf.Floor(Vector3.Distance(transform.position, Orga.transform.position)/100))
							{
								target = transform;
							}
						}

						FollowTargetWithRotation(target, 1.0f, speed);
				}
			}

			if(Physics.Raycast(hitRay0, out hit0))
			{
				if(hit0.collider.tag != "Wall")
				{
						if(Orga.GetComponent<Creator>().ammo < Mecha.GetComponent<Creator>().ammo)
						{
							target = Orga.transform;
						}
						else if(Mecha.GetComponent<Creator>().ammo < Orga.GetComponent<Creator>().ammo)
						{
							target = Mecha.transform;
						}
						else
						{
							if(Mathf.Floor(Vector3.Distance(transform.position, Mecha.transform.position)/100) < Mathf.Floor(Vector3.Distance(transform.position, Orga.transform.position)/100))
							{
								target = Mecha.transform;
							}

							if(Mathf.Floor(Vector3.Distance(transform.position, Orga.transform.position)/100) < Mathf.Floor(Vector3.Distance(transform.position, Mecha.transform.position)/100))
							{
								target = Orga.transform;
							}

							if(Mathf.Floor(Vector3.Distance(transform.position, Mecha.transform.position)/100) == Mathf.Floor(Vector3.Distance(transform.position, Orga.transform.position)/100))
							{
								target = transform;
							}
						}

						FollowTargetWithRotation(target, 1.0f, speed);
				}
			}
		}
		FollowTargetWithRotation(target, 1.0f, speed);

	}

	void FollowTargetWithRotation(Transform target, float distanceStop, float speed)
	{
		if(Vector3.Distance(transform.position, target.position) > distanceStop)
		{
			transform.LookAt(target);
			rbEnemy.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Orga" || other.collider.tag == "Mecha")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if(other.collider.tag == "Bullet")
		{
			Destroy(this.gameObject);
		}

		if(other.collider.tag == "Ball")
		{
			captured = true;
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Ball")
		{
			captured = true;
			Destroy(this.gameObject);
		}
	}
}
