using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			textScore.text = _score.ToString();

			//Text alpha
			var alpha = Mathf.Lerp(100, 230, (float) value / 10);

			var color = textScore.color;
			color.a = alpha / 255f;
			textScore.color = color;

			if (textBoingAnimation.enabled)
				textBoingAnimation.Play(0);
			else
				textBoingAnimation.enabled = true;
		}
	}

	private int _score = 1;


	[SerializeField] private TextMeshPro textScore;
	[SerializeField] private Animator textBoingAnimation;

	[SerializeField] private Canvas canvasStart;
	[SerializeField] private TextMeshProUGUI textStart;
	[SerializeField] private TextMeshProUGUI textTry;
	[SerializeField] private BallController ball;

	private void Awake()
	{
		Instance = this;
	}

	public void OnClick_StartButton()
	{
		OnStartGame();
	}

	private void OnStartGame()
	{
		IEnumerator Do()
		{
			yield return new WaitForSeconds(.2f);
			Score = 1;
			canvasStart.enabled = false;
			ball.OnStart();
		}

		StartCoroutine(Do());
	}

	public void OnGameOver()
	{
		canvasStart.enabled = true;
		textStart.enabled = false;
		textTry.enabled = true;
	}
}