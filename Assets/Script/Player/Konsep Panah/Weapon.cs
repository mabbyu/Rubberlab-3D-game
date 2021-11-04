using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private Karet karetPrefab;

    [SerializeField]
    private Transform spawnPoint;

    private Karet currentKaret;

    private string enemyTag;

    public bool isReloading;

    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void Reload()
    {
        if (isReloading || currentKaret != null) return;
        isReloading = true;
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);
        currentKaret = Instantiate(karetPrefab, spawnPoint);
        currentKaret.transform.localPosition = Vector3.zero;
        currentKaret.SetEnemyTag(enemyTag);
        currentKaret.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        if (isReloading || currentKaret == null) return;
        var force = spawnPoint.TransformDirection(Vector3.forward * firePower);
        currentKaret.Fly(force);
        currentKaret.isFired = true;
        currentKaret.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        currentKaret = null;
        

        Reload();
    }

    public bool IsReady()
    {
        return (!isReloading && currentKaret != null);
    }
}