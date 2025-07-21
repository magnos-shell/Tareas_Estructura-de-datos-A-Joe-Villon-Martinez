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
