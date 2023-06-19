using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageCaster : MonoBehaviour
{
    [SerializeField] private int        _damage         = 1;
    [SerializeField] private float      _size           = 1, 
                                        _maxDistance    = .01f;
    [SerializeField] private LayerMask _damageCastLayer;

    private PlayerFXController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerFXController>();
    }

    private void Update()
    {
        if(GameManager.Instance.GetDash()) ToDamage();
    }
    MeshRenderer ms;
    Material mat;
    private void ToDamage()
    {
        RaycastHit hit;
                

        bool isHit = Physics.SphereCast(transform.forward + transform.position, _size,transform.forward, out hit, _maxDistance, _damageCastLayer);
        if (isHit)
        {
            if(hit.transform.TryGetComponent<IDamageable>(out IDamageable id))
            {
                id.OnDamaged(_damage);
                _controller.Play("Hit");
                CameraManager.instance.PCamShake(5, 1, .5f);
                ms = hit.transform.GetComponent<MeshRenderer>();
                mat = ms.material;
                switch (mat.name[0])
                {
                    case 'R':
                        print("RED");
                        break;
                    case 'P':
                        print("PURPLE");
                        break;
                    case 'G':
                        print("GREEN");
                        break;
                    default:
                        Debug.LogError("Default");
                        break;
                }
            }
            else
            {
                _controller.Play("Hit");
                CameraManager.instance.PCamShake(5, 1, .5f);
                GetComponent<PlayerController>().PHit(0, true);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.forward + transform.position, _size);
    }
#endif
}
