using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectDropController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs;
    [SerializeField] private float objectCount = 1;
    [SerializeField] private float dropForce = 15f;

    public void DropObjects()
    {
        for (int i = 0; i < objectCount; i++)
        {
            GameObject droppedObject = Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Length)], transform.position, Quaternion.identity);

            Rigidbody2D droppedObjectRb = droppedObject.GetComponent<Rigidbody2D>();
            Vector2 randomDropForce = Random.insideUnitCircle * dropForce;
            droppedObjectRb.velocity = randomDropForce;
            // TODO: Random angular momentum on rigidbody
        }
    }
}