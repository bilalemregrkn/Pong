using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleColorController : MonoBehaviour
{
	private ParticleSystem _particleSystem;


	[SerializeField] private SpriteRenderer ballSprite;
	[SerializeField] private Color colorUp;
	[SerializeField] private Color colorDown;

	[SerializeField] private float maxHeight;

	[SerializeField] private bool isParticle;
	[SerializeField] private bool isBall;

	private void Start()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		var normalized = (maxHeight - transform.position.y) / (maxHeight * 2);
		var color = Color.Lerp(colorDown, colorUp, normalized);

		if (isParticle)
		{
			var main = _particleSystem.main;
			main.startColor = color;
		}

		if (isBall)
			ballSprite.color = color;
	}
}