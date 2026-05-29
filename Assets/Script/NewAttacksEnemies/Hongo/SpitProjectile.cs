using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    [SerializeField] private float V0 = 15f;
    [SerializeField] private GameObject acidPoolPrefab;
    private Vector3 targetPos;
    private Rigidbody rb;
    [SerializeField]LayerMask layerMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Vector3 target)
    {
        targetPos = target;
        Launch();
    }

    private void Launch()
    {
        Vector3 horizontalDir = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z).normalized;
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                          new Vector3(targetPos.x, 0, targetPos.z));

        float g = Physics.gravity.magnitude;
        float sinValue = Mathf.Clamp((distance * g) / (V0 * V0), -1f, 1f);
        float angle = Mathf.Asin(sinValue) / 2f;

        float Vx = V0 * Mathf.Cos(angle);
        float Vy = V0 * Mathf.Sin(angle);

        rb.linearVelocity = (horizontalDir * Vx) + (Vector3.up * Vy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        float groundY = 0f;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("CorreWachin");
            collision.gameObject.GetComponent<PlayerMaster>().applyDamage(10f);
            if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f, layerMask))
            {
                Debug.Log("Homoerotico");
                print(hit.collider.name);
                groundY = hit.point.y;
            }
            Instantiate(acidPoolPrefab,new Vector3(transform.position.x, groundY, transform.position.z) , Quaternion.identity);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Piso"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f, layerMask))
            {
                Debug.Log("Homoerotico");
                print(hit.collider.name);
                groundY = hit.point.y;
            }
            Instantiate(acidPoolPrefab, new Vector3(transform.position.x, groundY, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }        
    }
}
