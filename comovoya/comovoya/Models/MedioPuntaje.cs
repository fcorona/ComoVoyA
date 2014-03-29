using System.Collections.Generic;
using comovoya.Controllers;

namespace comovoya.Models
{
    public class MedioPuntaje
    {
        public string Vehiculo { get; set; }

        public int Puntaje { get; set; }

        public IList<Pregunta> Preguntas { get; set; }
    }
}