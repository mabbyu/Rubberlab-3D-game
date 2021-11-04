using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    [SerializeField]
    private float respawnTime;

    [SerializeField]
    private GameObject healthPanel;

    [SerializeField]
    private RectTransform healthBar;

    private float healthBarStartWidth;

    public GameObject meshRenderer;

    public GameObject deadObject;

    public float scoreGame;

    private bool isDead;

    public spawner s;

    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        currentHealth = maxHealth;
        healthBarStartWidth = healthBar.sizeDelta.x;
        UpdateUI();
    }

    public void ApplyDamage(float damage)
    {
        //if (isDead) return;
     
        scoreGame = damage;

        GameController.instance.currScore += scoreGame;

        var a = Instantiate(deadObject, transform.position, transform.rotation);
        a.GetComponentInChildren<Text>().text = scoreGame.ToString();

        StartCoroutine(RespawnAfterTime());
        Destroy(gameObject);

        s.currEnemy.Remove(gameObject);

        //UpdateUI();
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTime);
        //ResetHealth();
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        meshRenderer.SetActive(true);
        healthPanel.SetActive(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        float percentOutOf = (currentHealth / maxHealth) * 10;
        float newWidth = (percentOutOf / 10) * healthBarStartWidth;

        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
    }
}