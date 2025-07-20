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
 // Constructor para inicializar la cola con un tamaño fijo
    public ColaPersonalizada(int tamaño)
    {
        capacidad = tamaño;
        elementos = new Persona[capacidad];
        frente = 0;
        fin = -1;
        cantidad = 0;
    }

    // Método para agregar una persona al final de la cola (Encolar)
    public bool Encolar(Persona p)
    {
        if (cantidad >= capacidad) return false; // Si la cola está llena, no se puede agregar
        fin = (fin + 1) % capacidad;              // Movemos el índice fin circularmente
        elementos[fin] = p;                        // Guardamos la persona en la nueva posición
        cantidad++;                               // Actualizamos la cantidad de elementos
        return true;                              // Confirmamos que se agregó correctamente
    }

    // Método para sacar una persona de la cola (Desencolar)
    public Persona Desencolar()
    {
        if (cantidad == 0) return null;           // Si la cola está vacía, no hay nada que sacar
        Persona p = elementos[frente];            // Tomamos la persona que está al frente
        elementos[frente] = null;                  // Opcional: liberamos la referencia para GC
        frente = (frente + 1) % capacidad;         // Avanzamos el índice frente circularmente
        cantidad--;                                // Reducimos la cantidad porque salió uno
        return p;                                  // Devolvemos la persona que salió
    }

    // Método que indica cuántas personas hay actualmente en la cola
    public int Cantidad()
    {
        return cantidad;
    }

    // Método para obtener una copia de las personas que están en la cola sin modificarla
    public Persona[] ObtenerElementos()
    {
        Persona[] resultado = new Persona[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            int indice = (frente + i) % capacidad;  // Índice circular para recorrer la cola
            resultado[i] = elementos[indice];       // Copiamos cada persona al arreglo resultado
        }
        return resultado;
    }
}
