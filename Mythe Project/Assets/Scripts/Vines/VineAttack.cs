using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAttack : MonoBehaviour
{
    PlayerAttack player;
    Animator vineAnimator;

    bool isAttacking;

    void Start()
    {
        player = FindObjectOfType<PlayerAttack>();

        if (transform.GetChild(0).GetComponent<Animator>() != null) {
            vineAnimator = transform.GetChild(0).GetComponent<Animator>();
        }

        if (player != null)
        {
            player = player.GetComponent<PlayerAttack>();
            player.VineAttack += Attack;
        }

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        isAttacking = false;
    }

    void Attack(RaycastHit target)
    {
        if (!isAttacking)
        {
            transform.position = target.point;
            transform.up = target.normal;

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);

            if (vineAnimator != null) {
                vineAnimator.SetTrigger(Constants.AnimatorTriggerString.VINE_ATTACK);
            }

            if (player.Mana != null) {
                player.Mana.SubtractMana(Constants.SecondaryWeapon.VINES);
            }

            StartCoroutine("DisablingCoroutine");
            isAttacking = true;
        }
    }

    IEnumerator DisablingCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        isAttacking = false;
    }
}
