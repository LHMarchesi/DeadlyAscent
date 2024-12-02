using UnityEngine;

public class Punches : BaseWeapon
{
    [SerializeField] private GameObject RightPunch;
    [SerializeField] private GameObject leftPunch;

    public float Damage { get => weaponData.BaseDamage; private set { } }

    public bool IsPunching { get => isPunching; set => isPunching = value; }

    private float nextTimeToPunch;
    private bool isPunching;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time <= weaponData.cooldownWindow)
        {
            Punch(leftPunch);
        }

        if (Input.GetMouseButtonDown(0) && Time.time <= weaponData.cooldownWindow)
        {
            Punch(RightPunch);
        }
    }

    private void Punch(GameObject punch)
    {
        IsPunching = true;

        Rigidbody rb = punch.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        Animator punchAnimator = punch.GetComponent<Animator>();
        punchAnimator.SetTrigger("Punch");

        nextTimeToPunch = Time.time + weaponData.cooldownWindow;
    }


    public override void Shoot()
    {

    }
}
