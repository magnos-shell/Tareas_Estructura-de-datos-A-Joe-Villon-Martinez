// Clase para representar a una persona que está en la cola
class Persona
{
    public string Nombre;        // Nombre de la persona
    public int NumeroAsiento;    // Número del asiento asignado
}

// Clase para implementar una cola usando un arreglo circular
class ColaPersonalizada
{
    private Persona[] elementos; // Arreglo donde almacenamos las personas
    private int capacidad;       // Tamaño máximo de la cola (30 en este caso)
    private int frente;          // Índice del primer elemento en cola
    private int fin;             // Índice del último elemento en cola
    private int cantidad;        // Cuántos elementos hay actualmente

