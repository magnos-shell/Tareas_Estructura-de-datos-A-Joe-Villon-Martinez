// Clase que representa a un ciudadano del sistema de vacunación
public class Ciudadano
{
    // Propiedades del ciudadano
    public int Id { get; set; }           // Identificador único del ciudadano
    public string Nombre { get; set; }    // Nombre completo del ciudadano
    public string Cedula { get; set; }    // Número de cédula de identidad
    
    // Sobrescribir el método Equals para comparaciones correctas en HashSet
    public override bool Equals(object obj)
    {
        if (obj is Ciudadano ciudadano)
            return Id == ciudadano.Id;
        return false;
    }
// Sobrescribir GetHashCode para funcionamiento correcto en HashSet
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    // Sobrescribir ToString para mostrar información del ciudadano
    public override string ToString()
    {
        return $"{Nombre} (ID: {Id}, Cédula: {Cedula})";
    }
}

// Clase principal que maneja el sistema de vacunación COVID-19
public class SistemaVacunacion
{
    // Conjuntos para aplicar teoría de conjuntos
    private HashSet<Ciudadano> conjuntoTotalCiudadanos;      // Conjunto universal U
    private HashSet<Ciudadano> conjuntoVacunadosPfizer;      // Conjunto P (vacunados con Pfizer)
    private HashSet<Ciudadano> conjuntoVacunadosAstraZeneca; // Conjunto A (vacunados con AstraZeneca)
    private Random random; // Generador de números aleatorios
    
    // Constructor que inicializa el sistema
    public SistemaVacunacion()
    {
        random = new Random(42); // Semilla fija para reproducibilidad
        InicializarConjuntos();  // Inicializar todos los conjuntos
    }
    
    // Método que inicializa todos los conjuntos del sistema
    private void InicializarConjuntos()
    {
        // Crear el conjunto universal de 500 ciudadanos
        conjuntoTotalCiudadanos = GenerarCiudadanosFicticios(500);
        
        // Convertir a array para selección aleatoria
        var ciudadanosArray = conjuntoTotalCiudadanos.ToArray();
        
        // Crear conjunto de 75 ciudadanos vacunados con Pfizer
        conjuntoVacunadosPfizer = new HashSet<Ciudadano>();
        var indicesPfizer = GenerarIndicesAleatorios(75, ciudadanosArray.Length);
        foreach (var indice in indicesPfizer)
        {
            conjuntoVacunadosPfizer.Add(ciudadanosArray[indice]);
        }
        
        // Crear conjunto de 75 ciudadanos vacunados con AstraZeneca
        conjuntoVacunadosAstraZeneca = new HashSet<Ciudadano>();
        var indicesAstra = GenerarIndicesAleatorios(75, ciudadanosArray.Length);
        foreach (var indice in indicesAstra)
        {
            conjuntoVacunadosAstraZeneca.Add(ciudadanosArray[indice]);
        }
    }
    
    // Método que genera ciudadanos ficticios
    private HashSet<Ciudadano> GenerarCiudadanosFicticios(int cantidad)
    {
        var ciudadanos = new HashSet<Ciudadano>();
        
        // Generar la cantidad especificada de ciudadanos
        for (int i = 1; i <= cantidad; i++)
        {
            var ciudadano = new Ciudadano
            {
                Id = i,
                Nombre = $"Ciudadano {i}",
                Cedula = GenerarCedulaFicticia()
            };
            ciudadanos.Add(ciudadano);
        }
        
        return ciudadanos;
    }
    
    // Método que genera una cédula ficticia aleatoria
    private string GenerarCedulaFicticia()
    {
        // Formato: 2 dígitos iniciales + 8 dígitos aleatorios
        return $"{random.Next(10, 25)}{random.Next(10000000, 99999999)}";
    }
    
    // Método que genera índices aleatorios únicos para seleccionar ciudadanos
    private HashSet<int> GenerarIndicesAleatorios(int cantidad, int maximo)
    {
        var indices = new HashSet<int>();
        // Generar índices únicos hasta alcanzar la cantidad deseada
        while (indices.Count < cantidad)
        {
            indices.Add(random.Next(0, maximo));
        }
        return indices;
    }
    
    // 1. OPERACIÓN DE CONJUNTOS: Obtener ciudadanos que NO se han vacunado
    // Fórmula: U - (P ∪ A) donde U=total, P=Pfizer, A=AstraZeneca
    public HashSet<Ciudadano> ObtenerCiudadanosNoVacunados()
    {
        // Crear la unión de todos los vacunados (P ∪ A)
        var vacunados = new HashSet<Ciudadano>(conjuntoVacunadosPfizer);
        vacunados.UnionWith(conjuntoVacunadosAstraZeneca);
        
        // Obtener el complemento: U - (P ∪ A)
        var noVacunados = new HashSet<Ciudadano>(conjuntoTotalCiudadanos);
        noVacunados.ExceptWith(vacunados);
        
        return noVacunados;
    }
    
    // 2. OPERACIÓN DE CONJUNTOS: Obtener ciudadanos con ambas dosis
    // Fórmula: P ∩ A (intersección entre Pfizer y AstraZeneca)
    public HashSet<Ciudadano> ObtenerCiudadanosAmbasDosis()
    {
        // Calcular la intersección P ∩ A
        var ambasDosis = new HashSet<Ciudadano>(conjuntoVacunadosPfizer);
        ambasDosis.IntersectWith(conjuntoVacunadosAstraZeneca);
        
        return ambasDosis;
    }
    
    // 3. OPERACIÓN DE CONJUNTOS: Obtener ciudadanos que solo recibieron Pfizer
    // Fórmula: P - A (diferencia entre Pfizer y AstraZeneca)
    public HashSet<Ciudadano> ObtenerCiudadanosSoloPfizer()
    {
        // Calcular la diferencia P - A
        var soloPfizer = new HashSet<Ciudadano>(conjuntoVacunadosPfizer);
        soloPfizer.ExceptWith(conjuntoVacunadosAstraZeneca);
        
        return soloPfizer;
    }
    
    // 4. OPERACIÓN DE CONJUNTOS: Obtener ciudadanos que solo recibieron AstraZeneca
    // Fórmula: A - P (diferencia entre AstraZeneca y Pfizer)
    public HashSet<Ciudadano> ObtenerCiudadanosSoloAstraZeneca()
    {
        // Calcular la diferencia A - P
        var soloAstra = new HashSet<Ciudadano>(conjuntoVacunadosAstraZeneca);
        soloAstra.ExceptWith(conjuntoVacunadosPfizer);
        
        return soloAstra;
    }
    
    // Método que muestra las estadísticas generales del sistema
    public void MostrarEstadisticas()
    {
        Console.WriteLine("=== ESTADÍSTICAS DEL SISTEMA DE VACUNACIÓN COVID-19 ===");
        Console.WriteLine($"Total de ciudadanos: {conjuntoTotalCiudadanos.Count}");
        Console.WriteLine($"Ciudadanos con Pfizer: {conjuntoVacunadosPfizer.Count}");
        Console.WriteLine($"Ciudadanos con AstraZeneca: {conjuntoVacunadosAstraZeneca.Count}");
        Console.WriteLine();
    }
    
    // Método principal que genera el reporte completo
    public void GenerarReporte()
    {
        // Mostrar estadísticas generales
        MostrarEstadisticas();
        
        // === REPORTE 1: Ciudadanos no vacunados ===
        var noVacunados = ObtenerCiudadanosNoVacunados();
        Console.WriteLine("=== 1. CIUDADANOS NO VACUNADOS ===");
        Console.WriteLine($"Cantidad: {noVacunados.Count}");
        Console.WriteLine("Primeros 10 ciudadanos:");
        foreach (var ciudadano in noVacunados.Take(10))
        {
            Console.WriteLine($"  - {ciudadano}");
        }
        Console.WriteLine();
        
        // === REPORTE 2: Ciudadanos con ambas dosis ===
        var ambasDosis = ObtenerCiudadanosAmbasDosis();
        Console.WriteLine("=== 2. CIUDADANOS CON AMBAS DOSIS ===");
        Console.WriteLine($"Cantidad: {ambasDosis.Count}");
        if (ambasDosis.Count > 0)
        {
            Console.WriteLine("Lista completa:");
            foreach (var ciudadano in ambasDosis)
            {
                Console.WriteLine($"  - {ciudadano}");
            }
        }
        else
        {
            Console.WriteLine("No hay ciudadanos con ambas dosis.");
        }
        Console.WriteLine();
        
        // === REPORTE 3: Ciudadanos solo con Pfizer ===
        var soloPfizer = ObtenerCiudadanosSoloPfizer();
        Console.WriteLine("=== 3. CIUDADANOS SOLO CON PFIZER ===");
        Console.WriteLine($"Cantidad: {soloPfizer.Count}");
        Console.WriteLine("Primeros 10 ciudadanos:");
        foreach (var ciudadano in soloPfizer.Take(10))
        {
            Console.WriteLine($"  - {ciudadano}");
        }
        Console.WriteLine();
        
        // === REPORTE 4: Ciudadanos solo con AstraZeneca ===
        var soloAstra = ObtenerCiudadanosSoloAstraZeneca();
        Console.WriteLine("=== 4. CIUDADANOS SOLO CON ASTRAZENECA ===");
        Console.WriteLine($"Cantidad: {soloAstra.Count}");
        Console.WriteLine("Primeros 10 ciudadanos:");
        foreach (var ciudadano in soloAstra.Take(10))
        {
            Console.WriteLine($"  - {ciudadano}");
        }
        Console.WriteLine();
        
        // Verificar la correctitud de las operaciones
        VerificarOperacionesConjuntos();
    }
    
    // Método que verifica la correctitud matemática de las operaciones de conjuntos
    private void VerificarOperacionesConjuntos()
    {
        Console.WriteLine("=== VERIFICACIÓN DE OPERACIONES DE CONJUNTOS ===");
        
        // Obtener todos los resultados
        var noVacunados = ObtenerCiudadanosNoVacunados();
        var ambasDosis = ObtenerCiudadanosAmbasDosis();
        var soloPfizer = ObtenerCiudadanosSoloPfizer();
        var soloAstra = ObtenerCiudadanosSoloAstraZeneca();
        
        // Verificar que la suma de todas las categorías sea igual al total
        int totalCalculado = noVacunados.Count + ambasDosis.Count + soloPfizer.Count + soloAstra.Count;
        
        Console.WriteLine($"Total ciudadanos: {conjuntoTotalCiudadanos.Count}");
        Console.WriteLine($"Suma de categorías: {totalCalculado}");
        Console.WriteLine($"Verificación correcta: {totalCalculado == conjuntoTotalCiudadanos.Count}");
        Console.WriteLine();
        
        // Mostrar las fórmulas matemáticas utilizadas
        Console.WriteLine("=== FÓRMULAS DE TEORÍA DE CONJUNTOS APLICADAS ===");
        Console.WriteLine("U = Conjunto universal (todos los ciudadanos)");
        Console.WriteLine("P = Conjunto de vacunados con Pfizer");
        Console.WriteLine("A = Conjunto de vacunados con AstraZeneca");
        Console.WriteLine();
        Console.WriteLine("1. No vacunados: U - (P ∪ A)");
        Console.WriteLine("2. Ambas dosis: P ∩ A");
        Console.WriteLine("3. Solo Pfizer: P - A");
        Console.WriteLine("4. Solo AstraZeneca: A - P");
        Console.WriteLine();
    }
    
    // Método que guarda el reporte completo en un archivo de texto
    public void GuardarReporteEnArchivo(string nombreArchivo = "reporte_vacunacion.txt")
    {
        // Usar StreamWriter para escribir el archivo
        using (StreamWriter writer = new StreamWriter(nombreArchivo))
        {
            // Encabezado del reporte
            writer.WriteLine("REPORTE DEL SISTEMA DE VACUNACIÓN COVID-19");
            writer.WriteLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            writer.WriteLine(new string('=', 50));
            writer.WriteLine();
            
            // Escribir estadísticas generales
            writer.WriteLine("ESTADÍSTICAS GENERALES:");
            writer.WriteLine($"Total de ciudadanos: {conjuntoTotalCiudadanos.Count}");
            writer.WriteLine($"Ciudadanos con Pfizer: {conjuntoVacunadosPfizer.Count}");
            writer.WriteLine($"Ciudadanos con AstraZeneca: {conjuntoVacunadosAstraZeneca.Count}");
            writer.WriteLine();
            
            // Definir todas las categorías a reportar
            var categorias = new Dictionary<string, HashSet<Ciudadano>>
            {
                {"CIUDADANOS NO VACUNADOS", ObtenerCiudadanosNoVacunados()},
                {"CIUDADANOS CON AMBAS DOSIS", ObtenerCiudadanosAmbasDosis()},
                {"CIUDADANOS SOLO CON PFIZER", ObtenerCiudadanosSoloPfizer()},
                {"CIUDADANOS SOLO CON ASTRAZENECA", ObtenerCiudadanosSoloAstraZeneca()}
            };
            
            // Escribir cada categoría en el archivo
            foreach (var categoria in categorias)
            {
                writer.WriteLine($"{categoria.Key}:");
                writer.WriteLine($"Cantidad: {categoria.Value.Count}");
                writer.WriteLine();
                
                // Escribir todos los ciudadanos de la categoría
                foreach (var ciudadano in categoria.Value)
                {
                    writer.WriteLine($"  - {ciudadano}");
                }
                writer.WriteLine();
                writer.WriteLine(new string('-', 40));
                writer.WriteLine();
            }
