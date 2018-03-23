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
                    res.NumPags = AsignaEntero("NumPags");
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
        public BusquedaRapida ArticulosLeerPorCadena(string cadena)
        {
            var res = new BusquedaRapida();
            res.Articulos = new List<ArticuloBasico>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerTopPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var ar = new ArticuloBasico
                    {
                        IDArticulo = AsignaEntero("IDArticulo"),
                        Descripcion = AsignaCadena("Descripcion"),
                    };
                    res.Articulos.Add(ar);
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    res.NumReg = AsignaEntero("NumReg");
                }
            }
            return res;
        }



        public TipoVehiculo TiposVehiculoLeerPorID(int IDTipoVehiculo)
        {
            var res = new TipoVehiculo();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", IDTipoVehiculo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVehiculoLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDTipoVehiculo = AsignaEntero("IDTipoVehiculo");
                    res.Imagen = AsignaCadena("Imagen");
                    res.Descripcion = AsignaCadena("Descripcion");

                }
            }
            return res;
        }
        public List<TipoVehiculo> TiposVehiculoLeer(int? IDTipoVehiculo)
        {
            var res = new List<TipoVehiculo>();
            var iniciales = new List<string>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", IDTipoVehiculo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVehiculoLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var vh = new TipoVehiculo
                    {
                        IDTipoVehiculo = AsignaEntero("IDTipoVehiculo"),
                        Imagen = AsignaCadena("Imagen"),
                        Descripcion = AsignaCadena("Descripcion")
                    };
                    vh.InicialesMarcas = new List<string>();
                    res.Add(vh);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    iniciales.Add(AsignaCadena("Inicial"));
                };
            }
            foreach (TipoVehiculo vh in res)
            {
                if (vh.IDTipoVehiculo == IDTipoVehiculo)
                {
                    vh.InicialesMarcas = iniciales;
                }
            }
            return res;
        }
        public BuscadorMarcas MarcasLeer(int? IDTipoVehiculo)
        {
            var res = new BuscadorMarcas();
            res.Marcas = new List<Marca>();
            res.Iniciales = new List<Inicial>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", IDTipoVehiculo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var mc = new Marca();
                    mc.IDSeccion = AsignaEntero("IDSeccion");
                    mc.DescripcionSeccion = AsignaCadena("DescripcionSeccion");
                    mc.Imagen = AsignaCadena("Imagen");
                    mc.CodigoSeccion = AsignaCadena("CodigoSeccion");
                    mc.Inicial = AsignaCadena("Inicial");
                    res.Marcas.Add(mc);
                };
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ini = new Inicial
                    {
                        Valor = AsignaCadena("Inicial"),
                        Marcas = new List<Marca>(),
                    };
                    res.Iniciales.Add(ini);
                }
            }
            foreach (Marca mc in res.Marcas)
            {
                foreach (Inicial ini in res.Iniciales)
                {
                    if (ini.Valor == mc.Inicial)
                    {
                        ini.Marcas.Add(mc);
                    }
                }
            }
            return res;
        }
        public List<Modelo> ModelosLeer (int? IDTipoVehiculo, int IDSeccion)
        {
            var res = new List<Modelo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", IDTipoVehiculo),
                    new SqlParameter("@IDSeccion", IDSeccion),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ModelosLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var mc = new Modelo
                    {
                        IDFamilia = AsignaEntero("IDFamilia"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        Imagen = AsignaCadena("Imagen"),
                        Inicial = AsignaCadena("Inicial")
                    };
                    res.Add(mc);
                };
            }
            
            return res;
        }
        public Modelo ModelosLeerPorID(int IDFamilia)
        {
            var res = new Modelo();
            res.Imagenes = new List<ImagenFamilia>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDFamilia", IDFamilia),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ModelosLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDFamilia = AsignaEntero("IDFamilia");
                    res.Imagen = AsignaCadena("Imagen");
                    res.IdSeccion = AsignaEntero("IDSeccion");
                    res.CodigoFamilia = AsignaCadena("CodigoFamilia");
                    res.descripcionSeccion = AsignaCadena("DescripcionSeccion");
                    res.DescripcionFamilia = AsignaCadena("DescripcionFamilia");
                    res.descripcionTipoVehiculo = AsignaCadena("DescripcionTipoVehiculo");
                    res.idTipoVehiculo = AsignaEntero("IdTipoVehiculo");

                }
                res.Carrocerias = new List<Carroceria>();
                _reader.NextResult();
                while (_reader.Read())
                {
                    var cr = new Carroceria
                    {
                        Descripcion = AsignaCadena("Descripcion"),
                        IDCarroceria = AsignaEntero("IDCarroceria"),
                        IDModeloCarroceria = AsignaEntero("IDModeloCarroceria"),
                        Imagen = AsignaCadena("Imagen"),
                        CantidadArticulos = AsignaEntero("CantidadArticulos"),
                    };
                    res.Carrocerias.Add(cr);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var im = new ImagenFamilia
                    {
                        IDImagenFamilia = AsignaEntero("IDImagenFamilia"),
                        IDFamilia = AsignaEntero("IDFamilia"),
                        Valor = AsignaCadena("Valor"),
                    };
                    res.Imagenes.Add(im);
                }
            }
            return res;
        }
        public List<Ano> AnosLeerPor(int idmodelocarroceria, int idfamilia)
        {
            var res = new List<Ano>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idmodelocarroceria", idmodelocarroceria),
                    new SqlParameter("@idfamilia", idfamilia),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.AnosLeerPor", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var an = new Ano
                    {
                        Valor = AsignaEntero("Ano"),
                        CantidadArticulos = AsignaEntero("CantidadArticulos"),
                    };
                    res.Add(an);
                }

            }
            return res;
        }
        public Carroceria CarroceriasLeerEsquema(int idmodelocarroceria, int ano)
        {
            var res = new Carroceria();
            res.Vidrios = new List<Vidrio>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idmodelocarroceria", idmodelocarroceria),
                    new SqlParameter("@ano", ano),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarroceriasLeerEsquema", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDCarroceria = AsignaEntero("IDCarroceria");
                    res.Imagen = AsignaCadena("Imagen");
                    res.Descripcion = AsignaCadena("Descripcion");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var vd = new Vidrio
                    {
                        IDVidrio = AsignaEntero("IDVidrio"),
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        PosVer = AsignaEntero("PosVer"),
                        PosHor = AsignaEntero("PosHor"),
                        SpanVer = AsignaEntero("SpanVer"),
                        SpanHor = AsignaEntero("SpanHor"),
                        CantidadArticulos = AsignaEntero("CantidadArticulos"),
                        Imagen = AsignaCadena("Imagen"),
                    };
                    res.Vidrios.Add(vd);
                }
            }
            return res;
        }
        public List<Modelo> ModelosLeerPorMarca(int IDSeccion)
        {
            var res = new List<Modelo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", IDSeccion),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ModelosLeerPorMarca", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var md = new Modelo
                    {
                        IDFamilia = AsignaEntero("IDFamilia"),
                        Imagen = AsignaCadena("Imagen"),
                        IdSeccion = AsignaEntero("IDSeccion"),
                        CodigoFamilia = AsignaCadena("CodigoFamilia"),
                        descripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia")
                    };
                    res.Add(md);
                }

            }
            return res;
        }
        public byte[] ImagenLeerPorIDArticulo(int IDArticulo)
        {
            byte[] res = null;
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ImagenLeerPorIDArticulo", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res = AsignaArrayByte("Imagen");
                };
            }
            return res;
        }

        public Marca MarcasLeerPorID(int IDSeccion)
        {
            var res = new Marca();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", IDSeccion),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDSeccion = AsignaEntero("IDSeccion");
                    res.DescripcionSeccion = AsignaCadena("DescripcionSeccion");
                    res.Imagen = AsignaCadena("Imagen");
                    res.CodigoSeccion = AsignaCadena("CodigoSeccion");
                };
            }
            return res;
        }
        public List<Marca> MarcasLeerPorCadena(string cadena)
        {
            var res = new List<Marca>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var mc = new Marca
                    {
                        IDSeccion = AsignaEntero("IDSeccion"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        Imagen = AsignaCadena("Imagen"),
                        CodigoSeccion = AsignaCadena("CodigoSeccion")
                    };
                    res.Add(mc);
                };
            }
            return res;
        }

        public Carroceria CarroceriasLeerPorID(int IDCarroceria)
        {
            var res = new Carroceria();
            res.Vidrios = new List<Vidrio>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDCarroceria", IDCarroceria),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarroceriasLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDCarroceria = AsignaEntero("IDCarroceria");
                    res.Imagen = AsignaCadena("Imagen");
                    res.Descripcion = AsignaCadena("Descripcion");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var vd = new Vidrio
                    {
                        IDVidrio = AsignaEntero("IDVidrio"),
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        PosVer = AsignaEntero("PosVer"),
                        PosHor = AsignaEntero("PosHor"),
                        SpanVer = AsignaEntero("SpanVer"),
                        SpanHor = AsignaEntero("SpanHor"),
                    };
                    res.Vidrios.Add(vd);
                }
            }
            return res;
        }

        public Vidrio VidriosLeerPorID(int IDVidrio)
        {
            var res = new Vidrio();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDVidrio", IDVidrio),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.VidriosLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDVidrio = AsignaEntero("IDVidrio");
                    res.IDTipoVidrio = AsignaEntero("IDTipoVidrio");
                    res.DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio");
                    res.Descripcion = AsignaCadena("Descripcion");
                    res.Imagen = AsignaCadena("Imagen");
                }
            }
            return res;
        }

        public TipoVidrio TiposVidrioLeerPorID(int IDTipoVidrio)
        {
            var res = new TipoVidrio();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVidrio", IDTipoVidrio),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDTipoVidrio = AsignaEntero("IDTipoVidrio");
                    res.Descripcion = AsignaCadena("Descripcion");
                    res.Imagen = AsignaCadena("Imagen");
                }
            }
            return res;
        }
        public List<TipoVidrio> TiposVidrioLeerPorCadena(string cadena)
        {
            var res = new List<TipoVidrio>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var vid = new TipoVidrio
                    {
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        Imagen = AsignaCadena("Imagen"),
                    };
                    res.Add(vid);
                }
            }
            return res;
        }
        public List<TipoVidrio> TiposVidrioLeer()
        {
            var res = new List<TipoVidrio>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    //new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var vid = new TipoVidrio
                    {
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        Imagen = AsignaCadena("Imagen"),
                    };
                    res.Add(vid);
                }
            }
            return res;
        }
        public List<Carroceria> CarroceriasLeerPorCadena(string cadena)
        {
            var res = new List<Carroceria>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarroceriasLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var cr = new Carroceria
                    {
                        Descripcion = AsignaCadena("Descripcion"),
                        IDCarroceria = AsignaEntero("IDCarroceria"),
                        IDModeloCarroceria = AsignaEntero("IDModeloCarroceria"),
                    };
                    res.Add(cr);
                }
            }
            return res;
        }
        public Parametros ObtenerdescripcionesPorIDs(Parametros pr)
        {
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", pr.idTipoVehiculo),
                    new SqlParameter("@IDSeccion", pr.idSeccion),
                    new SqlParameter("@IDFamilia", pr.idFamilia),
                    new SqlParameter("@idVidrio", pr.idVidrio),
                    new SqlParameter("@idModeloCarroceria", pr.idModeloCarroceria),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ObtenerdescripcionesPorIDs", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    pr.idTipoVehiculo = AsignaEntero("IDTipoVehiculo");
                    pr.descripcionTipoVehiculo = AsignaCadena("Descripcion");
                    
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    pr.idSeccion = AsignaEntero("iDSeccion");
                    pr.descripcionSeccion = AsignaCadena("DescripcionSeccion");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    pr.idFamilia = AsignaEntero("IDFamilia");
                    pr.descripcionFamilia = AsignaCadena("DescripcionFamilia");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    pr.descripcionCarroceria = AsignaCadena("Descripcion");
                    pr.idCarroceria = AsignaEntero("IDCarroceria");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    pr.idTipoVidrio = AsignaEntero("IDTipoVidrio");
                    pr.descripcionTipoVidrio = AsignaCadena("Descripcion");
                }
                
            }
            return pr;
        }
    }
    
}
