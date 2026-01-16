using UnityEngine;

public class PencilMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Pencilrb;
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;

    public Vector2 Movementvector { get; private set; }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        Movementvector = MovementManager.Instance.GetMovementVectorNormalized();
    }
    // Update is called once per frame
    void FixedUpdate()

    {
        Vector2 targetvelocity = Movementvector * moveSpeed;
        
        Pencilrb.linearVelocity = Vector2.MoveTowards(Pencilrb.linearVelocity, 
        targetvelocity, 
        acceleration* Time.fixedDeltaTime);
        
    }
}
