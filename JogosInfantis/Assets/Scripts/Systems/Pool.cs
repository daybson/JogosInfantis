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

    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        var p = Instantiate(Resources.Load<GameObject>("Prefabs/" + ItemPrefabName));
        p.transform.SetAsLastSibling();
        var ip0 = p.GetComponent<IPoolItem>();
        ip0.Disable();
        this.items.Add(ip0);

        for (var i = 1; i < this.maxPoolItems; i++)
        {
            var obj = Instantiate(p);
            obj.transform.SetAsLastSibling();
            var ip = obj.GetComponent<IPoolItem>();
            ip.Disable();
            this.items.Add(ip);
        }
    }

    public IPoolItem RequestItem()
    {
        var i = this.items.FirstOrDefault(p => !p.IsUpdating);

        if (i != null)
        {
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
