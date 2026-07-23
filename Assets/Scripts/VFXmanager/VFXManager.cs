using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        VFX_2
    }

    public List<VFXManagerSetup> vfxSetup;

    public void PlayVFXByType(VFXType VFXType, Vector3 position)
    {
        foreach (var i in vfxSetup)
        {
            if (i.vfxType == VFXType)
            {
                var item = Instantiate(i.Prefab);
                item.transform.position = position;
                Destroy(item.gameObject, 5f);
                break;
            }
        }
    }
}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject Prefab;
}