using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    public Tilemap tilemap;
    public Grid grid;
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = getMove();
        transform.Translate(move);


        var pos = grid.WorldToCell(transform.position);
        var tileBase = tilemap.GetTile(pos);
        // Debug.Log(tileBase.name);
        // Debug.Log(pos);

    }

    private Vector2 getMove()
    {
        var isUp = Input.GetKeyDown(KeyCode.W);
        var isDown = Input.GetKeyDown(KeyCode.S);
        var isLeft = Input.GetKeyDown(KeyCode.A);
        var isRight = Input.GetKeyDown(KeyCode.D);

        Vector3 foo = new Vector3();
        if (isUp)
        {
            return Vector2.up;
        }

        if (isDown)
        {
            return Vector2.down;
        }

        if (isLeft)
        {
            return Vector2.left;
        }

        if (isRight)
        {
            return Vector2.right;
        }

        return Vector2.zero;
    }

}
