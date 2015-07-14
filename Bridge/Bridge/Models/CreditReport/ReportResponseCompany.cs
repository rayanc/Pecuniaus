using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Bridge.Models.CreditReport
{
    
    public class ReportResponseCompany
    {

        [JsonProperty("c")]
        public C C { get; set; }
    }
    public class Seguridad
    {

        [JsonProperty("@Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("IdUsuario")]
        public string IdUsuario { get; set; }

        [JsonProperty("NombreUsuario")]
        public string NombreUsuario { get; set; }

        [JsonProperty("Afiliado")]
        public string Afiliado { get; set; }

        [JsonProperty("Oficina")]
        public string Oficina { get; set; }

        [JsonProperty("Departamento")]
        public string Departamento { get; set; }

        [JsonProperty("FechaHora")]
        public string FechaHora { get; set; }

        [JsonProperty("DireccionIp")]
        public string DireccionIp { get; set; }

        [JsonProperty("UsuarioSistema")]
        public object UsuarioSistema { get; set; }

        [JsonProperty("ConsultaId")]
        public string ConsultaId { get; set; }

        [JsonProperty("InformacionConsultada")]
        public string InformacionConsultada { get; set; }
    }

    public class ErrorHandling
    {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
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

    public class Moneda
    {

        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("Cuentas")]
        public string Cuentas { get; set; }

        [JsonProperty("Cerradas")]
        public string Cerradas { get; set; }

        [JsonProperty("Abiertas")]
        public string Abiertas { get; set; }

        [JsonProperty("Normal")]
        public string Normal { get; set; }

        [JsonProperty("EnAtraso")]
        public object EnAtraso { get; set; }

        [JsonProperty("AcuerdoPago")]
        public object AcuerdoPago { get; set; }

        [JsonProperty("EnLegal")]
        public object EnLegal { get; set; }

        [JsonProperty("Castigado")]
        public object Castigado { get; set; }
    }

    public class Totales
    {

        [JsonProperty("Cuentas")]
        public string Cuentas { get; set; }

        [JsonProperty("Cerradas")]
        public string Cerradas { get; set; }

        [JsonProperty("Abiertas")]
        public string Abiertas { get; set; }

        [JsonProperty("Normal")]
        public string Normal { get; set; }

        [JsonProperty("EnAtraso")]
        public object EnAtraso { get; set; }

        [JsonProperty("AcuerdoPago")]
        public object AcuerdoPago { get; set; }

        [JsonProperty("EnLegal")]
        public object EnLegal { get; set; }

        [JsonProperty("Castigado")]
        public object Castigado { get; set; }
    }

    public class CantidadTotalCuentas
    {

        [JsonProperty("Moneda")]
        public Moneda[] Moneda { get; set; }

        [JsonProperty("Totales")]
        public Totales Totales { get; set; }
    }

    public class MasReciente
    {

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("Anos")]
        public string Anos { get; set; }

        [JsonProperty("Monto")]
        public string Monto { get; set; }
    }

    public class MasAntiguo
    {

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("Anos")]
        public string Anos { get; set; }

        [JsonProperty("Monto")]
        public string Monto { get; set; }
    }

    public class MenorAprobado
    {

        [JsonProperty("Fecha")]
        public object Fecha { get; set; }

        [JsonProperty("Anos")]
        public object Anos { get; set; }

        [JsonProperty("Monto")]
        public object Monto { get; set; }
    }

    public class MayorAdeudado
    {

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("Anos")]
        public string Anos { get; set; }

        [JsonProperty("Monto")]
        public string Monto { get; set; }
    }

    public class Moneda2
    {

        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("MasReciente")]
        public MasReciente MasReciente { get; set; }

        [JsonProperty("MasAntiguo")]
        public MasAntiguo MasAntiguo { get; set; }

        [JsonProperty("MenorAprobado")]
        public MenorAprobado MenorAprobado { get; set; }

        [JsonProperty("MayorAdeudado")]
        public MayorAdeudado MayorAdeudado { get; set; }
    }

    public class AnalisisCreditos
    {

        [JsonProperty("Moneda")]
        public Moneda2[] Moneda { get; set; }
    }

    public class MasReciente2
    {

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("Anos")]
        public string Anos { get; set; }

        [JsonProperty("DiasAtraso")]
        public string DiasAtraso { get; set; }

        [JsonProperty("Monto")]
        public string Monto { get; set; }
    }

    public class MasAntiguo2
    {

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("Anos")]
        public string Anos { get; set; }

        [JsonProperty("DiasAtraso")]
        public string DiasAtraso { get; set; }

        [JsonProperty("Monto")]
        public string Monto { get; set; }
    }

    public class MayorMontoAtraso
    {

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("Anos")]
        public string Anos { get; set; }

        [JsonProperty("DiasAtraso")]
        public string DiasAtraso { get; set; }

        [JsonProperty("Monto")]
        public string Monto { get; set; }
    }

    public class Moneda3
    {

        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("MasReciente")]
        public MasReciente2 MasReciente { get; set; }

        [JsonProperty("MasAntiguo")]
        public MasAntiguo2 MasAntiguo { get; set; }

        [JsonProperty("MayorMontoAtraso")]
        public MayorMontoAtraso MayorMontoAtraso { get; set; }
    }

    public class AnalisisAtrasos
    {

        [JsonProperty("Moneda")]
        public Moneda3[] Moneda { get; set; }
    }

    public class Moneda4
    {

        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("Anos")]
        public string Anos { get; set; }

        [JsonProperty("DiasAtraso")]
        public string DiasAtraso { get; set; }

        [JsonProperty("CreditoAprobado")]
        public string CreditoAprobado { get; set; }

        [JsonProperty("MontoAtraso")]
        public string MontoAtraso { get; set; }

        [JsonProperty("Producto")]
        public string Producto { get; set; }
    }

    public class PeorAtraso
    {

        [JsonProperty("Moneda")]
        public Moneda4[] Moneda { get; set; }
    }

    public class AnalisisCrediticio
    {

        [JsonProperty("CantidadTotalCuentas")]
        public CantidadTotalCuentas CantidadTotalCuentas { get; set; }

        [JsonProperty("AnalisisCreditos")]
        public AnalisisCreditos AnalisisCreditos { get; set; }

        [JsonProperty("AnalisisAtrasos")]
        public AnalisisAtrasos AnalisisAtrasos { get; set; }

        [JsonProperty("PeorAtraso")]
        public PeorAtraso PeorAtraso { get; set; }
    }

    public class Estatus
    {

        [JsonProperty("@Des_Estatus")]
        public string DesEstatus { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Moneda5
    {

        [JsonProperty("@Des_Moneda")]
        public string DesMoneda { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class M1
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class M2
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class M3
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class M4
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class M5
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class M6
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M7
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M8
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M9
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M10
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M11
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M12
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M13
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M14
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M15
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M16
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M17
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M18
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M19
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M20
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M21
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M22
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M23
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class M24
    {

        [JsonProperty("@F")]
        public string F { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@PDV")]
        public string PDV { get; set; }
    }

    public class HistorialPago
    {

        [JsonProperty("M1")]
        public M1 M1 { get; set; }

        [JsonProperty("M2")]
        public M2 M2 { get; set; }

        [JsonProperty("M3")]
        public M3 M3 { get; set; }

        [JsonProperty("M4")]
        public M4 M4 { get; set; }

        [JsonProperty("M5")]
        public M5 M5 { get; set; }

        [JsonProperty("M6")]
        public M6 M6 { get; set; }

        [JsonProperty("M7")]
        public M7 M7 { get; set; }

        [JsonProperty("M8")]
        public M8 M8 { get; set; }

        [JsonProperty("M9")]
        public M9 M9 { get; set; }

        [JsonProperty("M10")]
        public M10 M10 { get; set; }

        [JsonProperty("M11")]
        public M11 M11 { get; set; }

        [JsonProperty("M12")]
        public M12 M12 { get; set; }

        [JsonProperty("M13")]
        public M13 M13 { get; set; }

        [JsonProperty("M14")]
        public M14 M14 { get; set; }

        [JsonProperty("M15")]
        public M15 M15 { get; set; }

        [JsonProperty("M16")]
        public M16 M16 { get; set; }

        [JsonProperty("M17")]
        public M17 M17 { get; set; }

        [JsonProperty("M18")]
        public M18 M18 { get; set; }

        [JsonProperty("M19")]
        public M19 M19 { get; set; }

        [JsonProperty("M20")]
        public M20 M20 { get; set; }

        [JsonProperty("M21")]
        public M21 M21 { get; set; }

        [JsonProperty("M22")]
        public M22 M22 { get; set; }

        [JsonProperty("M23")]
        public M23 M23 { get; set; }

        [JsonProperty("M24")]
        public M24 M24 { get; set; }
    }

    public class Cuenta
    {

        [JsonProperty("Estatus")]
        public Estatus Estatus { get; set; }

        [JsonProperty("Moneda")]
        public Moneda5 Moneda { get; set; }

        [JsonProperty("Afiliado")]
        public string Afiliado { get; set; }

        [JsonProperty("FechaApertura")]
        public string FechaApertura { get; set; }

        [JsonProperty("FechaReportado")]
        public string FechaReportado { get; set; }

        [JsonProperty("MesConsolidado")]
        public string MesConsolidado { get; set; }

        [JsonProperty("FechaUltimaTransaccion")]
        public string FechaUltimaTransaccion { get; set; }

        [JsonProperty("CreditoAprobado")]
        public string CreditoAprobado { get; set; }

        [JsonProperty("TotalAdeudado")]
        public string TotalAdeudado { get; set; }

        [JsonProperty("Cuota")]
        public string Cuota { get; set; }

        [JsonProperty("TotalAtraso")]
        public object TotalAtraso { get; set; }

        [JsonProperty("VecesAtraso30")]
        public string VecesAtraso30 { get; set; }

        [JsonProperty("VecesAtraso60")]
        public string VecesAtraso60 { get; set; }

        [JsonProperty("VecesAtraso90")]
        public string VecesAtraso90 { get; set; }

        [JsonProperty("VecesAtraso120")]
        public string VecesAtraso120 { get; set; }

        [JsonProperty("EstatusUltimo")]
        public string EstatusUltimo { get; set; }

        [JsonProperty("FDR")]
        public string FDR { get; set; }

        [JsonProperty("HistorialPago")]
        public HistorialPago HistorialPago { get; set; }
    }

    public class Producto
    {

        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("@Cod")]
        public string Cod { get; set; }

        [JsonProperty("Cuenta")]
        public Cuenta[] Cuenta { get; set; }
    }

    public class DesgloseCreditos
    {

        [JsonProperty("Producto")]
        public Producto[] Producto { get; set; }
    }

    public class Persona
    {

        [JsonProperty("Nombres")]
        public string Nombres { get; set; }

        [JsonProperty("Apellidos")]
        public string Apellidos { get; set; }

        [JsonProperty("CedulaNueva")]
        public string CedulaNueva { get; set; }

        [JsonProperty("CedulaVieja")]
        public object CedulaVieja { get; set; }

        [JsonProperty("Edad")]
        public string Edad { get; set; }
    }

    public class PersonasRelacionadas
    {

        [JsonProperty("Persona")]
        public Persona Persona { get; set; }
    }

    public class Dir
    {

        [JsonProperty("@Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Direcciones
    {

        [JsonProperty("Dir")]
        public Dir[] Dir { get; set; }
    }

    public class Tel
    {

        [JsonProperty("@Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("@Suscriptor")]
        public string Suscriptor { get; set; }

        [JsonProperty("@Direccion")]
        public string Direccion { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Telefonos
    {

        [JsonProperty("Tel")]
        public Tel[] Tel { get; set; }
    }

    public class DCR
    {

        [JsonProperty("Seguridad")]
        public Seguridad Seguridad { get; set; }

        [JsonProperty("ErrorHandling")]
        public ErrorHandling ErrorHandling { get; set; }

        [JsonProperty("Foto")]
        public object Foto { get; set; }

        [JsonProperty("Empresa")]
        public Empresa Empresa { get; set; }

        [JsonProperty("AnalisisCrediticio")]
        public AnalisisCrediticio AnalisisCrediticio { get; set; }

        [JsonProperty("DesgloseCreditos")]
        public DesgloseCreditos DesgloseCreditos { get; set; }

        [JsonProperty("PersonasRelacionadas")]
        public PersonasRelacionadas PersonasRelacionadas { get; set; }

        [JsonProperty("Direcciones")]
        public Direcciones Direcciones { get; set; }

        [JsonProperty("Telefonos")]
        public Telefonos Telefonos { get; set; }
    }

    public class C
    {

        [JsonProperty("DCR")]
        public DCR DCR { get; set; }
    }

}




