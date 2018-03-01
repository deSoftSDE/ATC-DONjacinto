using dsCore2.DataAccess;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;

namespace dsASPCAppNews.DataAccess
{
    public class LecturasDA : DataList
    {
        public LecturasDA(IConfiguration configuration) : base(configuration)
        {
            var a = _configuration.GetConnectionString("DefaultConnection");
        }
        protected override void AsignarMetodoRelleno()
        {
            switch (Criterio.Entidad)
            {
                case "BuscaArticulo":
                case "VBuscaArticulo":
                case "BuscaArticuloUM":
                    MetodoRellenarLista = metodoRellenoBuscaArticulos;
                    TipoDato = typeof(BuscaArticulo);
                    break;


            }
        }



        private void metodoRellenoBuscaArticulos(object entidadLista)
        {
            ((BuscaArticulo)entidadLista).IdArticulo = AsignaEntero("IdArticulo");
            ((BuscaArticulo)entidadLista).IdUnidadManipulacion = AsignaEnteroNull("IdUnidadManipulacion");
            ((BuscaArticulo)entidadLista).IdUnidadValoracion = AsignaEnteroNull("IdUnidadValoracion");
            ((BuscaArticulo)entidadLista).IdMedidaUM = AsignaEnteroNull("IdUnidadVenta");
            ((BuscaArticulo)entidadLista).Codigo = AsignaCadena("Codigo");
            ((BuscaArticulo)entidadLista).Descripcion = AsignaCadena("Descripcion");
            ((BuscaArticulo)entidadLista).DescripcionUM = AsignaCadena("DescripcionUM");
            ((BuscaArticulo)entidadLista).GTINUC = AsignaCadena("GTINUC");
            ((BuscaArticulo)entidadLista).GTINUM = AsignaCadena("GTINUM");
            ((BuscaArticulo)entidadLista).ModoGestion = AsignaCadena("ModoGestion");
            ((BuscaArticulo)entidadLista).ContenidoVariable = AsignaBool("ContenidoVariable");
            ((BuscaArticulo)entidadLista).UnidadesContenido = AsignaDecimal("UnidadesContenido");
            ((BuscaArticulo)entidadLista).IdTipoPartida = AsignaEnteroNull("IdTipoPartida");
            ((BuscaArticulo)entidadLista).StockUM = AsignaDecimal("StockUM");
            ((BuscaArticulo)entidadLista).StockUV = AsignaDecimal("StockUV");
            ((BuscaArticulo)entidadLista).IdTipoIva = AsignaEnteroNull("IdTipoIva");
        }

    }
}
