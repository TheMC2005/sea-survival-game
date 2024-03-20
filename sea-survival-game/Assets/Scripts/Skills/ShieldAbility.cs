using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    PlayerHP playerhp;
    public bool canShield;
    [SerializeField] float coolDown;
    [SerializeField] int shieldPower = -15;
    [SerializeField] ParticleSystem particlesystem;
    void Start()
    {
        particlesystem.Stop();
        playerhp = GetComponent<PlayerHP>();
        canShield = true;
    }

    private void Update()
    {
        particlesystem.transform.position = GameManagerSingleton.Instance.player.transform.position;
        if (Input.GetKeyDown(KeyCode.E) && canShield)
        {
            Shield();
            particlesystem.Play();
            canShield = false;
            StartCoroutine(canShieldRoutine());
        }
    }

    private IEnumerator canShieldRoutine()
    {
        yield return new WaitForSeconds(coolDown-8);
        particlesystem.Stop();
        yield return new WaitForSeconds(coolDown - 3);
        canShield = true;
    }

    public void Shield()
    {
        playerhp.DealDamage(shieldPower);
        Stats.Instance.healthslider.maxValue -= shieldPower;
    }
}
