using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockZones : MonoBehaviour
{
    public List<DrawArea> _zones;
    private void Awake()
    {
        _zones = GetComponentsInChildren<DrawArea>().ToList();
    }
}
