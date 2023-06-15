using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField] private int        _damage         = 1;
    [SerializeField] private float      _size           = 1, 
                                        _distance       = 1, 
                                        _maxDistance    = .01f;
    [SerializeField] private LayerMask _damageCastLayer;

    private void ToDamage()
    {
        RaycastHit hit;

        bool isHit = Physics.SphereCast(transform.forward + new Vector3(0, 0, _distance), _size,transform.forward, out hit, _maxDistance, _damageCastLayer);

        if (isHit)
        {
            if(hit.transform.TryGetComponent<IDamaged>(out IDamaged id))
            {
                MeshRenderer ms = hit.transform.GetComponent<MeshRenderer>();
                Material mat = ms.material;
                switch (mat.name)
                {
                    case "Red":
                        break;
                    case "Purple":
                        break;
                    case "Green":
                        break;
                    default:
                        Debug.LogError("Default");
                }
            }
        }
    }
}
