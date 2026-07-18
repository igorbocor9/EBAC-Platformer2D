using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float jumpForce = 2f;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = 0.3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation player")]
    public string boolrun = "Run";
    public string triggerDeath = "Death";
    public float playerSwipeDuration = .1f;
}
