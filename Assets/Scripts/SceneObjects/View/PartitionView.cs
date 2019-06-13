using UnityEngine;
using System.Collections.Generic;

public class PartitionView : MonoBehaviour
{
    public void RefreshView(Ligne[] ligneList, List<bool>[] partition, int bornInf, int bornSup, int bornLim)
    {
        for (int i = 0; i < 8; ++i)
        {
            Ligne ligne = ligneList[i];
            ligne.Update(partition[i], bornInf, bornSup, bornLim);
        }
    }
}
