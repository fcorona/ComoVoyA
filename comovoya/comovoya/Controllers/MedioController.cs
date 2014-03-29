using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using comovoya.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace comovoya.Controllers
{
    public class MedioController : ApiController
    {
        async Task<List<MedioPuntaje>> GetMedioPuntajeZona1(Zona zona1)
        {
            var particular = new MedioPuntaje
            {
                Puntaje = 5,
                Vehiculo = "PART"
            };
            var bici = new MedioPuntaje
            {
                Puntaje = 5,
                Vehiculo = "BIC"
            };
            var publico = new MedioPuntaje
            {
                Puntaje = 5,
                Vehiculo = "PUB"
            };

            var problemasDeSeguridad = await GetData(Endpoints.ProblemasSeguridadBarrio, zona1.Id);
            var enqueSeMueveLaGente = await GetData(Endpoints.QueMedioDeTransporteUsa, zona1.Id);
            var satisfechoViasBarrio = await GetData(Endpoints.SatisfechoViasBarrio, zona1.Id);


            var medioId = int.Parse(enqueSeMueveLaGente.Id);
            if (medioId >= 1 && medioId <= 5)
            {
                publico.Puntaje++;
            }
            else
                switch (medioId)
                {
                    case 8:
                    case 6:
                        bici.Puntaje++;
                        break;
                    case 7:
                    case 9:
                        particular.Puntaje++;
                        break;
                }


            var problemaId = int.Parse(problemasDeSeguridad.Id);
            switch (problemaId)
            {
                case 2:
                    bici.Puntaje--;
                    break;
                case 3:
                    particular.Puntaje--;
                    break;
                default:
                    publico.Puntaje++;
                    break;
            }

            var probcallesId = int.Parse(satisfechoViasBarrio.Id);
            switch (probcallesId)
            {
                case 1:
                case 2:
                    particular.Puntaje--;
                    publico.Puntaje--;
                    bici.Puntaje++;
                    break;
                case 4:
                case 5:
                    particular.Puntaje--;
                    publico.Puntaje--;
                    bici.Puntaje++;
                    break;
            }
            return new List<MedioPuntaje>
                    {
                        particular,
                        bici,
                        publico
                    };
        }

        async Task<List<MedioPuntaje>> GetMedioPuntajeZona2(Zona zona2)
        {
            var particular = new MedioPuntaje
            {
                Puntaje = 5,
                Vehiculo = "PART"
            };
            var bici = new MedioPuntaje
            {
                Puntaje = 5,
                Vehiculo = "BIC"
            };
            var publico = new MedioPuntaje
            {
                Puntaje = 5,
                Vehiculo = "PUB"
            };

            var problemasDeSeguridad = await GetData(Endpoints.ProblemasSeguridadBarrio, zona2.Id);
            var satisfechoViasBarrio = await GetData(Endpoints.SatisfechoViasBarrio, zona2.Id);

            var problemaId = int.Parse(problemasDeSeguridad.Id);
            switch (problemaId)
            {
                case 2:
                    bici.Puntaje--;
                    break;
                case 3:
                    particular.Puntaje--;
                    break;
                default:
                    publico.Puntaje++;
                    break;
            }

            var probcallesId = int.Parse(satisfechoViasBarrio.Id);
            switch (probcallesId)
            {
                case 1:
                case 2:
                    particular.Puntaje--;
                    publico.Puntaje--;
                    bici.Puntaje++;
                    break;
                case 4:
                case 5:
                    particular.Puntaje--;
                    publico.Puntaje--;
                    bici.Puntaje++;
                    break;
            }
            return new List<MedioPuntaje>
                    {
                        particular,
                        bici,
                        publico
                    };
        }

        public async Task<HttpResponseMessage> GetMedio([FromUri] Coordenada coordenada)
        {
            var zona1 = Zona.GetPorLocalidadId(coordenada.A);
            var zona2 = Zona.GetPorLocalidadId(coordenada.B);

            var lista1 = await GetMedioPuntajeZona1(zona1);
            var lista2 = await GetMedioPuntajeZona2(zona2);

            IEnumerable<MedioPuntaje> union = lista1.Union(lista2);

            IEnumerable<MedioPuntaje> ss = union.GroupBy(x => x.Vehiculo)
                .Select(g => new MedioPuntaje
                             {
                                 Puntaje = g.Sum(x => x.Puntaje)/2,
                                 Vehiculo = g.Key
                             });

            return Request.CreateResponse(HttpStatusCode.OK, ss);
        }



        async Task<Resultado> GetData(string url, int zoneId)
        {
            using (var webClient = new WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(
                    new Uri(
                        string.Format("{0}&zone={1}",
                            url,
                            zoneId.ToString(CultureInfo.InvariantCulture))
                        ));

                var d = JsonConvert.DeserializeObject<dynamic>(json);
                dynamic jObject = JObject.Parse(d.datas.ToString());
                var valores = new List<Valor>();
                foreach (var data in jObject)
                {
                    var valor = new Valor();
                    int valOut;
                    string name = data.Name.ToString();
                    if (!int.TryParse(name, out valOut)) continue;
                    valor.Year = valOut;
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, int>>(data.Value.ToString());
                    valor.Value = dic;
                    valores.Add(valor);
                }
                List<OptionValue> lista = JsonConvert.DeserializeObject<List<OptionValue>>(d.optionValues.ToString());

                var t = valores.Where(x => x.Year >= DateTime.UtcNow.Year - 4).ToList();
                KeyValuePair<string, int> dics = t.SelectMany(x => x.Value)
                    .GroupBy(x => x.Key)
                    .ToDictionary(grp => grp.Key, grp => grp.Sum(kvp => kvp.Value) / t.Count())
                    .OrderByDescending(x => x.Value)
                    .FirstOrDefault();
                var descripcion = lista.First(x => x.Option == dics.Key).Description;
                return new Resultado
                {
                    Id = dics.Key,
                    Valor = dics.Value,
                    Descripcion = descripcion
                };
            }
        }
    }

    public class Resultado
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
    }

    public class Pregunta
    {
        public string Descripcion { get; set; }

        public string Respuesta { get; set; }

        public int Puntaje { get; set; }
    }

    public class Coordenada
    {
        public string A { get; set; }
        public string B { get; set; }
    }
}