using System;
using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue < 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Item_Hunger" 태그로 아이템 구분
        if (other.CompareTag("Item_Hunger"))
        {
            Destroy(other.gameObject); // 아이템 삭제
            StartCoroutine(IncreaseHungerOverTime());
        }
    }

    IEnumerator IncreaseHungerOverTime()
    {
        hunger.Add(100); // 헝거 수치 +50
        yield return new WaitForSeconds(1f); //1초 대기
        hunger.Add(100);
        yield return new WaitForSeconds(1f);
        hunger.Add(100);
        yield return new WaitForSeconds(1f);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }
}