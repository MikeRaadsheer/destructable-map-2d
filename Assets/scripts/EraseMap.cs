using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseMap : MonoBehaviour
{
    private SpriteRenderer _rend;
    public Sprite _sprite;
    private Texture2D _texture;
    Color[] _colors;

    private int _width;
    private int _height;

    void Start()
    {
        _texture = new Texture2D(_sprite.texture.width, _sprite.texture.height);
        _rend = GetComponent<SpriteRenderer>();


        _width = _sprite.texture.width;
        _height = _sprite.texture.height;


        _colors = _sprite.texture.GetPixels();

        _texture.SetPixels(_colors);
        _rend.sprite = _sprite;

    }

    //This function will draw a circle onto the texture at position cx, cy with radius r
    public void DrawOnsprite(int cx, int cy, int r)
    {
        int px, nx, py, ny, d;

        for (int x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));

            for (int y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                _colors[py * _width * py] = new Color(1, 1, 1, 1);
                _colors[py * _width + nx] = new Color(1, 1, 1, 1);
                _colors[ny * _height + px] = new Color(1, 1, 1, 1);
                _colors[ny * _height + nx] = new Color(1, 1, 1, 1);
            }
        }

        _texture.SetPixels(_colors);
        _texture.Apply(false);
    }


    void Update()
    {

        //Get mouse coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Check if mouse button is held down
        if (Input.GetMouseButton(0))
        {
            //Check if we hit the collider
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                //Normalize to the texture coodinates
                int y = (int)((0.5 - (transform.position - mousePosition).y) * _height);
                int x = (int)((0.5 - (transform.position - mousePosition).x) * _width);

                //Draw onto the sprite
                DrawOnsprite(x, y, 5);
            }
        }
    }
}
