using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    void Awake()
    {
        Invoke("ThisDestroy", 2f);
    }

    private void ThisDestroy()
    {
        Destroy(gameObject);
    }
}
