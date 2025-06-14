class Programa
{
    static void Main(string[] args)
    {
        // Variables para guardar los datos del alumno
        int id;
        string nombres, apellidos, direccion;
        string[] telefonos = new string[3]; // Tres números de teléfono

        // Mensaje inicial para el usuario
        System.Console.WriteLine("=== REGISTRO DE ESTUDIANTES ===\n");

        // Pedimos el ID del estudiante
        System.Console.Write("Ingrese el ID del estudiante: ");
        id = System.Convert.ToInt32(System.Console.ReadLine());

        // Pedimos los nombres
        System.Console.Write("Ingrese los nombres: ");
        nombres = System.Console.ReadLine();

        // Pedimos los apellidos
        System.Console.Write("Ingrese los apellidos: ");
        apellidos = System.Console.ReadLine();

        // Pedimos la dirección
        System.Console.Write("Ingrese la dirección: ");
        direccion = System.Console.ReadLine();

        // Solicitamos los teléfonos uno por uno
        System.Console.WriteLine("\nIngrese los teléfonos del estudiante:");
        for (int i = 0; i < telefonos.Length; i++)
        {
            System.Console.Write($"Teléfono {i + 1}: ");
            telefonos[i] = System.Console.ReadLine();

        }
        // Mostramos toda la información que ingresó el usuario
        System.Console.WriteLine("\n=== DATOS DEL ESTUDIANTE ===");
        System.Console.WriteLine($"ID: {id}");
        System.Console.WriteLine($"Nombres: {nombres}");
        System.Console.WriteLine($"Apellidos: {apellidos}");
        System.Console.WriteLine($"Dirección: {direccion}");
        System.Console.WriteLine("Teléfonos:");
        for (int i = 0; i < telefonos.Length; i++)
        {
            System.Console.WriteLine($"  Teléfono {i + 1}: {telefonos[i]}");
        }

        // Esperamos que el usuario presione una tecla para terminar
        System.Console.WriteLine("\nPresione cualquier tecla para salir...");
        System.Console.ReadKey();
    }
}