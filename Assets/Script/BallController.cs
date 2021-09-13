using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum HitState
{
	Perfect,
	Good,
	NotBad,
	Okay
}

public class BallController : MonoBehaviour
{
	public static BallController Instance { get; private set; }

	[SerializeField] private Rigidbody2D myRigidbody;

	[SerializeField] private float speed = 10;
	[SerializeField] private TrailRenderer myTrail;
	[SerializeField] private TextMeshPro textComment;
	[SerializeField] private BallColorController colorController;

	private void Awake()
	{
		Instance = this;
	}

	public void OnStart()
	{
		myRigidbody.velocity = Vector2.zero;
		transform.position = Vector2.zero;
		myTrail.Clear();
		myRigidbody.AddForce(Vector2.down * speed);
		colorController.CurrentHitCount = 0;
	}

	private void SetTextComment(float value)
	{
		value = Mathf.Abs(value);
		var result = "";
		if (value < .1f)
			result = "PERFECT!";
		else if (value < .25f)
			result = "GOOD";
		else if (value < .4f)
			result = "NOT BAD";
		else
			result = "OKAY";

		textComment.text = result;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.CompareTag("Racket"))
		{
			var racket = other.transform.GetComponent<RacketController>();
			var directionY = racket.isUp ? -1 : 1;

			var power = GetPowerX(other.transform.position, other.collider.bounds.size.x);
			SetTextComment(power);
			var force = new Vector2(power, directionY);
			myRigidbody.AddForce(force * speed);

			colorController.OnHit();
		}

		if (other.transform.CompareTag("Goal"))
		{
			GameManager.Instance.Score++;
			OnStart();
		}

		if (other.transform.CompareTag("GameOverGoal"))
		{
			myRigidbody.velocity = Vector2.zero;
			GameManager.Instance.OnGameOver();
		}
	}

	private float GetPowerX(Vector3 racketPosition, float racketHeight)
	{
		var value = (transform.position.x - racketPosition.x) / racketHeight;
		return value;
	}
}