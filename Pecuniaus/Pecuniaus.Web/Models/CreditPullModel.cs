using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Pecuniaus.Web.Models
{
    public class CreditPullModel
    {
        [Display(Name = "Credit Pull Type")]
        public IEnumerable<SelectListItem> Type { get; set; }
        
        public int typeid { get; set; }
        
        public CreditPullModel()
        {
            Type = new List<SelectListItem>();
            
        }

    }
    public class C
    {

        [JsonProperty("DCR")]
        public DCR DCR { get; set; }
    }

    public class ServiceResponse
    {

        [JsonProperty("c")]
        public C C { get; set; }
    }
    public class DCR
    {

        [JsonProperty("Foto")]
        public string Foto { get; set; }
        [JsonProperty("Seguridad")]
        public Seguridad Seguridad { get; set; }

        [JsonProperty("ErrorHandling")]
        public ErrorHandling ErrorHandling { get; set; }
        [JsonProperty("Individuo")]
        public Individuo Individuo { get; set; }
    }
    public class Seguridad
    {

        [JsonProperty("IdUsuario")]
        public string IdUsuario { get; set; }


        [JsonProperty("Afiliado")]
        public string Afiliates { get; set; }

        [JsonProperty("FechaHora")]
        public string DateFetched { get; set; }

        [JsonProperty("ConsultaId")]
        public string ConsultationId { get; set; }

        [JsonProperty("InformacionConsultada")]
        public string InformationConsulted { get; set; }
    }

    public class ErrorHandling
    {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
    public class Individuo
    {

        [JsonProperty("Nombres")]
        public string Name { get; set; }

        [JsonProperty("Apellidos")]
        public string Surname { get; set; }

        [JsonProperty("Apellidos1")]
        public string Surname1 { get; set; }

        [JsonProperty("Apellidos2")]
        public string Surname2 { get; set; }

        [JsonProperty("CedulaNueva")]
        public string NewId { get; set; }

        [JsonProperty("CedulaVieja")]
        public string OldId { get; set; }

        [JsonProperty("Pasaporte")]
        public object Pasaport { get; set; }

        [JsonProperty("Edad")]
        public string Age { get; set; }

        [JsonProperty("FechaNacimiento")]
        public string DOB { get; set; }

        [JsonProperty("LugarNacimiento")]
        public string BirthPlace { get; set; }

        [JsonProperty("EstadoCivil")]
        public object EstadoCivil { get; set; }

        [JsonProperty("Ocupacion")]
        public string Ocupation { get; set; }

        [JsonProperty("Categoria")]
        public object Category { get; set; }
        [JsonProperty("Sexo")]
        public string Sex { get; set; }

        [JsonProperty("Nacionalidad")]
        public string Nacionality { get; set; }

        [JsonProperty("Madre")]
        public string Mother { get; set; }

        [JsonProperty("Padre")]
        public string Father { get; set; }
    }
}