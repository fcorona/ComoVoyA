using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace comovoya.Models
{

    public class OptionValue
    {
        public string Description { get; set; }
        public string Option { get; set; }
    }


    public class Valor
    {
        public Valor()
        {
            Value = new Dictionary<string, int>();
        }
        public int Year { get; set; }

        public IDictionary<string, int> Value { get; set; }
    }

    public class Endpoints
    {
        //MOVILIDAD
        //¿Qué medio de transporte usa Usted principalmente para desplazarse a su trabajo, oficina o estudio?
        public static string QueMedioDeTransporteUsa = "http://api.bogotacomovamos.org/api/datas/2269/?key=comovamos";
        //¿cómo califica la gestión de la Empresa de Transporte del Tercer Milenio -Transmilenio S.A.?
        public static string GestionEmpresaTransmilenio = "http://api.bogotacomovamos.org/api/datas/2759/?key=comovamos";
        //¿Qué tan satisfecho(a) está usted con el estado de las vías en general de Bogotá usando estas opciones?
        public static string SatisfechoEstadoViasBogota = "http://api.bogotacomovamos.org/api/datas/2302/?key=comovamos";
        //¿Qué tan satisfecho(a) está usted en general con el estado de las vías en su barrio
        public static string SatisfechoViasBarrio = "http://api.bogotacomovamos.org/api/datas/1906/?key=comovamos";


        //SEGURIDAD
        //¿cuáles son los problemas más graves en relación con la seguridad que se presentan en su barrio?
        public static string ProblemasSeguridadBarrio = "http://api.bogotacomovamos.org/api/datas/1960/?key=comovamos";



        //Por clasificar:
        //        La semaforización de vías
        //http://api.bogotacomovamos.org/datasets/2288

        //La señalización de vías| suficiencia y coherencia
        //http://api.bogotacomovamos.org/datasets/2290

        //- - - - - - - 

        //Mide el número de casos reportados en el año
        //http://api.bogotacomovamos.org/datasets/82

        //Mide el número de vehículos hurtados en el año
        //http://api.bogotacomovamos.org/datasets/80	

        //Mide el número de motos hurtadas en el año
        //http://api.bogotacomovamos.org/datasets/81

        //Mide el promedio KM/H en la ciudad
        //http://api.bogotacomovamos.org/datasets/134

        //Mide el número kilómetro carril en buen estado sobre el total kilómetros carril
        //http://api.bogotacomovamos.org/datasets/127

        //Mide el número de vehículos de transporte particular registrado
        //http://api.bogotacomovamos.org/datasets/136



    }
}