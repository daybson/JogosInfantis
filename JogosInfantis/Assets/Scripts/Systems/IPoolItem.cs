using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPoolItem
{
    bool IsUpdating { get; }
    void Enable();
    void Disable();
}