using UnityEngine;

public class DestroyedBox : Props
{
    [SerializeField] private PieceBox[] _pieces;

    private void OnEnable() =>
        _pieces[0].Disappeared += Delete;

    private void OnDisable() =>
        _pieces[0].Disappeared -= Delete;

    public Rigidbody2D[] GetRigidbodies()
    {
        Rigidbody2D[] rigidbodies = new Rigidbody2D[_pieces.Length];

        for (int i = 0; i < _pieces.Length; i++)
            rigidbodies[i] = _pieces[i].Rigidbody;

        return rigidbodies;
    }
}