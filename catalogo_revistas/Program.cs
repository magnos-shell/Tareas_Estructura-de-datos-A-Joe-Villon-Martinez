using System;
/// Clase principal que gestiona un catálogo de revistas
public class CatalogoRevistas
{
    // ==========================================
    // VARIABLES GLOBALES ESTÁTICAS
    // ==========================================
    
    /// <summary>
    /// Arreglo estático que almacena los títulos de las revistas
    /// Capacidad máxima: 20 revistas
    /// </summary>
    private static string[] catalogoRevistas = new string[20];
    
    /// <summary>
    /// Contador que mantiene el número actual de revistas en el catálogo
    /// </summary>
    private static int totalRevistas = 0;

    // ==========================================
    // MÉTODOS DE INICIALIZACIÓN
    // ==========================================
    
    /// <summary>
    /// Inicializa el catálogo con 12 títulos de revistas predefinidas
    /// </summary>
    public static void InicializarCatalogo()
    {
        catalogoRevistas[0] = "National Geographic";
        catalogoRevistas[1] = "Time Magazine";
        catalogoRevistas[2] = "Scientific American";
        catalogoRevistas[3] = "Forbes";
        catalogoRevistas[4] = "Vogue";
        catalogoRevistas[5] = "Popular Science";
        catalogoRevistas[6] = "Sports Illustrated";
        catalogoRevistas[7] = "The Economist";
        catalogoRevistas[8] = "Wired";
        catalogoRevistas[9] = "Harvard Business Review";
        catalogoRevistas[10] = "Nature";
        catalogoRevistas[11] = "People";
        
        totalRevistas = 12;
    }

    // ==========================================
    // ALGORITMOS DE BÚSQUEDA
    // ==========================================
    
    /// <summary>
    /// Implementación del algoritmo de búsqueda ITERATIVA
    /// Recorre secuencialmente todo el arreglo usando un bucle for
    /// </summary>
    /// <param name="titulo">Título a buscar</param>
    /// <returns>true si encuentra, false si no encuentra</returns>
    public static bool BusquedaIterativa(string titulo)
    {
        string tituloBuscar = titulo.ToLower();
        
        for (int i = 0; i < totalRevistas; i++)
        {
            if (catalogoRevistas[i].ToLower().IndexOf(tituloBuscar) >= 0)
            {
                return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// Implementación del algoritmo de búsqueda RECURSIVA
    /// Similar al ejemplo del factorial proporcionado
    /// </summary>
    /// <param name="titulo">Título a buscar</param>
    /// <returns>true si encuentra, false si no encuentra</returns>
    public static bool BusquedaRecursiva(string titulo)
    {
        string tituloBuscar = titulo.ToLower();
        return BusquedaRecursivaAux(tituloBuscar, 0);
    }

    /// <summary>
    /// Método auxiliar recursivo similar al factorial
    /// Tiene caso base y llamada recursiva
    /// </summary>
    /// <param name="titulo">Título a buscar</param>
    /// <param name="indice">Índice actual</param>
    /// <returns>true si encuentra, false si no encuentra</returns>
    private static bool BusquedaRecursivaAux(string titulo, int indice)
    {
        // Caso base 1: llegamos al final sin encontrar
        if (indice >= totalRevistas)
            return false;

        // Caso base 2: encontramos el título
        if (catalogoRevistas[indice].ToLower().IndexOf(titulo) >= 0)
            return true;

        // Llamada recursiva con siguiente índice
        return BusquedaRecursivaAux(titulo, indice + 1);
    }

    /// <summary>
    /// Obtiene el título completo de la revista encontrada
    /// </summary>
    /// <param name="titulo">Título a buscar</param>
    /// <returns>Nombre completo o cadena vacía</returns>
    public static string ObtenerRevista(string titulo)
    {
        string tituloBuscar = titulo.ToLower();
        
        for (int i = 0; i < totalRevistas; i++)
        {
            if (catalogoRevistas[i].ToLower().IndexOf(tituloBuscar) >= 0)
            {
                return catalogoRevistas[i];
            }
        }
        
        return "";
    }

    // ==========================================
    // MÉTODOS DE GESTIÓN DEL CATÁLOGO
    // ==========================================
    
    /// <summary>
    /// Muestra todo el contenido del catálogo
    /// </summary>
    public static void MostrarCatalogo()
    {
        Console.WriteLine("\n=== CATALOGO COMPLETO DE REVISTAS ===");
        Console.WriteLine("Total de revistas: " + totalRevistas);
        Console.WriteLine("--------------------------------------");
        
        for (int i = 0; i < totalRevistas; i++)
        {
            Console.WriteLine((i + 1) + ". " + catalogoRevistas[i]);
        }
        Console.WriteLine("--------------------------------------");
    }

    /// <summary>
    /// Permite agregar una nueva revista al catálogo
    /// </summary>
    public static void AgregarRevista()
    {
        if (totalRevistas >= catalogoRevistas.Length)
        {
            Console.WriteLine("Error: El catálogo está lleno.");
            return;
        }

        Console.Write("Ingrese el título de la nueva revista: ");
        string nuevoTitulo = Console.ReadLine();
        
        if (!string.IsNullOrEmpty(nuevoTitulo))
        {
            catalogoRevistas[totalRevistas] = nuevoTitulo;
            totalRevistas++;
            Console.WriteLine("Revista agregada exitosamente.");
        }
        else
        {
            Console.WriteLine("Error: Debe ingresar un título válido.");
        }
    }

    /// <summary>
    /// Muestra el menú principal de opciones
    /// </summary>
    public static void MostrarMenu()
    {
        Console.WriteLine("\n=== GESTOR DE CATALOGO DE REVISTAS ===");
        Console.WriteLine("1. Buscar título (Método Iterativo)");
        Console.WriteLine("2. Buscar título (Método Recursivo)");
        Console.WriteLine("3. Mostrar catálogo completo");
        Console.WriteLine("4. Agregar nueva revista");
        Console.WriteLine("5. Comparar algoritmos de búsqueda");
        Console.WriteLine("0. Salir");
        Console.WriteLine("======================================");
    }

    // ==========================================
    // INTERFACES DE BÚSQUEDA CON USUARIO
    // ==========================================
    
    /// <summary>
    /// Interfaz para realizar búsqueda iterativa
    /// </summary>
    public static void RealizarBusquedaIterativa()
    {
        Console.WriteLine("\n--- BUSQUEDA ITERATIVA ---");
        Console.Write("Ingrese el título a buscar: ");
        string titulo = Console.ReadLine();

        if (string.IsNullOrEmpty(titulo))
        {
            Console.WriteLine("Error: Debe ingresar un título.");
            return;
        }

        bool encontrado = BusquedaIterativa(titulo);

        Console.WriteLine("\n--- RESULTADO ---");
        Console.WriteLine("Título buscado: " + titulo);
        Console.WriteLine("Método utilizado: Iterativo");
        
        if (encontrado)
        {
            Console.WriteLine("Resultado: ENCONTRADO");
            string revistaCompleta = ObtenerRevista(titulo);
            if (revistaCompleta != "")
            {
                Console.WriteLine("Revista: " + revistaCompleta);
            }
        }
        else
        {
            Console.WriteLine("Resultado: NO ENCONTRADO");
        }
    }

    /// <summary>
    /// Interfaz para realizar búsqueda recursiva
    /// </summary>
    public static void RealizarBusquedaRecursiva()
    {
        Console.WriteLine("\n--- BUSQUEDA RECURSIVA ---");
        Console.Write("Ingrese el título a buscar: ");
        string titulo = Console.ReadLine();

        if (string.IsNullOrEmpty(titulo))
        {
            Console.WriteLine("Error: Debe ingresar un título.");
            return;
        }

        bool encontrado = BusquedaRecursiva(titulo);

        Console.WriteLine("\n--- RESULTADO ---");
        Console.WriteLine("Título buscado: " + titulo);
        Console.WriteLine("Método utilizado: Recursivo");
        
        if (encontrado)
        {
            Console.WriteLine("Resultado: ENCONTRADO");
            string revistaCompleta = ObtenerRevista(titulo);
            if (revistaCompleta != "")
            {
                Console.WriteLine("Revista: " + revistaCompleta);
            }
        }
        else
        {
            Console.WriteLine("Resultado: NO ENCONTRADO");
        }
    }

    /// <summary>
    /// Compara el rendimiento de ambos algoritmos
    /// </summary>
    public static void CompararAlgoritmos()
    {
