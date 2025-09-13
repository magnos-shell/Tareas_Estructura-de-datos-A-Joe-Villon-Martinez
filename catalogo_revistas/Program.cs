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
    