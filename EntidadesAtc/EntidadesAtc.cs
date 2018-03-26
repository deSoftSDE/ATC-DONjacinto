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
        public int IdTipoIvaPortes { get; set; }
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
        public int PoRE { get; set; }
        public int IdUnidadManipulacion { get; set; }
        public int CantidadPalets { get; set; }
        public int CantidadXPalet { get; set; }
        public int CantidadUM { get; set; }
        public int CantidadXUM { get; set; }
        public int CantidadUV { get; set; }
        public decimal Precio { get; set; }
        public int PoDto1 { get; set; }
        public int PoDto2 { get; set; }
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
    }
    [DataContract]
    public class BusquedaRapida
    {
        [DataMember]
        public List<ArticuloBasico> Articulos { get; set; }
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
        public List<ArticuloCarroceria> Carrocerias { get; set; }

        //[DataMember]
        //public List<ModificarAccesorio> accesorioseliminar { get; set; }
    }
    public class QueryBusqueda
    {

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

    [DataContract]
    public class TipoVidrio
    {
        [DataMember]
        public int IDTipoVidrio { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
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
    }

    public class ArticulosYCategorias
    {
        public List<BuscaArticulo> Articulos { get; set; }
        public List<Categoria> Accesorios { get; set; }
        public Parametros Parametros { get; set; }
    }

}
