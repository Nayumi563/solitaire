using UnityEngine;

public class ContainerElement : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private void OnTriggerEnter2D(Collider2D _enterCollider)
    {
        Debug.Log(_enterCollider + "lkgmj");
    }
}
