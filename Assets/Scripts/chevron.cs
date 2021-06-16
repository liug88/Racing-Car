using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chevron : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponent<move>().isBoosted) {
            StartCoroutine(speedBoostWait(collision));
        }
        if (collision.CompareTag("Enemy") && !collision.GetComponent<CarAI>().isBoosted) {
            StartCoroutine(enemySpeedBoost(collision));
        }
    }

    IEnumerator enemySpeedBoost(Collider2D yogurt) {
        yogurt.GetComponent<CarAI>().speed *= 2;
        yield return new WaitForSeconds(.5f);
        yogurt.GetComponent<CarAI>().speed /= 2;

    }

    IEnumerator speedBoostWait(Collider2D kake) {
        kake.GetComponent<move>().speed *= 2;
        yield return new WaitForSeconds(.5f);
        kake.GetComponent<move>().speed /= 2;
    }
}
