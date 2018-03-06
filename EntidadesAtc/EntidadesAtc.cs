using Microsoft.AspNetCore.Http;
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
    [DataContract]
    public class Noticia
    {
        [DataMember]
        public int IDNoticia { get; set; }
        [DataMember]
        public int IDAutor { get; set; }
        [DataMember]
        public string Titular { get; set; }
        [DataMember]
        public List<string> Contenido { get; set; }
        [DataMember]
        public string NombreCategoria { get; set; }
        [DataMember]
        public string NombreSeccion { get; set; }
        [DataMember]
        public string NombreSubseccion { get; set; }
        [DataMember]
        public string Subtitular { get; set; }
        [DataMember]
        public List<Imagen> Imagenes { get; set; }
        [DataMember]
        public List<Banner> Banners { get; set; }
        [DataMember]
        public DateTime FechaPubli { get; set; }
        [DataMember]
        public int Visitas { get; set; }
        [DataMember]
        public string Autor { get; set; }
        [DataMember]
        public List<Puntuacion> Puntuaciones { get; set; }
        [DataMember]
        public List<Etiqueta> Etiquetas { get; set; }
        [DataMember]
        public List<Noticia> Relacionadas { get; set; }
        [DataMember]
        public List<Comentario> Comentarios { get; set; }
        [DataMember]
        public PropiedadesPublicidad PropiedadesPublicidad { get; set; }
        [DataMember]
        public string ImagenAutor { get; set; }
    }
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
        public string NombreCategoria { get; set; }
        [DataMember]
        public int IDCategoria { get; set; }
        [DataMember]
        public string ClaseCSS { get; set; }
        [DataMember]
        public int TipoCategoria { get; set; }
        [DataMember]
        public List<Seccion> Secciones { get; set; }
        [DataMember]
        public List<Noticia> Noticias { get; set; }
        [DataMember]
        public List<Banner> Banners { get; set; }
    }
    [DataContract]
    public class Seccion
    {
        [DataMember]
        public string NombreSeccion { get; set; }
        [DataMember]
        public int IDSeccion { get; set; }
        [DataMember]
        public int IDCategoria { get; set; }
        [DataMember]
        public List<Subseccion> Subsecciones { get; set; }
    }
    [DataContract]
    public class Subseccion
    {
        [DataMember]
        public string NombreSubseccion { get; set; }
        [DataMember]
        public int IDSubseccion { get; set; }
        [DataMember]
        public int IDSeccion { get; set; }
    }
    [DataContract]
    public class Servicio
    {
        [DataMember]
        public int IDServicio { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public string Icono { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public int Visible { get; set; }
        [DataMember]
        public int Orden { get; set; }
        [DataMember]
        public List<Imagen> Imagenes { get; set; }
    }
    [DataContract]
    public class Carga
    {
        [DataMember]
        public List<Categoria> Categorias { get; set; }
        [DataMember]
        public List<Servicio> Servicios { get; set; }
        [DataMember]
        public List<Pagina> Paginas { get; set; }
        [DataMember]
        public List<Cosa> Cosas { get; set; }
        [DataMember]
        public List<Banner> Banners { get; set; }
        [DataMember]
        public List<Noticia> Cabecera { get; set; }
        [DataMember]
        public List<ElementoPortada> Portada { get; set; }
        [DataMember]
        public List<Noticia> Noticias { get; set; }
    }
    [DataContract]
    public class Pagina
    {
        [DataMember]
        public int IDPagina { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Contenido { get; set; }
    }
    [DataContract]
    public class Cosa
    {
        [DataMember]
        public int IDCosa { get; set; }
        [DataMember]
        public string NombreCosa { get; set; }
        [DataMember]
        public string Banner { get; set; }
        [DataMember]
        public string DescripcionCosa { get; set; }
    }
    [DataContract]
    public class Banner
    {
        [DataMember]
        public int IDBanner { get; set; }
        [DataMember]
        public string URLBanner { get; set; }
        [DataMember]
        public string URLBannerAlt { get; set; }
        [DataMember]
        public string Enlace { get; set; }
        [DataMember]
        public int Relevancia { get; set; }
        [DataMember]
        public int IDCategoria { get; set; }
    }
    [DataContract]
    public class Imagen
    {
        [DataMember]
        public int IDImagen { get; set; }
        [DataMember]
        public string URLImagen { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Principal { get; set; }
        [DataMember]
        public int Servidor { get; set; }
        [DataMember]
        public int IDNoticia { get; set; }
        public int IDServicio { get; set; }
    }
    [DataContract]
    public class Puntuacion
    {
        [DataMember]
        public int IDPuntuacion { get; set; }
        [DataMember]
        public int IDNotiPuntu { get; set; }
        [DataMember]
        public int Cantidad { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Imagen { get; set; }
    }
    [DataContract]
    public class Etiqueta
    {
        [DataMember]
        public int IDEtiqueta { get; set; }
        [DataMember]
        public string NombreEtiqueta { get; set; }
    }
    [DataContract]
    public class Comentario
    {
        [DataMember]
        public int IDComentario { get; set; }
        [DataMember]
        public DateTime Creacion { get; set; }
        [DataMember]
        public int IDNoticia { get; set; }
        [DataMember]
        public int IDIDComentario { get; set; }
        [DataMember]
        public int IDUsuario { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public Usuario Usuario { get; set; }
        [DataMember]
        public int Eliminado { get; set; }
    }
    [DataContract]
    public class Usuario
    {
        [DataMember]
        public int IDUsuario { get; set; }
        [DataMember]
        public string NombreUsuario { get; set; }
        [DataMember]
        public string CuentaUsuario { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public byte[] TS { get; set; }
        //public List<Valoracion> Valoraciones { get; set; }
        //public TwitterTokens TokensTwitter { get; set; }
        [DataMember]
        public string Ocupacion { get; set; }
        [DataMember]
        public string Ubicacion { get; set; }
        [DataMember]
        public string Industria { get; set; }
    }
    [DataContract]
    public class PropiedadesPublicidad
    {
        [DataMember]
        public int ParrafosDif { get; set; }
        [DataMember]
        public int LongMinima { get; set; }
        [DataMember]
        public int CantidadBanners { get; set; }
    }
    [DataContract]
    public class ElementoPortada
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
    }
    [DataContract]
    public class Competicion
    {
        [DataMember]
        public int IDCompeticion { get; set; }
        [DataMember]
        public string NombreCompeticion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public List<CategoriaCompeticion> Categorias { get; set; }
    }
    [DataContract]
    public class CategoriaCompeticion
    {
        [DataMember]
        public int IDCategoriaCompeticion { get; set; }
        [DataMember]
        public string NombreCategoria { get; set; }
        [DataMember]
        public int IdCompeticion { get; set; }
        [DataMember]
        public List<Jornada> Jornadas { get; set; }
        [DataMember]
        public List<Equipo> Equipos { get; set; }
    }
    [DataContract]
    public class Equipo
    {
        [DataMember]
        public int IDEquipo { get; set; }
        [DataMember]
        public string NombreEquipo { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public int Puntuacion { get; set; }
        [DataMember]
        public int IDCategoriaCompeticionEquipo { get; set; }
    }
    [DataContract]
    public class Jornada
    {
        [DataMember]
        public int IDJornada { get; set; }
        [DataMember]
        public string NombreJornada { get; set; }
        [DataMember]
        public DateTime? FechaDesde { get; set; }
        [DataMember]
        public DateTime? FechaHasta { get; set; }
        [DataMember]
        public List<Encuentro> Encuentros { get; set; }
    }
    [DataContract]
    public class Encuentro
    {
        [DataMember]
        public int IDEncuentro { get; set; }
        [DataMember]
        public int IdEquipo1 { get; set; }
        [DataMember]
        public string NombreEquipo1 { get; set; }
        [DataMember]
        public string ImagenEquipo1 { get; set; }
        [DataMember]
        public string Resultado { get; set; }
        [DataMember]
        public int IdEquipo2 { get; set; }
        [DataMember]
        public string NombreEquipo2 { get; set; }
        [DataMember]
        public string ImagenEquipo2 { get; set; }
        [DataMember]
        public int IdJornada { get; set; }
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
    }
    public class QueryBusqueda
    {

    }

}
