using UnityEngine;

public class RacketController : MonoBehaviour
{
	public bool isUp;
	public bool isPlayer;

	[SerializeField] private float speed;
	[SerializeField] private float limitPositionX = 2.11f;

	[SerializeField, Range(0, 1f)] private float aiSpeed;

	private void Update()
	{
		Vector3 newPosition = transform.position;
		if (isPlayer)
		{
			var input = Input.GetAxis("Horizontal");
			newPosition = transform.position + input * speed * Time.deltaTime * Vector3.right;
		}
		else
		{
			newPosition.x = Mathf.Lerp(newPosition.x, BallController.Instance.transform.position.x, aiSpeed * GameManager.Instance.Score);
		}

		newPosition.x = Mathf.Clamp(newPosition.x, -limitPositionX, limitPositionX);

		transform.position = newPosition;
	}
}