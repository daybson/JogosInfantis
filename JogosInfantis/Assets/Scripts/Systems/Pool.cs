using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public int maxProjectilesPerPool;
    private List<GameObject> projectiles = new List<GameObject>();
    public string ProjetilePrefabName;

    private void Awake()
    {
        Initialize();
    }


    private void Initialize()
    {
        var p = Instantiate(Resources.Load<GameObject>("Prefabs/" + ProjetilePrefabName));
        p.SetActive(false);

        for (var i = 0; i < this.maxProjectilesPerPool; i++)
        {
            var obj = Instantiate(p);            
            this.projectiles.Add(obj);
        }

        Destroy(p);
    }


    public bool RequestProjectile(Transform origin)
    {
        var projectile = this.projectiles.FirstOrDefault(p => !p.activeSelf);

        if (projectile != null)
        {
            projectile.transform.position = origin.position;
            projectile.transform.up = origin.up;
            projectile.SetActive(true);
            return true;
        }

        Debug.LogWarning("All objects in use!");
        return false;
    }

    public GameObject RequestProjectile()
    {
        var projectile = this.projectiles.FirstOrDefault(p => !p.activeSelf);

        if (projectile != null)
        {
            //projectile.transform.position = origin.position;
            //projectile.transform.up = origin.up;
            projectile.SetActive(true);
            return projectile;
        }

        Debug.LogWarning("All objects in use!");
        return null;
    }


    //public void DeactivateProjectile(int key)
    //{
    //    try
    //    {
    //        this.projectiles[key].SetActive(false);
    //    }
    //    catch
    //    {
    //        Debug.LogWarning($"Error when accessing projectiles dictionary! Key received: {key}");
    //    }
    //}


    public void ResetAllProjectiles()
    {
        for (int i = 0; i < this.maxProjectilesPerPool; i++)
            this.projectiles[i].SetActive(false);
    }
}
