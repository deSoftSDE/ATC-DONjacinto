using dsCore2.DataAccess;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dsASPCAtc.DataAccess
{
    public class AdaptadorAtc : BaseDataAccess
    {
        private readonly IConfiguration _configuration;
        public AdaptadorAtc(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ListadoArticulos LeerArticulosVenta(BusquedaArticulos bs)
        {
            var res = new ListadoArticulos();
            res.Articulos = new List<Articulo>();
            var uds = new List<UnidadManipulacion>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idCliente", bs.idCliente),
                    new SqlParameter("@idDelegacion", bs.idDelegacion.NullValue()),
                    new SqlParameter("@idSeccion", bs.idDelegacion.NullValue()),
                    new SqlParameter("@idFamilia", bs.idDelegacion.NullValue()),
                    new SqlParameter("@idGenerico", bs.idDelegacion.NullValue()),
                    new SqlParameter("@pagina", bs.pagina),
                    new SqlParameter("@tamanoPagina", bs.tamanoPagina),
                    new SqlParameter("@orderBy", bs.orderBy),
                    new SqlParameter("@tipoOrden", bs.tipoOrden),
                    new SqlParameter("@valorFuncion", bs.valorFuncion),
                    new SqlParameter("@forzarImagenes", bs.forzarImagenes),

                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.LeerArticulosVenta", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.NumReg = AsignaEntero("NumReg");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ud = new UnidadManipulacion
                    {
                        idArticulo = AsignaEntero("IdArticulo"),
                        idUnidadManipulacion = AsignaEntero("IdUnidadManipulacion"),
                        DescripcionUM = AsignaCadena("DescripcionUM"),
                        GTIN = AsignaCadena("GTIN"),
                        CodUdVenta = AsignaCadena("CodUdVenta"),
                        DescUdVenta = AsignaCadena("DescUdVenta"),
                        ModoContenido = AsignaCadena("ModoContenido"),
                        UnidadesContenido = AsignaDecimal("UnidadesContenido"),
                        PrecioTarifa = AsignaDecimal("PrecioTarifa"),
                    };
                    uds.Add(ud);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ar = new Articulo
                    {
                        idArticulo = AsignaEntero("IDArticulo"),
                        Codigo = AsignaEntero("Codigo"),
                        Descripcion = AsignaCadena("Descripcion"),
                        GTINUC = AsignaCadena("GTINUC"),
                        CodUdValor = AsignaCadena("CodUdValor"),
                        DescUdValor = AsignaCadena("DescUdValor"),
                        Imagen = AsignaCadena("Imagen"),
                        RowGuid = AsignaGuidNull("RowGuid"),
                        idSeccion = AsignaEntero("IDSeccion"),
                        idFamilia = AsignaEnteroNull("IDFamilia"),
                        idGenerico = AsignaEnteroNull("IDGenerico"),
                        idTipoIva = AsignaEntero("IDTipoIva"),
                        idArtProm = AsignaEnteroNull("idArtProm"),
                        UnidadesManipulacion = new List<UnidadManipulacion>(),
                    };
                    res.Articulos.Add(ar);
                }
            }
            foreach (Articulo ar in res.Articulos)
            {
                foreach (UnidadManipulacion ud in uds)
                {
                    if (ud.idArticulo == ar.idArticulo)
                    {
                        ar.UnidadesManipulacion.Add(ud);
                    }
                }
            }
            return res;
        }
        public List<ArticuloBasico> ArticulosLeerPorCadena(string cadena)
        {
            var res = new List<ArticuloBasico>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var ar = new ArticuloBasico
                    {
                        IDArticulo = AsignaEntero("IDArticulo"),
                        Descripcion = AsignaCadena("Descripcion"),
                    };
                    res.Add(ar);
                }
            }
            return res;
        }
    }
    
}
