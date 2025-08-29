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
