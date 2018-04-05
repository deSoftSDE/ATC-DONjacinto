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
        public BusquedaRapida MarcasYModelosLeerPorCadena(string cadena)
        {
            var res = new BusquedaRapida();
            res.Articulos = new List<ArticuloBasico>();
            res.Marcas = new List<Marca>();
            var Modelos = new List<Modelo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasYModelosLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var mc = new Marca
                    {
                        IDSeccion = AsignaEntero("IDSeccion"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        Imagen = AsignaCadena("Imagen"),
                        CodigoSeccion = AsignaCadena("CodigoSeccion"),
                        Inicial = AsignaCadena("Inicial"),
                        Modelos = new List<Modelo>()
                    };
                    res.Marcas.Add(mc);
                };
                _reader.NextResult();
                while (_reader.Read())
                {
                    var mc = new Modelo
                    {
                        IDFamilia = AsignaEntero("IDFamilia"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        Imagen = AsignaCadena("Imagen"),
                        IdSeccion = AsignaEntero("IDSeccion"),
                    };
                    Modelos.Add(mc);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ar = new ArticuloBasico
                    {
                        IDArticulo = AsignaEntero("IDArticulo"),
                        Descripcion = AsignaCadena("Descripcion"),
                    };
                    res.Articulos.Add(ar);
                }
            }
            foreach (Marca mc in res.Marcas)
            {
                foreach (Modelo model in Modelos)
                {
                    if (model.IdSeccion == mc.IDSeccion)
                    {
                        mc.Modelos.Add(model);
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

        public ArticulosYCategorias ArticulosLeerBusqueda(Parametros pr)
        {
            var res = new ArticulosYCategorias();
            res.Articulos = new List<BuscaArticulo>();
            res.Accesorios = new List<Categoria>();
            res.TiposVidrio = new List<TipoVidrio>();
            var accesorios = new List<BuscaArticulo>();
            var udman = new List<UnidadManipulacion>();
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
                    new SqlParameter("@ano", pr.ano),
                    new SqlParameter("@eurocode", pr.eurocode),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerBusqueda", param);
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
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo
                    {
                        IdArticulo = AsignaEntero("IdArticulo"),
                        Codigo = AsignaCadena("Codigo"),
                        Descripcion = AsignaCadena("Descripcion"),
                        IdFamilia = AsignaEnteroNull("IdFamilia"),
                        IdSeccion = AsignaEnteroNull("IdSeccion"),
                        IdGenerico = AsignaEnteroNull("IdGenerico"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        DescripcionCorta = AsignaCadena("DescripcionCorta"),
                        DescripcionDetallada = AsignaCadena("DescripcionDetallada"),
                        IdTipoVidrio = AsignaEntero("IdTipoVidrio"),
                        DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio"),
                        DescripcionWeb1 = AsignaCadena("DescripcionWeb1"),
                        DescripcionWeb2 = AsignaCadena("DescripcionWeb2"),
                        AnoInicial = AsignaEntero("AnoInicial"),
                        AnoFinal = AsignaEntero("AnoFinal"),
                        IdCategoria = AsignaEntero("IDCategoria"),
                        DescripcionCategoria = AsignaCadena("DescripcionCategoria"),
                        UnidadesManipulacion = new List<UnidadManipulacion>(),
                    };
                    res.Articulos.Add(ar);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var um = new UnidadManipulacion
                    {
                        idArticulo = AsignaEntero("IDArticulo"),
                        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                        StockFinalUV = AsignaDecimal("StockFinalUV"),
                        NombreAlmacen = AsignaCadena("NombreAlmacen"),

                    };
                    udman.Add(um);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var cat = new Categoria
                    {
                        IDCategoria = AsignaEntero("IDCategoria"),
                        Descripcion = AsignaCadena("Descripcion"),
                        IdArticuloCategoria = AsignaEntero("IDArticuloCategoria"),
                        Articulos = new List<BuscaArticulo>(),
                    };
                    res.Accesorios.Add(cat);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo
                    {
                        IdArticulo = AsignaEntero("IdArticulo"),
                        Codigo = AsignaCadena("Codigo"),
                        Descripcion = AsignaCadena("Descripcion"),
                        IdFamilia = AsignaEnteroNull("IdFamilia"),
                        IdSeccion = AsignaEnteroNull("IdSeccion"),
                        IdGenerico = AsignaEnteroNull("IdGenerico"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        DescripcionCorta = AsignaCadena("DescripcionCorta"),
                        DescripcionDetallada = AsignaCadena("DescripcionDetallada"),
                        IdTipoVidrio = AsignaEntero("IdTipoVidrio"),
                        DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio"),
                        DescripcionWeb1 = AsignaCadena("DescripcionWeb1"),
                        DescripcionWeb2 = AsignaCadena("DescripcionWeb2"),
                        AnoInicial = AsignaEntero("AnoInicial"),
                        AnoFinal = AsignaEntero("AnoFinal"),
                        IdCategoria = AsignaEntero("IDCategoria"),
                        DescripcionCategoria = AsignaCadena("DescripcionCategoria"),
                        UnidadesManipulacion = new List<UnidadManipulacion>(),
                    };
                    if (!ar.IdTipoVidrio.HasValue)
                    {
                        ar.IdTipoVidrio = 0;
                    }
                    accesorios.Add(ar);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var um = new UnidadManipulacion
                    {
                        idArticulo = AsignaEntero("IDArticulo"),
                        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                        StockFinalUV = AsignaDecimal("StockFinalUV"),
                        NombreAlmacen = AsignaCadena("NombreAlmacen"),

                    };
                    udman.Add(um);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var vid = new TipoVidrio
                    {
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        Imagen = AsignaCadena("Imagen"),
                        Articulos = new List<BuscaArticulo>()
                    };
                    res.TiposVidrio.Add(vid);
                }
            }
            var vidriogenerico = new TipoVidrio
            {
                IDTipoVidrio = 0,
                Descripcion = "Otros",
                Imagen = "",
                Articulos = new List<BuscaArticulo>()
            };
            res.TiposVidrio.Add(vidriogenerico);
            foreach (BuscaArticulo articulo in accesorios)
            {
                foreach (Categoria ct in res.Accesorios)
                {
                    if (ct.IDCategoria == articulo.IdCategoria)
                    {
                        ct.Articulos.Add(articulo);
                    }
                }
                foreach (UnidadManipulacion ud in udman)
                {
                    if (ud.idArticulo == articulo.IdArticulo)
                    {
                        articulo.UnidadesManipulacion.Add(ud);
                    }
                }
            }
            foreach (BuscaArticulo articulo in res.Articulos)
            {
                foreach (UnidadManipulacion ud in udman)
                {
                    if (ud.idArticulo == articulo.IdArticulo)
                    {
                        articulo.UnidadesManipulacion.Add(ud);
                    }
                }
                foreach (TipoVidrio tiv in res.TiposVidrio)
                {
                    if (tiv.IDTipoVidrio == articulo.IdTipoVidrio)
                    {
                        tiv.Articulos.Add(articulo);
                    }
                }
            }
            res.Parametros = pr;
            res.Accesorios.RemoveAll(c => c.Articulos.Count < 1);
            res.TiposVidrio.RemoveAll(d => d.Articulos.Count < 1);
            return res;
        }

        public Byte[] ImagenesLeerPorIDArticulo(int IDArticulo)
        {
            Byte[] res = new List<Byte>().ToArray();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ImagenesLeerPorIDArticulo", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res = AsignaArrayByte("Imagen");
                }
            }
            return res;
        }
        public UnClickYNovedades ArticulosLeerUnClick()
        {
            var res = new UnClickYNovedades();
            res.UnClick = new List<BuscaArticulo>();
            res.Novedades = new List<BuscaArticulo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerUnClick", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo
                    {
                        IdArticulo = AsignaEntero("IdArticulo"),
                        Codigo = AsignaCadena("Codigo"),
                        Descripcion = AsignaCadena("Descripcion"),
                        IdFamilia = AsignaEnteroNull("IdFamilia"),
                        IdSeccion = AsignaEnteroNull("IdSeccion"),
                        IdGenerico = AsignaEnteroNull("IdGenerico"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        DescripcionCorta = AsignaCadena("DescripcionCorta"),
                        DescripcionDetallada = AsignaCadena("DescripcionDetallada"),
                        IdTipoVidrio = AsignaEntero("IdTipoVidrio"),
                        DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio"),
                        DescripcionWeb1 = AsignaCadena("DescripcionWeb1"),
                        DescripcionWeb2 = AsignaCadena("DescripcionWeb2"),
                        AnoInicial = AsignaEntero("AnoInicial"),
                        AnoFinal = AsignaEntero("AnoFinal"),
                        IdCategoria = AsignaEntero("IDCategoria"),
                        DescripcionCategoria = AsignaCadena("DescripcionCategoria"),
                        UnidadesManipulacion = new List<UnidadManipulacion>(),
                    };
                    res.UnClick.Add(ar);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo
                    {
                        IdArticulo = AsignaEntero("IdArticulo"),
                        Codigo = AsignaCadena("Codigo"),
                        Descripcion = AsignaCadena("Descripcion"),
                        IdFamilia = AsignaEnteroNull("IdFamilia"),
                        IdSeccion = AsignaEnteroNull("IdSeccion"),
                        IdGenerico = AsignaEnteroNull("IdGenerico"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        DescripcionCorta = AsignaCadena("DescripcionCorta"),
                        DescripcionDetallada = AsignaCadena("DescripcionDetallada"),
                        IdTipoVidrio = AsignaEntero("IdTipoVidrio"),
                        DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio"),
                        DescripcionWeb1 = AsignaCadena("DescripcionWeb1"),
                        DescripcionWeb2 = AsignaCadena("DescripcionWeb2"),
                        AnoInicial = AsignaEntero("AnoInicial"),
                        AnoFinal = AsignaEntero("AnoFinal"),
                        IdCategoria = AsignaEntero("IDCategoria"),
                        DescripcionCategoria = AsignaCadena("DescripcionCategoria"),
                        UnidadesManipulacion = new List<UnidadManipulacion>(),
                    };
                    res.Novedades.Add(ar);
                }
            }
            return res;
        }

        public BuscaArticulo ArticulosLeerPorID(int IDArticulo)
        {
            var res = new BuscaArticulo();
            res.Modelo = new Modelo();
            var Accesorios = new List<BuscaArticulo>();
            res.Carrocerias = new List<ArticuloCarroceria>();
            res.UnidadesManipulacion = new List<UnidadManipulacion>();
            var uds = new List<UnidadManipulacion>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IdArticulo = AsignaEntero("IdArticulo");
                    res.Codigo = AsignaCadena("Codigo");
                    res.Descripcion = AsignaCadena("Descripcion");
                    res.IdFamilia = AsignaEnteroNull("IdFamilia");
                    res.IdSeccion = AsignaEnteroNull("IdSeccion");
                    res.IdGenerico = AsignaEnteroNull("IdGenerico");
                    res.DescripcionFamilia = AsignaCadena("DescripcionFamilia");
                    res.DescripcionSeccion = AsignaCadena("DescripcionSeccion");
                    res.DescripcionCorta = AsignaCadena("DescripcionCorta");
                    res.DescripcionDetallada = AsignaCadena("DescripcionDetallada");
                    res.IdTipoVidrio = AsignaEntero("IdTipoVidrio");
                    res.DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio");
                    res.DescripcionWeb1 = AsignaCadena("DescripcionWeb1");
                    res.DescripcionWeb2 = AsignaCadena("DescripcionWeb2");
                    res.AnoInicial = AsignaEntero("AnoInicial");
                    res.AnoFinal = AsignaEntero("AnoFinal");
                    res.IdCategoria = AsignaEntero("IDCategoria");
                    res.DescripcionCategoria = AsignaCadena("DescripcionCategoria");
                }
                res.Accesorios = new List<Categoria>();
                _reader.NextResult();
                while (_reader.Read())
                {
                    var cat = new Categoria
                    {
                        IDCategoria = AsignaEntero("IDCategoria"),
                        Descripcion = AsignaCadena("Descripcion"),
                        IdArticuloCategoria = AsignaEntero("IDArticuloCategoria"),
                        Articulos = new List<BuscaArticulo>(),
                    };
                    res.Accesorios.Add(cat);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo
                    {
                        IdArticulo = AsignaEntero("IdArticuloRel"),
                        Descripcion = AsignaCadena("DescripcionArticuloRel"),
                        IdCategoria = AsignaEntero("IdCategoria"),
                        IdArticuloCategoria = AsignaEntero("IDArticuloCategoria"),
                        Codigo = AsignaCadena("Codigo"),
                        UnidadesManipulacion = new List<UnidadManipulacion>()
                    };
                    Accesorios.Add(ar);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var carr = new ArticuloCarroceria
                    {
                        IDModeloCarroceria = AsignaEntero("IDModeloCarroceria"),
                        DescripcionCarroceria = AsignaCadena("DescripcionCarroceria"),
                        Anos = AsignaCadena("Anos"),
                        DescripcionArticuloModelo = AsignaCadena("DescripcionArticuloModelo"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        IDArticuloModelo = AsignaEntero("IDArticuloModelo"),
                        IDFamilia = AsignaEntero("IDFamilia"),
                    };
                    res.Carrocerias.Add(carr);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var um = new UnidadManipulacion
                    {
                        idArticulo = AsignaEntero("IDArticulo"),
                        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                        StockFinalUV = AsignaDecimal("StockFinalUV"),
                        NombreAlmacen = AsignaCadena("NombreAlmacen"),

                    };
                    res.UnidadesManipulacion.Add(um);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                        var um = new UnidadManipulacion
                        {
                            idArticulo = AsignaEntero("IDArticulo"),
                            idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                            idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                            StockFinalUV = AsignaDecimal("StockFinalUV"),
                            NombreAlmacen = AsignaCadena("NombreAlmacen"),

                        };
                        uds.Add(um);
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    res.Modelo.IDFamilia = AsignaEntero("IDFamilia");
                    res.Modelo.Imagen = AsignaCadena("Imagen");
                    res.Modelo.IdSeccion = AsignaEntero("IDSeccion");
                    res.Modelo.CodigoFamilia = AsignaCadena("CodigoFamilia");
                    res.Modelo.descripcionSeccion = AsignaCadena("DescripcionSeccion");
                    res.Modelo.DescripcionFamilia = AsignaCadena("DescripcionFamilia");
                    res.Modelo.descripcionTipoVehiculo = AsignaCadena("DescripcionTipoVehiculo");
                    res.Modelo.idTipoVehiculo = AsignaEntero("IdTipoVehiculo");
                    res.Modelo.Imagenes = new List<ImagenFamilia>();
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
                    res.Modelo.Imagenes.Add(im);
                }
            }
            foreach (BuscaArticulo ar in Accesorios)
            {
                
                foreach (Categoria cat in res.Accesorios)
                {
                    if (ar.IdCategoria == cat.IDCategoria)
                    {
                        cat.Articulos.Add(ar);
                    }
                }
                foreach (UnidadManipulacion ud in uds)
                {
                    if (ud.idArticulo == ar.IdArticulo)
                    {
                        ar.UnidadesManipulacion.Add(ud);
                    }
                }
            }
            res.Accesorios.RemoveAll(c => c.Articulos.Count < 1);
            
            return res;
        }
        public Carrito CarritosUsuariosAnadirArticulo(int IDUsuario, int IDArticulo, int? Cantidad)
        {
            var res = new Carrito();
            res.Articulos = new List<ArticuloCarrito>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                    new SqlParameter("@IDUsuario", IDUsuario),
                    new SqlParameter("@Cantidad", Cantidad),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarritosUsuariosAnadirArticulo", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                res = RellenarCarrito(IDUsuario);
            }
            return res;
        }
        public Carrito CarritosUsuariosEliminarArticulo(int IDUsuario, int IDArticulo)
        {
            var res = new Carrito();
            res.Articulos = new List<ArticuloCarrito>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                    new SqlParameter("@IDUsuario", IDUsuario),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarritosUsuariosEliminarArticulo", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                res = RellenarCarrito(IDUsuario);
            }
            return res;
        }
        public Carrito CarritosUsuariosLeerPorIDUsuario(int IDUsuario)
        {
            var res = new Carrito();
            res.Articulos = new List<ArticuloCarrito>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDUsuario", IDUsuario),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarritosUsuariosLeerPorIDUsuario", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                res = RellenarCarrito(IDUsuario);
            }
            return res;
        }

        private Carrito RellenarCarrito(int IDUsuario)
        {
            var res = new Carrito();
            res.Articulos = new List<ArticuloCarrito>();
            res.IDUsuario = IDUsuario;
            if (_reader.Read())
            {
                res.Precio = AsignaDecimal("Precio");
            }
            _reader.NextResult();
            while (_reader.Read())
            {
                var ar = new ArticuloCarrito
                {
                    IDArticulo = AsignaEntero("IDArticulo"),
                    Descripcion = AsignaCadena("Descripcion"),
                    Cantidad = AsignaEntero("Cantidad"),
                    PrecioUd = AsignaDecimal("PrecioUd"),
                    Precio = AsignaDecimal("Precio"),
                };
                res.Articulos.Add(ar);
            }
            return res;
        }
        public UsuarioWeb UsuariosLogin(string email, string password)
        {
            var res = new UsuarioWeb();
            res.Cliente = new Cliente();
            res.Domicilios = new List<Domicilio>();
            res.Promociones = new List<Promocion>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@nombre", email),
                    new SqlParameter("@password", password)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ValidarUsuario", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IdUsuarioWeb = AsignaEntero("IdUsuarioWeb");
                    res.UltimaConexion = AsignaFechaNull("UltimaConexion");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    res.Cliente.IDCliente = AsignaEntero("IDCliente");
                    res.Cliente.IdDelegacion = AsignaEntero("IdDelegacion");
                    res.Cliente.Clientee = AsignaCadena("Cliente");
                    res.Cliente.NombreComercial = AsignaCadena("NombreComercial");
                    res.Cliente.IdTarifaPrecios = AsignaEntero("IdTarifaPrecios");
                    res.Cliente.IdRegimenIva = AsignaEntero("IdRegimenIva");
                    res.Cliente.Cif = AsignaCadena("Cif");
                    res.Cliente.RazonSocial = AsignaCadena("RazonSocial");
                    res.Cliente.Nombre = AsignaCadena("Nombre");
                    res.Cliente.Apellido1 = AsignaCadena("Apellido1");
                    res.Cliente.Apellido2 = AsignaCadena("Apellido2");
                    res.Cliente.AplicarIva = AsignaEntero("AplicarIva");
                    res.Cliente.AplicarRE = AsignaEntero("AplicarRE");
                    res.Cliente.PoCompensacion = AsignaDecimal("PoCompensacion");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var dom = new Domicilio
                    {
                        IDDomicilioCliente = AsignaEntero("IDDomicilioCliente"),
                        IdCliente = AsignaEntero("IdCliente"),
                        IdDomicilioRelacion = AsignaEntero("IDDomicilioRelacion"),
                        IdRelacion = AsignaEntero("IdRelacion"),
                        IdTipoIva = AsignaEntero("IdTipoIva"),
                        Direccion = AsignaCadena("Direccion"),
                        Numero = AsignaCadena("Numero"),
                        PisoPuerta = AsignaCadena("PisoPuerta"),
                        IdLocalidad = AsignaEntero("IdLocalidad"),
                        NombreMunicipio = AsignaCadena("NombreMunicipio"),
                        CodPostal = AsignaEntero("CodPostal"),
                        IdProvincia = AsignaEntero("IdProvincia"),
                        NombreProvincia = AsignaCadena("NombreProvincia"),
                        IdPais = AsignaEntero("IdPais"),
                        NombreDomicilio = AsignaCadena("NombreDomicilio"),
                        TipoDomicilio = AsignaCadena("TipoDomicilio"),
                        Venta = AsignaEntero("Venta"),
                        Entrega = AsignaEntero("Entrega"),
                        Cobro = AsignaEntero("Cobro"),
                        IdTipoDomicilio = AsignaEntero("IdTipoDomicilio"),
                        NombrePais = AsignaCadena("NombrePais"),
                        ApdoPostal = AsignaCadena("ApdoPostal"),
                    };
                    res.Domicilios.Add(dom);
                }
                _reader.NextResult();
                while (_reader.Read())
                {

                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var pm = new Promocion
                    {
                        IDPromocion = AsignaEntero("IDPromocion"),
                        Nombre = AsignaCadena("Nombre"),
                        Descripcion = AsignaCadena("Descripcion"),
                        Imagen = AsignaCadena("Imagen"),
                        RowGuid = AsignaGuidNull("RowGuid"),
                    };
                    res.Promociones.Add(pm);
                }
            }
            return res;
        }
    }
    
}
