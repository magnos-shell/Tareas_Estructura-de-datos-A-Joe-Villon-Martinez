// Clase para representar un Jugador
public class Jugador
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Posicion { get; set; }
    public int Edad { get; set; }
    public int NumeroCamiseta { get; set; }

    public Jugador(int id, string nombre, string posicion, int edad, int numeroCamiseta)
    {
        Id = id;
        Nombre = nombre;
        Posicion = posicion;
        Edad = edad;
        NumeroCamiseta = numeroCamiseta;
    }

    public string ToString()
    {
        return "ID: " + Id + ", " + Nombre + " - #" + NumeroCamiseta + " (" + Posicion + ", " + Edad + " años)";
    }

    public bool Equals(object obj)
    {
        if (obj is Jugador jugador)
            return Id == jugador.Id;
        return false;
    }

    public int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

// Clase para representar un Equipo
public class Equipo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ciudad { get; set; }
    public System.Collections.Generic.HashSet<Jugador> Jugadores { get; set; } // Conjunto de jugadores
    public string Entrenador { get; set; }

    public Equipo(int id, string nombre, string ciudad, string entrenador)
    {
        Id = id;
        Nombre = nombre;
        Ciudad = ciudad;
        Entrenador = entrenador;
        Jugadores = new System.Collections.Generic.HashSet<Jugador>(); // Conjunto vacío inicialmente
    }

    public bool AgregarJugador(Jugador jugador)
    {
        // Un equipo no puede tener más de 25 jugadores
        if (Jugadores.Count >= 25)
        {
            System.Console.WriteLine("Error: El equipo " + Nombre + " ya tiene el máximo de 25 jugadores.");
            return false;
        }

        // Verificar que no haya números de camiseta duplicados
        foreach (var j in Jugadores)
        {
            if (j.NumeroCamiseta == jugador.NumeroCamiseta)
            {
                System.Console.WriteLine("Error: Ya existe un jugador con el número " + jugador.NumeroCamiseta + " en " + Nombre + ".");
                return false;
            }
        }

        return Jugadores.Add(jugador); // Add retorna false si el elemento ya existe
    }

    public bool RemoverJugador(int idJugador)
    {
        Jugador jugadorARemover = null;
        foreach (var j in Jugadores)
        {
            if (j.Id == idJugador)
            {
                jugadorARemover = j;
                break;
            }
        }
        
        if (jugadorARemover != null)
        {
            return Jugadores.Remove(jugadorARemover);
        }
        return false;
    }

    public string ToString()
    {
        return Nombre + " - " + Ciudad + " (Entrenador: " + Entrenador + ", Jugadores: " + Jugadores.Count + ")";
    }

    public bool Equals(object obj)
    {
        if (obj is Equipo equipo)
            return Id == equipo.Id;
        return false;
    }

    public int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

// Clase principal del sistema de torneo
public class SistemaTorneo
{
    // Mapas (Diccionarios) para almacenar equipos y jugadores
    private System.Collections.Generic.Dictionary<int, Equipo> equipos; // Mapa: ID -> Equipo
    private System.Collections.Generic.Dictionary<int, Jugador> jugadores; // Mapa: ID -> Jugador
    private System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<Equipo>> equiposPorCiudad; // Mapa: Ciudad -> Conjunto de Equipos

    // Conjuntos para diferentes categorías
    private System.Collections.Generic.HashSet<string> posicionesValidas;
    private System.Collections.Generic.HashSet<int> numerosReservados; // Números de camiseta reservados

    public SistemaTorneo()
    {
        equipos = new System.Collections.Generic.Dictionary<int, Equipo>();
        jugadores = new System.Collections.Generic.Dictionary<int, Jugador>();
        equiposPorCiudad = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<Equipo>>();
        
        // Inicializar conjuntos
        posicionesValidas = new System.Collections.Generic.HashSet<string>();
        posicionesValidas.Add("Portero");
        posicionesValidas.Add("Defensa");
        posicionesValidas.Add("Centrocampista");
        posicionesValidas.Add("Delantero");
        
        numerosReservados = new System.Collections.Generic.HashSet<int>();
        numerosReservados.Add(12); // Ejemplo: número 12 reservado
    }

    // Registrar un nuevo equipo
    public bool RegistrarEquipo(int id, string nombre, string ciudad, string entrenador)
    {
        if (equipos.ContainsKey(id))
        {
            System.Console.WriteLine("Error: Ya existe un equipo con ID " + id + ".");
            return false;
        }

        var nuevoEquipo = new Equipo(id, nombre, ciudad, entrenador);
        equipos[id] = nuevoEquipo;

        // Agregar al mapa de equipos por ciudad
        if (!equiposPorCiudad.ContainsKey(ciudad))
        {
            equiposPorCiudad[ciudad] = new System.Collections.Generic.HashSet<Equipo>();
        }
        equiposPorCiudad[ciudad].Add(nuevoEquipo);

        System.Console.WriteLine("Equipo '" + nombre + "' registrado exitosamente.");
        return true;
    }

    // Registrar un nuevo jugador
    public bool RegistrarJugador(int id, string nombre, string posicion, int edad, int numeroCamiseta)
    {
        if (jugadores.ContainsKey(id))
        {
            System.Console.WriteLine("Error: Ya existe un jugador con ID " + id + ".");
            return false;
        }

        if (!posicionesValidas.Contains(posicion))
        {
            System.Console.WriteLine("Error: Posición '" + posicion + "' no es válida. Posiciones válidas: Portero, Defensa, Centrocampista, Delantero");
            return false;
        }

        if (numerosReservados.Contains(numeroCamiseta))
        {
            System.Console.WriteLine("Error: El número " + numeroCamiseta + " está reservado.");
            return false;
        }

        var nuevoJugador = new Jugador(id, nombre, posicion, edad, numeroCamiseta);
        jugadores[id] = nuevoJugador;

        System.Console.WriteLine("Jugador '" + nombre + "' registrado exitosamente.");
        return true;
    }

    // Asignar jugador a equipo
    public bool AsignarJugadorAEquipo(int idJugador, int idEquipo)
    {
        if (!jugadores.ContainsKey(idJugador))
        {
            System.Console.WriteLine("Error: No existe jugador con ID " + idJugador + ".");
            return false;
        }

        if (!equipos.ContainsKey(idEquipo))
        {
            System.Console.WriteLine("Error: No existe equipo con ID " + idEquipo + ".");
            return false;
        }

        var jugador = jugadores[idJugador];
        var equipo = equipos[idEquipo];

        if (equipo.AgregarJugador(jugador))
        {
            System.Console.WriteLine("Jugador '" + jugador.Nombre + "' asignado al equipo '" + equipo.Nombre + "'.");
            return true;
        }

        return false;
    }

    // Operaciones de conjuntos entre equipos
    public void MostrarOperacionesConjuntos(int idEquipo1, int idEquipo2)
    {
        if (!equipos.ContainsKey(idEquipo1) || !equipos.ContainsKey(idEquipo2))
        {
            System.Console.WriteLine("Error: Uno o ambos equipos no existen.");
            return;
        }

        var equipo1 = equipos[idEquipo1];
        var equipo2 = equipos[idEquipo2];

        System.Console.WriteLine("\n=== OPERACIONES DE CONJUNTOS ENTRE " + equipo1.Nombre + " Y " + equipo2.Nombre + " ===");

        // Intersección (jugadores en común)
        var interseccion = new System.Collections.Generic.HashSet<Jugador>();
        foreach (var jugador in equipo1.Jugadores)
        {
            if (equipo2.Jugadores.Contains(jugador))
            {
                interseccion.Add(jugador);
            }
        }
        System.Console.WriteLine("Intersección (jugadores en común): " + interseccion.Count);

        // Unión
        var union = new System.Collections.Generic.HashSet<Jugador>(equipo1.Jugadores);
        foreach (var jugador in equipo2.Jugadores)
        {
            union.Add(jugador);
        }
        System.Console.WriteLine("Unión (total de jugadores únicos): " + union.Count);

        // Diferencia (jugadores solo en equipo1)
        var diferencia = new System.Collections.Generic.HashSet<Jugador>();
        foreach (var jugador in equipo1.Jugadores)
        {
            if (!equipo2.Jugadores.Contains(jugador))
            {
                diferencia.Add(jugador);
            }
        }
        System.Console.WriteLine("Diferencia (" + equipo1.Nombre + " - " + equipo2.Nombre + "): " + diferencia.Count);

        // Diferencia simétrica
        var diferenciaSimetrica = new System.Collections.Generic.HashSet<Jugador>();
        foreach (var jugador in equipo1.Jugadores)
        {
            if (!equipo2.Jugadores.Contains(jugador))
            {
                diferenciaSimetrica.Add(jugador);
            }
        }
        foreach (var jugador in equipo2.Jugadores)
        {
            if (!equipo1.Jugadores.Contains(jugador))
            {
                diferenciaSimetrica.Add(jugador);
            }
        }
        System.Console.WriteLine("Diferencia simétrica: " + diferenciaSimetrica.Count);
    }

    // Mostrar estadísticas usando operaciones de conjuntos
    public void MostrarEstadisticas()
    {
        System.Console.WriteLine("\n=== ESTADÍSTICAS DEL TORNEO ===");
        System.Console.WriteLine("Total de equipos: " + equipos.Count);
        System.Console.WriteLine("Total de jugadores registrados: " + jugadores.Count);

        // Conjunto de todas las posiciones representadas
        var todasLasPosiciones = new System.Collections.Generic.HashSet<string>();
        foreach (var jugador in jugadores.Values)
        {
            todasLasPosiciones.Add(jugador.Posicion);
        }
        
        string posiciones = "";
        foreach (var pos in todasLasPosiciones)
        {
            if (posiciones != "") posiciones += ", ";
            posiciones += pos;
        }
        System.Console.WriteLine("Posiciones representadas: " + posiciones);

        // Equipos por ciudad
        System.Console.WriteLine("\nEquipos por ciudad:");
        foreach (var ciudad in equiposPorCiudad.Keys)
        {
            System.Console.WriteLine("  " + ciudad + ": " + equiposPorCiudad[ciudad].Count + " equipos");
        }

        // Jugadores por posición
        var jugadoresPorPosicion = new System.Collections.Generic.Dictionary<string, int>();
        foreach (var jugador in jugadores.Values)
        {
            if (jugadoresPorPosicion.ContainsKey(jugador.Posicion))
            {
                jugadoresPorPosicion[jugador.Posicion]++;
            }
            else
            {
                jugadoresPorPosicion[jugador.Posicion] = 1;
            }
        }
        
        System.Console.WriteLine("\nJugadores por posición:");
        foreach (var posicion in jugadoresPorPosicion.Keys)
        {
            System.Console.WriteLine("  " + posicion + ": " + jugadoresPorPosicion[posicion] + " jugadores");
        }
    }

    // Listar todos los equipos
    public void ListarEquipos()
    {
        System.Console.WriteLine("\n=== EQUIPOS REGISTRADOS ===");
        foreach (var equipo in equipos.Values)
        {
            System.Console.WriteLine(equipo.ToString());
            if (equipo.Jugadores.Count > 0)
            {
                System.Console.WriteLine("  Jugadores:");
                
                // Ordenar jugadores por número de camiseta
                var jugadoresOrdenados = new System.Collections.Generic.List<Jugador>(equipo.Jugadores);
                jugadoresOrdenados.Sort((j1, j2) => j1.NumeroCamiseta.CompareTo(j2.NumeroCamiseta));
                
                foreach (var jugador in jugadoresOrdenados)
                {
                    System.Console.WriteLine("    " + jugador.ToString());
                }
            }
            System.Console.WriteLine();
        }
    }

    // Buscar equipos por ciudad
    public void BuscarEquiposPorCiudad(string ciudad)
    {
        if (equiposPorCiudad.ContainsKey(ciudad))
        {
            System.Console.WriteLine("\n=== EQUIPOS EN " + ciudad.ToUpper() + " ===");
            foreach (var equipo in equiposPorCiudad[ciudad])
            {
                System.Console.WriteLine(equipo.ToString());
            }
        }
        else
        {
            System.Console.WriteLine("No hay equipos registrados en " + ciudad + ".");
        }
    }
}

// Programa principal
class Program
{
    static void Main(string[] args)
    {
        var sistema = new SistemaTorneo();

        System.Console.WriteLine("=== SISTEMA DE REGISTRO DE TORNEO DE FÚTBOL ===\n");

        // Registrar equipos
        sistema.RegistrarEquipo(1, "Barcelona SC", "Guayaquil", "Carlos Alfaro");
        sistema.RegistrarEquipo(2, "Liga de Quito", "Quito", "Luis Zubeldía");
        sistema.RegistrarEquipo(3, "El Nacional", "Quito", "Marcelo Zuleta");
        sistema.RegistrarEquipo(4, "Emelec", "Guayaquil", "Hernán Torres");

        System.Console.WriteLine();

        // Registrar jugadores
        sistema.RegistrarJugador(101, "Damián Díaz", "Centrocampista", 34, 10);
        sistema.RegistrarJugador(102, "Carlos Garcés", "Portero", 28, 1);
        sistema.RegistrarJugador(103, "Bruno Piñatares", "Defensa", 25, 4);
        sistema.RegistrarJugador(104, "Michael Hoyos", "Delantero", 29, 9);
        
        sistema.RegistrarJugador(201, "Alexander Domínguez", "Portero", 35, 1);
        sistema.RegistrarJugador(202, "Moisés Caicedo", "Centrocampista", 22, 8);
        sistema.RegistrarJugador(203, "Enner Valencia", "Delantero", 33, 13);
        
        sistema.RegistrarJugador(301, "Sebastián Pérez", "Centrocampista", 27, 5);
        sistema.RegistrarJugador(302, "Marco Angulo", "Centrocampista", 22, 7);

        System.Console.WriteLine();

        // Asignar jugadores a equipos
        sistema.AsignarJugadorAEquipo(101, 1); // Damián Díaz -> Barcelona SC
        sistema.AsignarJugadorAEquipo(102, 1); // Carlos Garcés -> Barcelona SC
        sistema.AsignarJugadorAEquipo(103, 1); // Bruno Piñatares -> Barcelona SC
        sistema.AsignarJugadorAEquipo(104, 1); // Michael Hoyos -> Barcelona SC

        sistema.AsignarJugadorAEquipo(201, 2); // Alexander Domínguez -> Liga de Quito
        sistema.AsignarJugadorAEquipo(202, 2); // Moisés Caicedo -> Liga de Quito
        sistema.AsignarJugadorAEquipo(203, 2); // Enner Valencia -> Liga de Quito

        sistema.AsignarJugadorAEquipo(301, 3); // Sebastián Pérez -> El Nacional
        sistema.AsignarJugadorAEquipo(302, 3); // Marco Angulo -> El Nacional

        // Mostrar información
        sistema.ListarEquipos();
        sistema.MostrarEstadisticas();
        sistema.BuscarEquiposPorCiudad("Quito");
        sistema.MostrarOperacionesConjuntos(1, 2); // Barcelona SC vs Liga de Quito

        System.Console.WriteLine("\nPresione cualquier tecla para salir...");
        System.Console.ReadKey();
    }
}
    