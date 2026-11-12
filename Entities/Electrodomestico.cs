namespace Recuperatorio.Entities;

public class Electrodomestico
{
    private int id;
    private string nombre;
    private string modelo;
    private bool estado;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Modelo { get => modelo; set => modelo = value; }
    public bool Estado { get => estado; set => estado = value; }
}
