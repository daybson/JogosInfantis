using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public int maxPoolItems;
    public List<IPoolItem> items = new List<IPoolItem>();
    public string ItemPrefabName;

    private void Awake()
    {
        Initialize();
    }


    private void Initialize()
    {
        var p = Instantiate(Resources.Load<GameObject>("Prefabs/" + ItemPrefabName));

        for (var i = 0; i < this.maxPoolItems; i++)
        {
            var obj = Instantiate(p);
            var ip = obj.GetComponent<IPoolItem>();
            ip.Disable();
            this.items.Add(ip);
        }
        
        Destroy(p);
    }

    public IPoolItem RequestItem()
    {
        var i = this.items.FirstOrDefault(p => !p.IsUpdating);

        if (i != null)
        {
            print("Item enabled");
            i.Enable();
            return i;
        }

        Debug.LogWarning("All objects in use!");
        return null;
    }

    public void DisablePoolItems()
    {
        this.items.ForEach(p => p.Disable());
    }
}
