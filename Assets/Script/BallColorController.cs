using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BallColorController : MonoBehaviour
{
	private SpriteRenderer _spriteRenderer;

	[SerializeField] private Color startColor;
	[SerializeField] private Color endColor;
	[SerializeField, Range(1, 50)] private int maxHit;

	public int CurrentHitCount { get; set; }

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void OnHit()
	{
		CurrentHitCount++;
		var color = Color.Lerp(startColor, endColor, (float) CurrentHitCount / maxHit);
		_spriteRenderer.color = color;
	}
}