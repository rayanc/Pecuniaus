using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Bridge.Models
{
     public class ReportResponse
        {

            [JsonProperty("c")]
            public C C;
        }
        public class C
        {

            [JsonProperty("DCR")]
            public DCR DCR;
        }
        public class Empresa
        {

            [JsonProperty("Nombre")]
            public string Nombre { get; set; }

            [JsonProperty("NombreComercial")]
            public string NombreComercial { get; set; }

            [JsonProperty("RNC")]
            public string RNC { get; set; }

            [JsonProperty("Siglas")]
            public object Siglas { get; set; }

            [JsonProperty("TipoEmpresa")]
            public string TipoEmpresa { get; set; }

            [JsonProperty("ActividadComercial")]
            public string ActividadComercial { get; set; }

            [JsonProperty("FechaConstitucion")]
            public string FechaConstitucion { get; set; }

            [JsonProperty("Edad")]
            public object Edad { get; set; }
        }
      
        [JsonObject("Seguridad")]
        public class MemberDetails
        {

            [JsonProperty("@Descripcion")]
            public string Description;

            [JsonProperty("IdUsuario")]
            public string UserId;

            [JsonProperty("NombreUsuario")]
            public string UserName;

            [JsonProperty("Afiliado")]
            public string Afiliates;

            [JsonProperty("Oficina")]
            public string Office;

            [JsonProperty("Departamento")]
            public string Department;

            [JsonProperty("FechaHora")]
            public string DateFetched;

            [JsonProperty("DireccionIp")]
            public string IPAddress;

            [JsonProperty("UsuarioSistema")]
            public object UsuarioSistema;

            [JsonProperty("ConsultaId")]
            public string ConsultationId;

            [JsonProperty("InformacionConsultada")]
            public string InformationConsulted;
        }

        public class ErrorHandling
        {

            [JsonProperty("Id")]
            public string Id;

            [JsonProperty("Description")]
            public string Description;
        }

        [JsonObject("Individuo")]
        public class MerchantDetails
        {

            [JsonProperty("Nombres")]
            public string Name;

            [JsonProperty("Apellidos")]
            public string SurName;

            [JsonProperty("Apellidos1")]
            public string SurName1;

            [JsonProperty("Apellidos2")]
            public string SurName2;

            [JsonProperty("CedulaNueva")]
            public string NewId;

            [JsonProperty("CedulaVieja")]
            public string OldId;

            [JsonProperty("Pasaporte")]
            public string Passport;

            [JsonProperty("Edad")]
            public string Age;

            [JsonProperty("FechaNacimiento")]
            public string DateofBirth;

            [JsonProperty("LugarNacimiento")]
            public string PlaceofBirth;

            [JsonProperty("EstadoCivil")]
            public string Maritalstatus;

            [JsonProperty("Ocupacion")]
            public string Ocupation;

            [JsonProperty("Categoria")]
            public string Category;

            [JsonProperty("Conyugue")]
            public string Spouse;

            [JsonProperty("CedulaConyugue")]
            public string SpouseId;

            [JsonProperty("Sexo")]
            public string Sex;

            [JsonProperty("Nacionalidad")]
            public string Nationality;

            [JsonProperty("Madre")]
            public string MotherName;

            [JsonProperty("Padre")]
            public string FatherName;
        }

        public class Moneda
        {

            [JsonProperty("@Id")]
            public string Id;

            [JsonProperty("Cuentas")]
            public string Cuentas;

            [JsonProperty("Cerradas")]
            public string Cerradas;

            [JsonProperty("Abiertas")]
            public string Abiertas;

            [JsonProperty("Normal")]
            public string Normal;

            [JsonProperty("EnAtraso")]
            public object EnAtraso;

            [JsonProperty("AcuerdoPago")]
            public object AcuerdoPago;

            [JsonProperty("EnLegal")]
            public object EnLegal;

            [JsonProperty("Castigado")]
            public object Castigado;
        }

        [JsonObject("Totales")]
        public class TotalAmount
        {

            [JsonProperty("Cuentas")]
            public string Accounts;

            [JsonProperty("Cerradas")]
            public string Closed;

            [JsonProperty("Abiertas")]
            public string Open;

            [JsonProperty("Normal")]
            public string Normal;

            [JsonProperty("EnAtraso")]
            public string OnDelay;

            [JsonProperty("AcuerdoPago")]
            public string PaymentAgreement;

            [JsonProperty("EnLegal")]
            public string InLegal;

            [JsonProperty("Castigado")]
            public string Punished;
        }

        [JsonObject("CantidadTotalCuentas")]
        public class CantidadTotalCuentas
        {

            [JsonProperty("Moneda")]
            public Moneda Moneda;

            [JsonProperty("Totales")]
            public TotalAmount Totales;
        }
        [JsonObject("")]
        public class MasReciente
        {

            [JsonProperty("Fecha")]
            public string Fecha;

            [JsonProperty("Anos")]
            public string Anos;

            [JsonProperty("Monto")]
            public string Monto;
        }
        [JsonObject("")]
        public class MasAntiguo
        {

            [JsonProperty("Fecha")]
            public string Fecha;

            [JsonProperty("Anos")]
            public string Anos;

            [JsonProperty("Monto")]
            public string Monto;
        }
        [JsonObject("")]
        public class MenorAprobado
        {

            [JsonProperty("Fecha")]
            public object Fecha;

            [JsonProperty("Anos")]
            public object Anos;

            [JsonProperty("Monto")]
            public object Monto;
        }
        [JsonObject("")]
        public class MayorAdeudado
        {

            [JsonProperty("Fecha")]
            public string Fecha;

            [JsonProperty("Anos")]
            public string Anos;

            [JsonProperty("Monto")]
            public string Monto;
        }
        [JsonObject("")]

        public class Moneda2
        {

            [JsonProperty("@Id")]
            public string Id;

            [JsonProperty("MasReciente")]
            public MasReciente MasReciente;

            [JsonProperty("MasAntiguo")]
            public MasAntiguo MasAntiguo;

            [JsonProperty("MenorAprobado")]
            public MenorAprobado MenorAprobado;

            [JsonProperty("MayorAdeudado")]
            public MayorAdeudado MayorAdeudado;
        }
        [JsonObject("")]
        public class AnalisisCreditos
        {

            [JsonProperty("Moneda")]
            public Moneda2 Moneda;
        }
        [JsonObject("")]
        public class MasReciente2
        {

            [JsonProperty("Fecha")]
            public string Fecha;

            [JsonProperty("Anos")]
            public string Anos;

            [JsonProperty("DiasAtraso")]
            public string DiasAtraso;

            [JsonProperty("Monto")]
            public string Monto;
        }
        [JsonObject("")]
        public class MasAntiguo2
        {

            [JsonProperty("Fecha")]
            public string Fecha;

            [JsonProperty("Anos")]
            public string Anos;

            [JsonProperty("DiasAtraso")]
            public string DiasAtraso;

            [JsonProperty("Monto")]
            public string Monto;
        }
        [JsonObject("")]
        public class MayorMontoAtraso
        {

            [JsonProperty("Fecha")]
            public string Fecha;

            [JsonProperty("Anos")]
            public string Anos;

            [JsonProperty("DiasAtraso")]
            public string DiasAtraso;

            [JsonProperty("Monto")]
            public string Monto;
        }
        [JsonObject("")]
        public class Moneda3
        {

            [JsonProperty("@Id")]
            public string Id;

            [JsonProperty("MasReciente")]
            public MasReciente2 MasReciente;

            [JsonProperty("MasAntiguo")]
            public MasAntiguo2 MasAntiguo;

            [JsonProperty("MayorMontoAtraso")]
            public MayorMontoAtraso MayorMontoAtraso;
        }
        [JsonObject("")]
        public class AnalisisAtrasos
        {

            [JsonProperty("Moneda")]
            public Moneda3 Moneda;
        }
        [JsonObject("")]
        public class Moneda4
        {

            [JsonProperty("@Id")]
            public string Id;

            [JsonProperty("Fecha")]
            public string Fecha;

            [JsonProperty("Anos")]
            public string Anos;

            [JsonProperty("DiasAtraso")]
            public string DiasAtraso;

            [JsonProperty("CreditoAprobado")]
            public string CreditoAprobado;

            [JsonProperty("MontoAtraso")]
            public string MontoAtraso;

            [JsonProperty("Producto")]
            public string Producto;
        }
        [JsonObject("")]
        public class PeorAtraso
        {

            [JsonProperty("Moneda")]
            public Moneda4 Moneda;
        }
        [JsonObject("")]
        public class CreditAnalysis1
        {

            [JsonProperty("CantidadTotalCuentas")]
            public CantidadTotalCuentas CantidadTotalCuentas;

            [JsonProperty("AnalisisCreditos")]
            public AnalisisCreditos AnalisisCreditos;

            [JsonProperty("AnalisisAtrasos")]
            public AnalisisAtrasos AnalisisAtrasos;

            [JsonProperty("PeorAtraso")]
            public PeorAtraso PeorAtraso;
        }
        [JsonObject("")]
        public class Producto
        {

            [JsonProperty("@Id")]
            public string Id;

            [JsonProperty("@Cod")]
            public string Cod;

            [JsonProperty("Cuenta")]
            public object Cuenta;
        }
        [JsonObject("")]
        public class BreakdownCredits
        {

            [JsonProperty("Producto")]
            public Producto[] Producto;
        }
        [JsonObject("Persona")]
        public class Persona
        {

            [JsonProperty("Nombres")]
            public string Name;

            [JsonProperty("Apellidos")]
            public string Apellidos;

            [JsonProperty("CedulaNueva")]
            public string CedulaNueva;

            [JsonProperty("CedulaVieja")]
            public object CedulaVieja;

            [JsonProperty("Edad")]
            public string Edad;
        }
        [JsonObject("PersonasRelacionadas")]
        public class PersonasRelacionadas
        {

            [JsonProperty("Persona")]
            public Persona Persona;
        }
        [JsonObject("Dir")]
        public class Dir
        {

            [JsonProperty("@Fecha")]
            public string Fecha;

            [JsonProperty("#text")]
            public string Text;
        }
        [JsonObject("Direcciones")]
        public class Direcciones
        {

            [JsonProperty("Dir")]
            public Dir[] Dir;
        }
        [JsonObject("Tel")]
        public class Telephone
        {

            [JsonProperty("@Tipo")]
            public string Tipo;

            [JsonProperty("@Suscriptor")]
            public string Suscriptor;

            [JsonProperty("@Direccion")]
            public string Direccion;

            [JsonProperty("#text")]
            public string Text;
        }
        [JsonObject("Telefonos")]
        public class TelePhones
        {

            [JsonProperty("Tel")]
            public Telephone[] Tel;
        }

        public class DCR
        {

            [JsonProperty("Seguridad")]
            public MemberDetails Seguridad;

            [JsonProperty("ErrorHandling")]
            public ErrorHandling ErrorHandling;

            [JsonProperty("Foto")]
            public string Foto;

            [JsonProperty("Individuo")]
            public MerchantDetails Individuo;

            [JsonProperty("AnalisisCrediticio")]
            public CreditAnalysis AnalisisCrediticio;

            [JsonProperty("DesgloseCreditos")]
            public BreakdownCredits DesgloseCreditos;

            /* [JsonProperty("PersonasRelacionadas")]
             public PersonasRelacionadas PersonasRelacionadas;

             [JsonProperty("Direcciones")]
             public Direcciones Direcciones;

             [JsonProperty("Telefonos")]
             public TelePhones Telefonos;*/
        }

      
    }
