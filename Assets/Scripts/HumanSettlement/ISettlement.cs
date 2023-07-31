using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISettlement
{
    public void SettlersSpawned(List<Human> settlers);
    public void SettlementDestroyed();
}