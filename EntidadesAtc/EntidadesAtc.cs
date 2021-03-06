﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using dsCore.Comun;
using MimeKit;

namespace EntidadesAtc
{

    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
    }
    [DataContract]
    public class ImagenArticulo
    {
        [DataMember]
        public int IDImagenArticulo { get; set; }
        public bool active { get; set; }
    }
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class ElementoMenu
    {
        public int IDElementoMenu { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ElementoMenu> Elementos { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
    }
    [DataContract]
    public class Categoria
    {
        [DataMember]
        public int IDCategoria { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public List<BuscaArticulo> Articulos { get; set; }
        [DataMember]
        public int IdArticuloCategoria { get; set; }
    }
    public class FormModel
    {
        public string campo1 { get; set; }
        public string campo2 { get; set; }
        public string campo3 { get; set; }
        public IFormFile campo4 { get; set; }
    }
    public class PedidoWeb
    {
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdDelegacion { get; set; }
        public int IdDomiEnt { get; set; }
        public decimal ImporteBruto { get; set; }
        public decimal PoDtoCom { get; set; }
        public decimal PoDtoPP { get; set; }
        public decimal ImportePortes { get; set; }
        public int? IdTipoIvaPortes { get; set; }
        public decimal TotalBaseImponible { get; set; }
        public decimal TotalCuotaIva { get; set; }
        public decimal TotalCuotaRE { get; set; }
        public decimal ImporteLiquido { get; set; }
        public decimal PoRecFinanciero { get; set; }
        public decimal ImporteRecFin { get; set; }
        public decimal ImporteDocumento { get; set; }
        public string Observaciones { get; set; }
        public int IdRegimenIva { get; set; }
        public Boolean AplicarRe { get; set; }
        public Boolean AplicarIva { get; set; }
        public List<LineaPedidoVentas> LineasPedido { get; set; }
        public List<LineaIva> LineasIva { get; set; }
    }
    public class LineaPedidoVentas
    {
        public int IdArticulo { get; set; }
        public int IdLinDocumento { get; set; }
        public int? IdUnidadValoracion { get; set; }
        public int? IdUnidadVenta { get; set; }
        public int IdPoIva { get; set; }
        public decimal PoIva { get; set; }
        public decimal PoRE { get; set; }
        public int IdUnidadManipulacion { get; set; }
        public int CantidadPalets { get; set; }
        public int CantidadXPalet { get; set; }
        public int CantidadUM { get; set; }
        public int CantidadXUM { get; set; }
        public int CantidadUV { get; set; }
        public decimal Precio { get; set; }
        public decimal PoDto1 { get; set; }
        public decimal PoDto2 { get; set; }
        public decimal ImporteBruto { get; set; }
        public decimal ImporteDtosLinea { get; set; }
        public decimal ImporteBonificaciones { get; set; }
        public decimal BonifPrecio { get; set; }
        public decimal ImporteNeto { get; set; }
        public int ImporteDtoComercial { get; set; }
        public decimal ImporteDtoPP { get; set; }
        public int? IdCargo { get; set; }
        public decimal ValorOtrosCargos { get; set; }
        public decimal ImporteOtrosCargos { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal ImporteCuotaIva { get; set; }
        public decimal ImporteCuotaRE { get; set; }
        public decimal ImporteLiquido { get; set; }
        public int? IdPromocion { get; set; }
        public Boolean Bonificada { get; set; }
        public string TipoTrans { get; set; }
        public int IdTipoIva { get; set; }
    }
    public class LineaIva
    {
        public int IdLineaIva { get; set; }
        public int IdDocumento { get; set; }
        public int IdTipoIva { get; set; }
        public decimal PoIva { get; set; }
        public decimal PoRE { get; set; }
        public decimal BaseBruta { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal CuotaIva { get; set; }
        public decimal CuotaRE { get; set; }
        public Boolean Manual { get; set; }
        public string TipoTrans { get; set; }
        public List<LineaPedidoVentas> Articulos { get; set; }
        public int IdPorcentajeIva { get; set; }
        public int idTipoIvaLeeEste { get; set; }
    }
    [DataContract]
    public class CampoBusqueda
    {
        [DataMember]
        public string cadena { get; set; }
        [DataMember]
        public int? numPag { get; set; }
        [DataMember]
        public string LastValor { get; set; }
        [DataMember]
        public int LastIndice { get; set; }
        [DataMember]
        public string AccionPagina { get; set; }
        [DataMember]
        public string FirstValor { get; set; }
        [DataMember]
        public int FirstIndice { get; set; }
    }
    public class BusquedaArticulos
    {
        public int idCliente { get; set; }
        public int? idDelegacion { get; set; }
        public int? idSeccion { get; set; }
        public int? idFamilia { get; set; }
        public int? idGenerico { get; set; }
        public int pagina { get; set; }
        public int tamanoPagina { get; set; }
        public string orderBy { get; set; }
        public string tipoOrden { get; set; }
        public string valorFuncion { get; set; }
        public int forzarImagenes { get; set; }
    }
    public class ListadoArticulos
    {
        public List<Articulo> Articulos { get; set; }
        public int NumReg { get; set; }
        public int NumPags { get; set; }
    }
    public class Articulo
    {
        public int idArticulo { get; set; }
        public string GTINUC { get; set; } //TAMPOCO SE SI ES UNA Cadena
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CodUdValor { get; set; }
        public string DescUdValor { get; set; }
        public string Imagen { get; set; }
        public Guid? RowGuid { get; set; }
        public int idSeccion { get; set; }
        public int? idFamilia { get; set; }
        public int? idGenerico { get; set; }
        public int idTipoIva { get; set; }
        public int? idArtProm { get; set; }
        public List<UnidadManipulacion> UnidadesManipulacion { get; set; }
    }
    public class UnidadManipulacion
    {
        public int idUnidadManipulacion { get; set; }
        public int idArticulo { get; set; }

        public string GTIN { get; set; } //AUN NO SE SI ES CADENA O NO
        public string DescripcionUM { get; set; }
        public string CodUdVenta { get; set; }
        public string DescUdVenta { get; set; }
        public string ModoContenido { get; set; }
        public decimal UnidadesContenido { get; set; }
        public decimal PrecioTarifa { get; set; }
        public int idAcumuladoUdMan { get; set; }
        public decimal StockFinalUV { get; set; }
        public string NombreAlmacen { get; set; }
        public List<AcumuladoStock> AcumuladosStock { get; set; }
        public PrecioTarifaUM PrecioTarifaUM { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
    }
    public class ListadoFacturas
    {
        public int Registros { get; set; }
        public List<Factura> Facturas { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
    public class FacturacionMensual
    {
        public int Anio { get; set; }
        public string Mes { get; set; }
        public decimal? TotalMes { get; set; }
        public string arg { get; set; }
        public decimal val { get; set; }
    }
    public class Factura
    {
        public int IdFactura { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string Documento { get; set; }
        public decimal TotalBaseImponible { get; set; }
        public decimal TotalCuotaIva { get; set; }
        public decimal TotalCuotaRE { get; set; }
        public decimal ImporteLiquido { get; set; }
        public int IDCabPedidoVentas { get; set; }
    }
    public class DebitosPendientes
    {
        public int Registros { get; set; }
        public List<DebitoPendiente> Contenido { get; set; }
    }
    public class EfectosCurso
    {
        public int Registros { get; set; }
        public List<EfectoCurso> Contenido { get; set; }
    }
    public class ExtractosMovimiento
    {
        public int Registros { get; set; }
        public List<ExtractoMovimiento> Contenido { get; set; }
    }
    public class ExtractoMovimiento
    {
        public int Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ImporteCargo { get; set; }
        public decimal ImporteAbono { get; set; }
        public string Comentario { get; set; }
        public string IdOrigen { get; set; }
    }
    public class EfectoCurso
    {
        public decimal Importe { get; set; }
        public int IdEfectoCobro { get; set; }
        public string DescripcionTipoEfecto { get; set; }
        public string NumeroDocumento { get; set; }
        public string DocumentoOrigen { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime FechaVto { get; set; }
        public DateTime? FechaCobro { get; set; }
        public string NombreCartera { get; set; }
        public string ColorEstado { get; set; }
    }
    public class Pedido
    {
        public int IdCabPedidoVentas { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string Documento { get; set; }
        public decimal TotalBaseImponible { get; set; }
        public decimal TotalCuotaIva { get; set; }
        public decimal ImporteLiquido { get; set; }
        public decimal TotalCuotaRE { get; set; }
        public string Estado { get; set; }
        public string ColorEstado { get; set; }
    }
    public class DebitoPendiente
    {
        public int IdDebito { get; set; }
        public string NumeroDocumentoOrigen { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVtoPrevista { get; set; }
        public DateTime FechaDebitoVencido { get; set; }
        public decimal ImporteRiesgo { get; set; }
        public decimal ImportePendiente { get; set; }
        public decimal ImporteCobrado { get; set; }
        public string Estado { get; set; }
        public string Origen { get; set; }
        public string ColorEstado { get; set; }
    }
    public class SituacionCliente
    {
        public int idCliente { get; set; }
        public decimal ImporteDebitos { get; set; }
        public decimal ImportePendiente { get; set; }
        public decimal ImporteRiesgo { get; set; }
        public int MaxDias { get; set; }
        public int Documentos { get; set; }
        public int HayImpagos { get; set; }
        public int DebitosVencidos { get; set; }
        public int Media { get; set; }
    }
    public class AcumuladoStock
    {
        public int idUnidadManipulacion { get; set; }
        public int idArticulo { get; set; }

        public string GTIN { get; set; } //AUN NO SE SI ES CADENA O NO
        public string DescripcionUM { get; set; }
        public string CodUdVenta { get; set; }
        public string DescUdVenta { get; set; }
        public string ModoContenido { get; set; }
        public decimal UnidadesContenido { get; set; }
        public decimal PrecioTarifa { get; set; }
        public int idAcumuladoUdMan { get; set; }
        public decimal StockFinalUV { get; set; }
        public string NombreAlmacen { get; set; }
    }
    [DataContract]
    public class BusquedaRapida
    {
        [DataMember]
        public List<ArticuloBasico> Articulos { get; set; }
        [DataMember]
        public List<Marca> Marcas { get; set; }
        [DataMember]
        public int NumReg { get; set; }
    }
    [DataContract]
    public class ArticuloBasico
    {
        [DataMember]
        public int IDArticulo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public int CantidadInt { get; set; }
    }

    [DataContract]
    [Serializable]
    public class BuscaArticulo
    {
        [DataMember]
        public int IdArticulo { get; set; }

        [DataMember]
        public int? IdUnidadManipulacion { get; set; }

        [DataMember]
        public int? IdUnidadValoracion { get; set; }

        [DataMember]
        public int? IdMedidaUM { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string DescripcionUM { get; set; }

        [DataMember]
        public string GTINUC { get; set; }

        [DataMember]
        public string GTINUM { get; set; }

        [DataMember]
        public string ModoGestion { get; set; }

        [DataMember]
        public bool ContenidoVariable { get; set; }

        [DataMember]
        public decimal UnidadesContenido { get; set; }

        [DataMember]
        public decimal StockUM { get; set; }

        [DataMember]
        public decimal StockUV { get; set; }

        [DataMember]
        public string strStock { get; set; }

        [DataMember]
        public int? IdTipoPartida { get; set; }

        [DataMember]
        public int? IdTipoIva { get; set; }
        [DataMember]
        public int? IdFamilia { get; set; }
        [DataMember]
        public int? IdSeccion { get; set; }
        [DataMember]
        public int? IdTipoVidrio { get; set; }
        [DataMember]
        public int? IdGenerico { get; set; }
        [DataMember]
        public string DescripcionFamilia { get; set; }
        [DataMember]
        public string DescripcionSeccion { get; set; }
        [DataMember]
        public string DescripcionCorta { get; set; }
        [DataMember]
        public string DescripcionTipoVidrio { get; set; }
        [DataMember]
        public string DescripcionDetallada { get; set; }
        [DataMember]
        public string DescripcionWeb1 { get; set; }
        [DataMember]
        public string DescripcionWeb2 { get; set; }
        [DataMember]
        public int AnoInicial { get; set; }
        [DataMember]
        public int AnoFinal { get; set; }
        [DataMember]
        public List<Categoria> Accesorios { get; set; }
        [DataMember]
        public int? IdCategoria { get; set; }
        [DataMember]
        public int IdArticuloCategoria { get; set; }
        [DataMember]
        public string DescripcionCategoria { get; set; }
        [DataMember]
        public List<ArticuloCarroceria> Carrocerias { get; set; }
        [DataMember]
        public List<UnidadManipulacion> UnidadesManipulacion { get; set; }
        [DataMember]
        public Boolean active { get; set; }
        [DataMember]
        public ArticuloEurocode Eurocode { get; set; }
        [DataMember]
        public Modelo Modelo { get; set; }
        public List<ImagenArticulo> Imagenes { get; set; }
        //[DataMember]
        //public List<ModificarAccesorio> accesorioseliminar { get; set; }
    }
    public class CaracteristicasArticulo
    {
        public string Valor;
        public string Caracter;
        public CaracteristicasArticulo(string val, string caract)
        {
            Valor = val;
            Caracter = caract;

        }
    }
    public class ArticuloEurocode
    {
        public string Color { get; set; }
        public string Carroceria { get; set; }
        public List<CaracteristicasArticulo> Modificaciones { get; set; }
        public List<CaracteristicasArticulo> Caracteristicas { get; set; }
        public string CaracterCarroceria { get; set; }
        public int PosicionCaracterCarroceria { get; set; }
        public string CaracterColor { get; set; }
        public int[] PosicionCaracterColor { get; set; }
        public string BandaSuperior { get; set; }
        public string CaracterBandaSuperior { get; set; }
        public string PosicionVidrio { get; set; }
        public string CaracterPosicionVidrio { get; set; }
        public string TipoAccesorio { get; set; }
        public string CaracterTipoAccesorio { get; set; }
        public string TipoVidrio { get; set; }
        public string CaracterTipoVidrio { get; set; }
    }
    public class CaracteristicaEurocode
    {
        public string Valor { get; set; }
        public string Propiedad { get; set; }
        public string Tipo { get; set; }
        public CaracteristicaEurocode(string val)
        {
            Valor = val;
        }
        public CaracteristicaEurocode(string val, string prop, string tipo)
        {
            Valor = val;
            Propiedad = prop;
            Tipo = tipo;
        }
    };
    public class QueryBusqueda
    {

    }
    public class PrecioTarifaUM
    {
        public string DescripcionUM { get; set; }
        public int idUnidadManipulacion { get; set; }
        public decimal PrecioTarifa { get; set; }
    }
    public class UsuarioWeb
    {
        public int IdUsuarioWeb { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public Cliente Cliente { get; set; }
        public List<Domicilio> Domicilios { get; set; }
        public List<Promocion> Promociones { get; set; }
        public string UltimaIP { get; set; }
        public InfoMenuWeb InfoMenuWeb { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public List<MensajeWeb> Mensajes { get; set; }
        public EmpresaWeb DatosEmpresa { get; set; }
    }
    public class RecuperacionPassword
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public string repeatpassword { get; set; }
        public string username { get; set; }
    }
    public class UsuarioError
    {
        public string nombre { get; set; }
        public string motivo { get; set; }
    }
    public class ResultadoInvitaciones
    {
        public List<string> Enviadas { get; set; }
        public List<UsuarioError> UsuariosError { get; set; }
    }
    public class ResultadoRegistro
    {
        public int? Resultado { get; set; }
        public string Cadena { get; set; }
        public PropiedadesSitio Propiedades { get; set; }
        public UsuarioDatosEmail Usuario { get; set; }
        public string CadenaMail { get; set; }
    }
    public class ResultadoAsignacion
    {
        public UsuarioDatosEmail Usuario { get; set; }
        public int Resultado { get; set; }
        public PropiedadesSitio Propiedades { get; set; }
        public string Cadena { get; set; }
    }
    public class ResultadoRecuperacionContrasena
    {
        public int Resultado { get; set; }
        public PropiedadesSitio Propiedades { get; set; }
        public string Cadena { get; set; }
    }
    public class SolicitudRegistro
    {
        public string nombre { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string informacion { get; set; }
        public string cif { get; set; }
        public string empresa { get; set; }
        public string empresawildcards { get; set; }

    }
    public class SolicitudRecuperacion
    {
        public string email { get; set; }
    }
    [DataContract]
    public class EmpresaWeb
    {
        [DataMember]
        public int IdDatosWeb { get; set; }
        [DataMember]
        public int IdEmpresa { get; set; }
        [DataMember]
        public Guid GuidImg { get; set; }
        [DataMember]
        public Guid GuidIcono { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string CodPostal { get; set; }
        [DataMember]
        public string Localidad { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Web { get; set; }
        [DataMember]
        public string PaginaFacebook { get; set; }
        [DataMember]
        public string PaginaTwitter { get; set; }
        [DataMember]
        public string PaginaGooglePlus { get; set; }
        [DataMember]
        public string PaginaPinterest { get; set; }
        [DataMember]
        public string PaginaLinkedIn { get; set; }
        [DataMember]
        public string AcercaDe { get; set; }
        [DataMember]
        public int IdClienteVentaDirecta { get; set; }
        [DataMember]
        public Boolean VisiblePedidos { get; set; }
        [DataMember]
        public Boolean VisibleFacturas { get; set; }
        [DataMember]
        public Boolean VisibleFinanzas { get; set; }
        [DataMember]
        public Boolean VisibleCatalogo { get; set; }
        [DataMember]
        public Boolean VisibleCuenta { get; set; }
        [DataMember]
        public Boolean VisibleIdiomas { get; set; }
        [DataMember]
        public Boolean VisibleMensajes { get; set; }
        [DataMember]
        public Boolean VisiblePlantillas { get; set; }
        [DataMember]
        public Boolean VisibleInvitado { get; set; }
        [DataMember]
        public Boolean VisibleVentaDirecta { get; set; }
        [DataMember]
        public string NombreCuenta { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string ServCorreoSal { get; set; }
        [DataMember]
        public int PuertoCorreoSal { get; set; }
        [DataMember]
        public string NombreSitio { get; set; }
        [DataMember]
        public string RutaLogo { get; set; }
        [DataMember]
        public string dirEmailContacto { get; set; }
        [DataMember]
        public Boolean VisibleCategorias { get; set; }
        [DataMember]
        public Boolean VisibleVehiculos { get; set; }
        [DataMember]
        public Boolean VisibleNovedades { get; set; }
        [DataMember]
        public Boolean VisibleExpress { get; set; }
        [DataMember]
        public Boolean VisibleUltimosPedidos { get; set; }
        [DataMember]
        public Boolean VisibleIP { get; set; }
        [DataMember]
        public Boolean VisibleUltimaConexion { get; set; }
        [DataMember]
        public Boolean VisibleEurocodeListado { get; set; }
        [DataMember]
        public Boolean VisibleEurocodeFicha { get; set; }
        [DataMember]
        public Boolean VisibleAlmacenesListado { get; set; }
        [DataMember]
        public Boolean VisibleAlmacenesFicha { get; set; }
        [DataMember]
        public string PaginaInstagram { get; set; }
        [DataMember]
        public string Copyright { get; set; }
        [DataMember]
        public Boolean VisiblePrecioListado { get; set; }
        [DataMember]
        public Boolean VisiblePrecios { get; set; }
        [DataMember]
        public Boolean VisibleDtos { get; set; }
        [DataMember]
        public Boolean VisibleTotalCompra { get; set; }
    }
    [DataContract]
    public class FormularioBajoPedido
    {
        [DataMember]
        public string descripcionArticulo { get; set; }
        [DataMember]
        public string eurocodeArticulo { get; set; }
        [DataMember]
        public string comentario { get; set; }
        [DataMember]
        public int idCliente { get; set; }
        [DataMember]
        public string nombreCliente { get; set; }
    }
    public class ResultadoValidacionGuidRecuperacion
    {
        public int Resultado { get; set; }
        public UsuarioDatosEmail Usuario { get; set; }
    }
    [DataContract]
    public class ImagenCabWeb
    {
        [DataMember]
        public int IdImagen { get; set; }
        [DataMember]
        public int IdEmpresa { get; set; }
        [DataMember]
        public Guid RowGuid { get; set; }
        [DataMember]
        public string ImagenSt { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Subtitulo { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public int tipoTransaccion { get; set; }
        public string url { get; set; }
        public string classcontent { get; set; }
    }
    [DataContract]
    public class FormularioCambioPassword
    {
        [DataMember]
        public string actual { get; set; }
        [DataMember]
        public string newn { get; set; }
        [DataMember]
        public string newnew { get; set; }
        [DataMember]
        public string idUsuarioWeb { get; set; }
    }
    public class UsuarioDatosEmail
    {
        public int IdUsuarioWeb { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid GuidValidacion { get; set; }
        public string NombreCompleto { get; set; }
        public string EmaildsWin { get; set; }
        public Guid? GuidRecuperacion { get; set; }
    }
    public class ProcesoPedido
    {
        public int usuario { get; set; }
        public int domicilio { get; set; }
    }
    public class Domicilio
    {
        public int IDDomicilioCliente { get; set; }
        public int IdCliente { get; set; }
        public int IdDomicilioRelacion { get; set; }
        public int IdRelacion { get; set; }
        public int IdTipoIva { get; set; }
        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string PisoPuerta { get; set; }
        public int IdLocalidad { get; set; }
        public string NombreMunicipio { get; set; }
        public int CodPostal { get; set; }
        public int IdProvincia { get; set; }
        public string NombreProvincia { get; set; }
        public int IdPais { get; set; }
        public string NombreDomicilio { get; set; }
        public string TipoDomicilio { get; set; }
        public int Venta { get; set; }
        public int Entrega { get; set; }
        public int Cobro { get; set; }
        public int IdTipoDomicilio { get; set; }
        public string NombrePais { get; set; }
        public string ApdoPostal { get; set; }
    }
    public class Promocion
    {
        public int IDPromocion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public Guid? RowGuid { get; set; }
    }
    public class Cliente
    {
        public int IDCliente { get; set; }
        public int IdDelegacion { get; set; }
        public string Clientee { get; set; }
        public string NombreComercial { get; set; }
        public int IdTarifaPrecios { get; set; }
        public int IdRegimenIva { get; set; }
        public string Cif { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int AplicarIva { get; set; }
        public int AplicarRE { get; set; }
        public decimal PoCompensacion { get; set; }
    }
    [DataContract]
    public class Carrito
    {
        [DataMember]
        public List<ArticuloCarrito> Articulos { get; set; }
        [DataMember]
        public int? IDUsuario { get; set; }
        [DataMember]
        public decimal Precio { get; set; }
        [DataMember]
        public List<TipoIva> TipoIva { get; set; }
        [DataMember]
        public decimal TotalBaseImponible { get; set; }
        [DataMember]
        public decimal TotalIva { get; set; }
        [DataMember]
        public decimal TotalPedido { get; set; }
        [DataMember]
        public string Mensaje { get; set; }
    }
    public class TipoIva
    {
        [DataMember]
        public int IdTipoIva { get; set; }
        [DataMember]
        public string DescripcionTipoIva { get; set; }
        [DataMember]
        public decimal PorcentajeIva { get; set; }
        [DataMember]
        public decimal PorcentajeRE { get; set; }
        [DataMember]
        public int IdPorcentajeIva { get; set; }
        [DataMember]
        public List<ArticuloCarrito> Articulos { get; set; }
        [DataMember]
        public decimal TotalArticulos { get; set; }
        [DataMember]
        public decimal PrecioConIva { get; set; }
        [DataMember]
        public decimal TotalIva { get; set; }
        public decimal ValorIva { get; set; }
        public decimal ValorRE { get; set; }
    }
    [DataContract]
    public class ArticuloCarrito
    {
        [DataMember]
        public int IDArticulo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Cantidad { get; set; }
        [DataMember]
        public decimal Precio { get; set; }
        [DataMember]
        public decimal PrecioUd { get; set; }
        [DataMember]
        public int IDUnidadManipulacion { get; set; }
        [DataMember]
        public decimal Dto1 { get; set; }
        [DataMember]
        public decimal Dto2 { get; set; }
        [DataMember]
        public decimal Bon1 { get; set; }
        [DataMember]
        public decimal Bon2 { get; set; }
        public int IdTipoIva { get; set; }
    }

    [DataContract]
    public class ArticuloCarroceria
    {
        [DataMember]
        public int IDModeloCarroceria { get; set; }
        [DataMember]
        public string DescripcionCarroceria { get; set; }
        [DataMember]
        public string Anos { get; set; }
        [DataMember]
        public string DescripcionArticuloModelo { get; set; }
        [DataMember]
        public int IDArticuloModelo { get; set; }
        [DataMember]
        public int IDFamilia { get; set; }
        [DataMember]
        public string DescripcionFamilia { get; set; }
        [DataMember]
        public string DescripcionSeccion { get; set; }
    }
    public class UnClickYNovedades
    {
        public List<BuscaArticulo> UnClick { get; set; }
        public List<BuscaArticulo> Novedades { get; set; }
    }
    [DataContract]
    public class TipoVidrio
    {
        [DataMember]
        public int IDTipoVidrio { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        public List<BuscaArticulo> Articulos { get; set; }
        public string url { get; set; }
    }
    [DataContract]
    public class Carroceria
    {
        [DataMember]
        public int IDCarroceria { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<Vidrio> Vidrios { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public int IDModeloCarroceria { get; set; }
        [DataMember]
        public int CantidadArticulos { get; set; }
    }
    [DataContract]
    public class Ano
    {
        [DataMember]
        public int Valor { get; set; }
        [DataMember]
        public int CantidadArticulos { get; set; }
    }
    [DataContract]
    public class ResultadoIM
    {
        [DataMember]
        public int Identidad { get; set; }
        [DataMember]
        public byte[] TS { get; set; }
        [DataMember]
        public string Resultado { get; set; }
    }
    [DataContract]
    public class CadenasBusqueda
    {
        [DataMember]
        public string Vista { get; set; }
        [DataMember]
        public string EntidadFuncion { get; set; }
        [DataMember]
        public string CampoClave { get; set; }
        [DataMember]
        public string Entidad { get; set; }
        [DataMember]
        public string CampoOrdenacion { get; set; }
    }
    [DataContract]
    public class BusquedaPaginada
    {
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string cadena { get; set; }
        [DataMember]
        public int? numPag { get; set; }
        [DataMember]
        public string LastValor { get; set; }
        [DataMember]
        public int LastIndice { get; set; }
        [DataMember]
        public string AccionPagina { get; set; }
        [DataMember]
        public string FirstValor { get; set; }
        [DataMember]
        public int FirstIndice { get; set; }
        [DataMember]
        public int? idSeccion { get; set; }
    }
    [DataContract]
    public class Vidrio
    {
        [DataMember]
        public int IDVidrio { get; set; }
        [DataMember]
        public int IDTipoVidrio { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public int PosVer { get; set; }
        [DataMember]
        public int PosHor { get; set; }
        [DataMember]
        public int SpanVer { get; set; }
        [DataMember]
        public int SpanHor { get; set; }
        [DataMember]
        public string DescripcionTipoVidrio { get; set; }
        [DataMember]
        public int Modificador { get; set; }
        [DataMember]
        public int CantidadArticulos { get; set; }
    }
    public class Parametros
    {
        public int? idModeloCarroceria;
        public int? ano;

        public int? idTipoVehiculo { get; set; }
        public int? idSeccion { get; set; }
        public int? idFamilia { get; set; }
        public string descripcionTipoVehiculo { get; set; }
        public string descripcionSeccion { get; set; }
        public string descripcionFamilia { get; set; }
        public int? idVidrio { get; set; }
        public string descripcionTipoVidrio { get; set; }
        public string descripcionCarroceria { get; set; }
        public int? idCarroceria { get; set; }
        public int? idTipoVidrio { get; set; }
        public string eurocode { get; set; }
        public int? idCategoria { get; set; }
        public int idCliente { get; set; }
    }
    [DataContract]
    public class TipoVehiculo
    {
        [DataMember]
        public int IDTipoVehiculo { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public List<string> InicialesMarcas { get; set; }
        //public int IdFamilia { get; set; }
    }
    /*public class MarcaModelo
    {
        public int IDMarcaModelo { get; set; }
        public int IDFamilia { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }*/
    [DataContract]
    public class Marca
    {
        [DataMember]
        public int IDSeccion { get; set; }
        [DataMember]
        public string DescripcionSeccion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string CodigoSeccion { get; set; }
        public string Inicial { get; set; }
        [DataMember]
        public List<Modelo> Modelos { get; set; }
    }
    [DataContract]
    public class Modelo
    {
        [DataMember]
        public int IDFamilia { get; set; }
        [DataMember]
        public string DescripcionFamilia { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string CodigoFamilia { get; set; }
        [DataMember]
        public int? IdSeccion { get; set; }
        [DataMember]
        public string descripcionSeccion { get; set; }
        [DataMember]
        public int idTipoVehiculo { get; set; }
        [DataMember]
        public string descripcionTipoVehiculo { get; set; }
        [DataMember]
        public List<Carroceria> Carrocerias { get; set; }
        [DataMember]
        public List<Carroceria> CarroceriasEliminadas { get; set; }
        [DataMember]
        public string Inicial { get; set; }
        [DataMember]
        public List<ImagenFamilia> Imagenes { get; set; }
        public string url { get; set; }
    }
    [DataContract]
    public class MensajeRespuesta
    {
        [DataMember]
        public bool Existente { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public string Archivo { get; set; }
        [DataMember]
        public long Tamano { get; set; }
    }
    [DataContract]
    public class MensajeError
    {

        [DataMember]
        public string Contenido { get; set; }
        public string Codigo { get; set; }
        public int Estado { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
    }
    public class ListadoAnos
    {
        public List<Ano> Anos { get; set; }
        public List<IntervaloAnos> Intervalos { get; set; }
    }
    public class InfoMenuWeb
    {
        public List<TipoVehiculo> Vehiculos { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
    public class IntervaloAnos
    {
        public List<Ano> Anos { get; set; }
        public string titulo { get; set; }
        public string titulojunto { get; set; }
    }
    [DataContract]
    public class BuscadorMarcas
    {
        [DataMember]
        public List<Marca> Marcas { get; set; }
        [DataMember]
        public List<Inicial> Iniciales { get; set; }
        public List<Modelo> Modelos { get; set; }
    }
    [DataContract]
    public class Inicial
    {
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public List<Marca> Marcas { get; set; }
        public List<Modelo> Modelos { get; set; }
    }
    [DataContract]
    public class Tamano
    {
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int Res { get; set; }
    }

    [DataContract]
    public class ImagenFamilia
    {
        [DataMember]
        public int IDImagenFamilia { get; set; }
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public int IDFamilia { get; set; }
        public string holi { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public bool active { get; set; }
    }

    public class ArticulosYCategorias
    {
        public List<BuscaArticulo> Articulos { get; set; }
        public List<Categoria> Accesorios { get; set; }
        public Parametros Parametros { get; set; }
        public List<TipoVidrio> TiposVidrio { get; set; }
    }


    public class dsMail
    {
        public static string EnviarEmail(MailEnvio mail_envio)
        {
            string res = "";
            //construye el mensaje
            MimeMessage message = BuildMessage(mail_envio);
            //realiza el envio
            res = SendBySMTP(message, mail_envio.Cuenta);
            return res;
            //return "holi";
        }
        private static MimeMessage BuildMessage(MailEnvio mail_envio)
        {
            Cuenta cuenta = mail_envio.Cuenta;
            Mail mail = mail_envio.Mail;

            //asigna remitente / destinatario
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(cuenta.NombreCuenta, cuenta.Usuario));

            string[] rcpts = mail.Destinatario.Split(';');
            if (rcpts.Length == 0)
                message.To.Add(new MailboxAddress(mail.Destinatario));
            else
            {
                foreach (string recipient in rcpts)
                    message.To.Add(new MailboxAddress(recipient));
            }

            if (!string.IsNullOrWhiteSpace(mail.CC))
            {
                string[] rcptsc = mail.CC.Split(';');
                if (rcptsc.Length == 0)
                    message.Cc.Add(new MailboxAddress(mail.CC));
                else
                {
                    foreach (string recipient in rcptsc)
                        message.Cc.Add(new MailboxAddress(recipient));
                }
                //message.Cc.Add(new MailboxAddress(mail.CC));
            }

            if (!string.IsNullOrWhiteSpace(mail.CCO))
            {
                string[] rcptsco = mail.CCO.Split(';');
                if (rcptsco.Length == 0)
                    message.Bcc.Add(new MailboxAddress(mail.CCO));
                else
                {
                    foreach (string recipient in rcptsco)
                        message.Bcc.Add(new MailboxAddress(recipient));
                }
                //message.Bcc.Add(new MailboxAddress(mail.CCO));
            }
            message.Subject = mail.Asunto;

            //construye el mensaje
            var builder = new BodyBuilder();
            builder.HtmlBody = mail.Texto;
            //if (html != "") builder.HtmlBody = html;
            //else builder.TextBody = mail.Texto;
            if (mail.ListaAdjuntos != null && mail.ListaAdjuntos.Count > 0)
            {
                foreach (AdjuntoMail adjunto in mail.ListaAdjuntos.OrderBy(o => o.Orden))
                    builder.Attachments.Add(adjunto.Archivo); // @"C:\Users\Joey\Documents\party.ics"); 
            }

            //string banner = System.IO.File.ReadAllText(@"C:\Temp\Firmagenerica.html");
            if (mail.Firmar && !string.IsNullOrWhiteSpace(cuenta.Firma))
                builder.HtmlBody += cuenta.Firma;
            //var image = builder.LinkedResources.Add(@"C:\Temp\Firmagenerica.html");
            //image.ContentId = MimeUtils.GenerateMessageId();

            message.Body = builder.ToMessageBody();

            return message;
        }
        private static string SendBySMTP(MimeMessage message, Cuenta cuenta)
        {
            string res = "error";

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(cuenta.ServCorreoSal, (int)cuenta.PuertoCorreoSal, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(cuenta.Usuario, cuenta.Clave);
                client.Send(message);
                client.Disconnect(true);

                res = "ok";
            }

            return res;
        }
        static string billede1 = string.Empty;
        static string billede2 = string.Empty;
    }
    public class PropiedadesSitio
    {
        public string NombreCuenta { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string ServCorreoSal { get; set; }
        public int PuertoCorreoSal { get; set; }
        public string CorreoRegistro { get; set; }
        public string URLSitio { get; set; }
        public string CorreoVerificacion { get; set; }
        public string CorreoInvitacion { get; set; }
        public string CorreoConfirmacion { get; set; }
        public string RutaValidacion { get; set; }
        public string NombreSitio { get; set; }
        public string RutaLogo { get; set; }
        public string CorreoRecuperacion { get; set; }
        public string RutaRecuperacion { get; set; }
        public string CorreoContacto { get; set; }
        public string dirEmailContacto { get; set; }
        public string dirEmailBajoPedido { get; set; }
        public string CorreoBajoPedido { get; set; }
    }
    public class FormularioContacto
    {
        public string nombre { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string message { get; set; }
    }

    [DataContract]
    public class MensajeWeb
    {
        [DataMember]
        public int IdMensaje { get; set; }
        [DataMember]
        public int IdCliente { get; set; }
        [DataMember]
        public int IdUsuarioWeb { get; set; }
        [DataMember]
        public int Prioridad { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Mensaje { get; set; }
        [DataMember]
        public DateTime? FechaEnvio { get; set; }
        [DataMember]
        public DateTime? FechaLeido { get; set; }
        [DataMember]
        public Boolean Leido { get; set; }
        [DataMember]
        public string TipoTransaccion { get; set; }
        [DataMember]
        public string Cliente { get; set; }
    }
    [DataContract]
    public class FormularioExcel
    {
        [DataMember]
        public IFormFile files { get; set; }
        [DataMember]
        public Boolean vaciar { get; set; }
        [DataMember]
        public string CoordenadasCodigo { get; set; }
        [DataMember]
        public string CoordenadasCantidad { get; set; }
    }


    public class CustomSection1
    {
        public string Hi { get; set; }
        public string Hello { get; set; }
    }

    public class CustomSection2
    {
        public string Bye { get; set; }
    }
    public class PropiedadYValor
    {
        public string euro { get; set; }
        public string desc { get; set; }
    }
    public class EntidadEurocodes
    {
        public List<PropiedadYValor> TiposVidrio { get; set; }
        public List<PropiedadYValor> Color { get; set; }
        public List<PropiedadYValor> Luneta { get; set; }
        public List<PropiedadYValor> CaracLuneta { get; set; }
        public List<PropiedadYValor> TopTins { get; set; }
        public List<PropiedadYValor> CaracParabrisas { get; set; }
        public List<PropiedadYValor> CarroceriasVehiculos { get; set; }
        public List<PropiedadYValor> PosicionesVidrios { get; set; }
        public List<PropiedadYValor> CaracLateralesTechos { get; set; }
        public List<PropiedadYValor> TipoAccesorioSK { get; set; }
        public List<PropiedadYValor> TipoAccesorioX { get; set; }
        public List<PropiedadYValor> CaracAccesorioSK { get; set; }
        public List<PropiedadYValor> CaracAccesorioX { get; set; }
    }

}
