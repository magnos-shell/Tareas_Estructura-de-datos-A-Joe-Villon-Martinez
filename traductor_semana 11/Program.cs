class Program
{
    // Diccionarios para traducir en ambas direcciones
    static System.Collections.Generic.Dictionary<string, string> englishToSpanish = new System.Collections.Generic.Dictionary<string, string>();
    static System.Collections.Generic.Dictionary<string, string> spanishToEnglish = new System.Collections.Generic.Dictionary<string, string>();

    static void Main(string[] args)
    {
        InicializarDiccionario();
        MostrarMenu();
    }

    static void InicializarDiccionario()
    {
        // Palabras base del diccionario
        string[,] palabras = {
            {"time", "tiempo"},
            {"person", "persona"},
            {"year", "año"},
            {"way", "camino"},
            {"day", "día"},
            {"thing", "cosa"},
            {"man", "hombre"},
            {"world", "mundo"},
            {"life", "vida"},
            {"hand", "mano"},
            {"part", "parte"},
            {"child", "niño"},
            {"eye", "ojo"},
            {"woman", "mujer"},
            {"place", "lugar"},
            {"work", "trabajo"},
            {"week", "semana"},
            {"case", "caso"},
            {"point", "punto"},
            {"government", "gobierno"},
            {"company", "empresa"}
        };
// Llenar ambos diccionarios
        for (int i = 0; i < palabras.GetLength(0); i++)
        {
            string english = palabras[i, 0].ToLower();
            string spanish = palabras[i, 1].ToLower();
            
            englishToSpanish[english] = spanish;
            spanishToEnglish[spanish] = english;
        }
    }

    static void MostrarMenu()
    {
        int opcion;
        do
        {
            LimpiarPantalla();
            System.Console.WriteLine("=== TRADUCTOR BÁSICO INGLÉS-ESPAÑOL ===");
            System.Console.WriteLine("1. Traducir una frase");
            System.Console.WriteLine("2. Agregar palabras al diccionario");
            System.Console.WriteLine("0. Salir");
            System.Console.Write("Seleccione una opción: ");
            
            if (int.TryParse(System.Console.ReadLine(), out opcion))
            {
                switch (opcion)
                {
                    case 1:
                        TraducirFrase();
                        break;
                    case 2:
                        AgregarPalabra();
                        break;
                    case 0:
                        System.Console.WriteLine("¡Gracias por usar el traductor!");
                        break;
                    default:
                        System.Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                        System.Console.ReadKey();
                        break;
                }
            }
            else
            {
                System.Console.WriteLine("Por favor ingrese un número válido.");
                System.Console.ReadKey();
            }
        } while (opcion != 0);
    }

    static void TraducirFrase()
    {
        LimpiarPantalla();
        System.Console.WriteLine("=== TRADUCIR FRASE ===");
        System.Console.Write("Ingrese la frase a traducir: ");
        string frase = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(frase))
        {
            System.Console.WriteLine("No se ingresó ninguna frase.");
            System.Console.WriteLine("Presione cualquier tecla para continuar...");
            System.Console.ReadKey();
            return;
        }

        string fraseTraducida = ProcesarFrase(frase);
        
        System.Console.WriteLine();
        System.Console.WriteLine("Frase original: " + frase);
        System.Console.WriteLine("Traducción: " + fraseTraducida);
        System.Console.WriteLine();
        System.Console.WriteLine("Presione cualquier tecla para continuar...");
        System.Console.ReadKey();
    }

    static string ProcesarFrase(string frase)
    {
        // Separar la frase en palabras
        string[] palabras = frase.Split(' ');
        string[] palabrasTraducidas = new string[palabras.Length];

        for (int i = 0; i < palabras.Length; i++)
        {
            string palabra = palabras[i];
            string palabraLimpia = LimpiarPalabra(palabra);
            string palabraMinus = palabraLimpia.ToLower();
            
            // Verificar en qué diccionario buscar
            string traduccion = "";
            
            // Intentar traducir de inglés a español
            if (englishToSpanish.ContainsKey(palabraMinus))
            {
                traduccion = englishToSpanish[palabraMinus];
            }
            // Intentar traducir de español a inglés
            else if (spanishToEnglish.ContainsKey(palabraMinus))
            {
                traduccion = spanishToEnglish[palabraMinus];
            }
            
            // Si encontró traducción, mantener el formato original
            if (!string.IsNullOrEmpty(traduccion))
            {
                traduccion = AplicarFormato(palabra, palabraLimpia, traduccion);
                palabrasTraducidas[i] = palabra.Replace(palabraLimpia, traduccion);
            }
            else
            {
                // Si no encontró traducción, mantener la palabra original
                palabrasTraducidas[i] = palabra;
            }
        }

        return string.Join(" ", palabrasTraducidas);
    }

    static string LimpiarPalabra(string palabra)
    {
        string palabraLimpia = "";
        foreach (char c in palabra)
        {
            if (char.IsLetter(c))
            {
                palabraLimpia += c;
            }
        }
        return palabraLimpia;
    }

    static string AplicarFormato(string original, string limpia, string traduccion)
    {
        // Mantener mayúsculas/minúsculas del original
        if (limpia.Length > 0 && traduccion.Length > 0)
        {
            if (char.IsUpper(limpia[0]))
            {
                traduccion = char.ToUpper(traduccion[0]) + traduccion.Substring(1);
            }
            else
            {
                traduccion = char.ToLower(traduccion[0]) + traduccion.Substring(1);
            }
        }
        return traduccion;
    }

    static void LimpiarPantalla()
    {
        try
        {
            System.Console.Clear();
        }
        catch
        {
