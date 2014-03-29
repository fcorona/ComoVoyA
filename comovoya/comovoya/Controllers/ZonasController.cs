using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace comovoya.Controllers
{
    public class ZonasController : ApiController
    {



        public HttpResponseMessage GetLocalidades(string id)
        {
            IEnumerable<Localidad> localidades = (from z in Zona.Zonas
                                                  let ls = z.Localidades
                                                  from l in ls
                                                  where l.Nombre.ToLower().StartsWith(id.ToLower())
                                                  select l).ToList();
            if (localidades.Any())
                return Request.CreateResponse(HttpStatusCode.OK, localidades);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontró la localidad");
        }
    }

    public class Localidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Zona
    {
        public static IList<Zona> Zonas = new[]
                                              {
                                                  new Zona
                                                  {
                                                      Id = 1,
                                                      Nombre = "Occidental",
                                                      Localidades = new[]
                                                                    {
                                                                        new Localidad
                                                                        {
                                                                            Id = 1,
                                                                            Nombre = "Ciudad Kennedy",
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 2,
                                                                            Nombre = "Fontibon"
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 3,
                                                                            Nombre = "Engativa"
                                                                        }
                                                                    }
                                                  },
                                                  new Zona
                                                  {
                                                      Id = 2,
                                                      Nombre = "Sur occidental",
                                                      Localidades = new[]
                                                                    {
                                                                        new Localidad
                                                                        {
                                                                            Id = 4,
                                                                            Nombre = "Tunjuelito",
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 5,
                                                                            Nombre = "Bosa"
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 6,
                                                                            Nombre = "Ciudad Bolívar"
                                                                        }
                                                                    }
                                                  },
                                                  new Zona
                                                  {
                                                      Id = 3,
                                                      Nombre = "Sur oriental",
                                                      Localidades = new[]
                                                                    {
                                                                        new Localidad
                                                                        {
                                                                            Id = 7,
                                                                            Nombre = "San Cristobal",
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 8,
                                                                            Nombre = "Usme",
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 9,
                                                                            Nombre = "Antonio Nariño"
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 10,
                                                                            Nombre = "Rafael Uribe Uribe"
                                                                        }
                                                                    }
                                                  },
                                                  new Zona
                                                  {
                                                      Id = 4,
                                                      Nombre = " Centro",
                                                      Localidades = new[]
                                                                    {
                                                                        new Localidad
                                                                        {
                                                                            Id = 11,
                                                                            Nombre = "Santa Fe",
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 12,
                                                                            Nombre = "Los Martires"
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 13,
                                                                            Nombre = "Puente Aranda"
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 14,
                                                                            Nombre = "Candelaria"
                                                                        }
                                                                    }
                                                  },
                                                  new Zona
                                                  {
                                                      Id = 5,
                                                      Nombre = " Chapinero",
                                                      Localidades = new[]
                                                                    {
                                                                        new Localidad
                                                                        {
                                                                            Id = 15,
                                                                            Nombre = "Chapinero",
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 16,
                                                                            Nombre = "Barrios Unidos"
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 17,
                                                                            Nombre = "Teusaquillo"
                                                                        }
                                                                    }
                                                  },
                                                  new Zona
                                                  {
                                                      Id = 6,
                                                      Nombre = " Norte",
                                                      Localidades = new[]
                                                                    {
                                                                        new Localidad
                                                                        {
                                                                            Id = 18,
                                                                            Nombre = "Usaquen",
                                                                        },
                                                                        new Localidad
                                                                        {
                                                                            Id = 19,
                                                                            Nombre = "Suba"
                                                                        }
                                                                    }
                                                  }
                                              };

        public int Id { get; set; }
        public string Nombre { get; set; }
        public IList<Localidad> Localidades { get; set; }

        public static Zona GetPorLocalidadId(string localidadId)
        {
          
            return (from z in Zonas
                   let ls = z.Localidades
                   from l in ls
                   where String.Equals(l.Nombre, localidadId, StringComparison.CurrentCultureIgnoreCase)
                   select z).FirstOrDefault();
        }
    }
}