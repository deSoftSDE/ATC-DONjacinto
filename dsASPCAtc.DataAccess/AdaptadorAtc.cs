using dsCore2.DataAccess;
using EntidadesAtc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

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

        public int UsuariosWebModificarClave(FormularioCambioPassword form)
        {
            var res = 0;
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idUsuarioWeb", form.idUsuarioWeb),
                    new SqlParameter("@claveActual", form.actual),
                    new SqlParameter("@claveNueva", form.newn),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.UsuariosWebModificarClave", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res = AsignaEntero("Resultado");
                }
            }
            return res;
        }
        public List<ImagenCabWeb> ImagenesCabWebLeer()
        {
            var res = new List<ImagenCabWeb>();
            var streaming = _configuration.GetSection("StreamFiles")["rutaStreaming"];
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ImagenesCabWebLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var cw = new ImagenCabWeb
                    {
                        IdImagen = AsignaEntero("IdImagen"),
                        IdEmpresa = AsignaEntero("IdEmpresa"),
                        RowGuid = AsignaGuid("RoweGuid"),
                        ImagenSt = AsignaCadena("ImagenSt"),
                        Titulo = AsignaCadena("Titulo"),
                        Subtitulo = AsignaCadena("Subtitulo"),
                        Contenido = AsignaCadena("Contenido"),
                    };
                    cw.url = streaming + cw.ImagenSt;
                    if (cw.Subtitulo != "")
                    {
                        cw.classcontent = "d-none";
                    } else
                    {
                        cw.classcontent = "";
                    }
                    res.Add(cw);
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

        public EfectosCurso FinanzasEfectosCursoLeer(int iddeudor, int bloque, int pagina)
        {
            var res = new EfectosCurso();
            res.Contenido = new List<EfectoCurso>();

            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@iddeudor", iddeudor),
                    new SqlParameter("@bloque", bloque),
                    new SqlParameter("@pagina", pagina),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.FinanzasEfectosCursoLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Registros = AsignaEntero("Registros");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var c = new EfectoCurso
                    {
                        IdEfectoCobro = AsignaEntero("IdEfectoCobro"),
                        DescripcionTipoEfecto = AsignaCadena("DescripcionTipoEfecto"),
                        FechaRecepcion = AsignaFecha("FechaRecepcion"),
                        FechaVto = AsignaFecha("FechaVto"),
                        FechaCobro = AsignaFechaNull("FechaCobro"),
                        Importe = AsignaDecimal("Importe"),
                        NumeroDocumento = AsignaCadena("NumeroDocumento"),
                        DocumentoOrigen = AsignaCadena("DocumentoOrigen"),
                        Estado = AsignaCadena("Estado"),
                        NombreCartera = AsignaCadena("NombreCartera"),
                    };
                    switch (c.Estado)
                    {
                        case "D":
                            c.Estado = "Pendiente";
                            c.ColorEstado = "red";
                            break;
                        case "P":
                            c.Estado = "Pagado";
                            c.ColorEstado = "green";
                            break;
                        case "R":
                            c.Estado = "Riesgo";
                            c.ColorEstado = "blue";
                            break;
                    }
                    res.Contenido.Add(c);
                };
            }
            return res;
        }


        public DebitosPendientes FinanzasDebitosPendientesLeer(int idcliente, int bloque, int pagina)
        {
            var res = new DebitosPendientes();
            res.Contenido = new List<DebitoPendiente>();

            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idcliente", idcliente),
                    new SqlParameter("@bloque", bloque),
                    new SqlParameter("@pagina", pagina),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.FinanzasDebitosPendientesLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Registros = AsignaEntero("Registros");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var c = new DebitoPendiente
                    {
                        IdDebito = AsignaEntero("IDDebito"),
                        NumeroDocumentoOrigen = AsignaCadena("NumeroDocumentoOrigen"),
                        FechaDocumento = AsignaFecha("FechaDocumento"),
                        FechaVtoPrevista = AsignaFecha("FechaVtoPrevista"),
                        FechaDebitoVencido = AsignaFecha("FechaDebitoVencido"),
                        ImporteRiesgo = AsignaDecimal("ImporteRiesgo"),
                        ImportePendiente = AsignaDecimal("ImportePendiente"),
                        ImporteCobrado = AsignaDecimal("ImporteCobrado"),
                        Estado = AsignaCadena("Estado"),
                        Origen = AsignaCadena("Origen"),
                    };
                    switch (c.Origen)
                    {
                        case "A":
                            c.Origen = "Albarán";
                            break;
                        case "F":
                            c.Origen = "Factura";
                            break;
                        case "M":
                            c.Origen = "Manual";
                            break;
                    }
                    switch (c.Estado)
                    {
                        case "D":
                            c.Estado = "Pendiente";
                            c.ColorEstado = "red";
                            break;
                        case "P":
                            c.Estado = "Pagado";
                            c.ColorEstado = "green";
                            break;
                        case "R":
                            c.Estado = "Riesgo";
                            c.ColorEstado = "blue";
                            break;
                    }
                    res.Contenido.Add(c);
                };
            }
            return res;
        }

        public ExtractosMovimiento FinanzasExtractosLeer(int iddeudor, int bloque, int pagina)
        {
            var res = new ExtractosMovimiento();
            res.Contenido = new List<ExtractoMovimiento>();

            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@iddeudor", iddeudor),
                    new SqlParameter("@bloque", bloque),
                    new SqlParameter("@pagina", pagina),
                     new SqlParameter("@iddelegacion", 1),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.FinanzasExtractosLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Registros = AsignaEntero("Registros");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var c = new ExtractoMovimiento
                    {
                        Tipo = AsignaEntero("Tipo"),
                        Fecha = AsignaFecha("Fecha"),
                        ImporteCargo = AsignaDecimal("ImporteCargo"),
                        ImporteAbono = AsignaDecimal("ImporteAbono"),
                        Comentario = AsignaCadena("Comentario"),
                        IdOrigen = AsignaCadena("IdOrigen"),
                    };
                    
                    res.Contenido.Add(c);
                };
            }
            return res;
        }

        public InfoMenuWeb InfoMenuWebLeer()
        {
            var res = new InfoMenuWeb();
            res.Vehiculos = new List<TipoVehiculo>();
            res.Categorias = new List<Categoria>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.InfoMenusWebLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var vh = new TipoVehiculo
                    {
                        IDTipoVehiculo = AsignaEntero("IDTipoVehiculo"),
                        Imagen = AsignaCadena("Imagen"),
                        Descripcion = AsignaCadena("Descripcion")
                    };
                    
                    res.Vehiculos.Add(vh);
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
                    res.Categorias.Add(cat);
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
        public ListadoAnos AnosLeerPor(int idmodelocarroceria, int idfamilia)
        {
            var res = new ListadoAnos();
            res.Anos = new List<Ano>();
            res.Intervalos = new List<IntervaloAnos>();
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
                    res.Anos.Add(an);
                }
                var listado = splitList<Ano>(res.Anos, 10);
                var c = listado.ToList<List<Ano>>();
                foreach (List<Ano> l in c)
                {
                    
                    l.OrderByDescending(p => p.Valor);
                    var listad = new IntervaloAnos();
                    listad.Anos = l;
                    listad.titulo = listad.Anos[0].Valor.ToString() + " - " + listad.Anos[listad.Anos.Count - 1].Valor.ToString();
                    listad.titulojunto = listad.Anos[0].Valor.ToString() + listad.Anos[listad.Anos.Count - 1].Valor.ToString();
                    res.Intervalos.Add(listad);
                }
                var i = 1;

            }
            return res;
        }
        public static IEnumerable<List<T>> splitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
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
            var acstock = new List<AcumuladoStock>();
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
                    new SqlParameter("@idcategoria", pr.idCategoria),
                    new SqlParameter("@idcliente", pr.idCliente),
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
                    var um = new AcumuladoStock
                    {
                        idArticulo = AsignaEntero("IDArticulo"),
                        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                        StockFinalUV = AsignaDecimal("StockFinalUV"),
                        NombreAlmacen = AsignaCadena("NombreAlmacen"),

                    };
                    acstock.Add(um);
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
                    var um = new AcumuladoStock
                    {
                        idArticulo = AsignaEntero("IDArticulo"),
                        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                        StockFinalUV = AsignaDecimal("StockFinalUV"),
                        NombreAlmacen = AsignaCadena("NombreAlmacen"),

                    };
                    acstock.Add(um);
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
                        Precio = AsignaDecimal("Precio"),
                        Descuento = AsignaDecimal("Dto1"),
                        AcumuladosStock = new List<AcumuladoStock>(),

                    };
                    udman.Add(um);
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
                        Precio = AsignaDecimal("Precio"),
                        Descuento = AsignaDecimal("Dto1"),
                        AcumuladosStock = new List<AcumuladoStock>(),
                    };
                    udman.Add(um);
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
            foreach (AcumuladoStock ac in acstock)
            {
                foreach (UnidadManipulacion ud in udman)
                {
                    if (ud.idUnidadManipulacion == ac.idUnidadManipulacion)
                    {
                        ud.AcumuladosStock.Add(ac);
                    }
                }
            }
            
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
        public Byte[] ImagenesLeerPorID(int IDImagenArticulo)
        {
            Byte[] res = new List<Byte>().ToArray();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDImagenArticulo", IDImagenArticulo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ImagenesArticulosLeerPorID", param);
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
            var unidadesManipulacion = new List<UnidadManipulacion>();
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
                        AcumuladosStock = new List<AcumuladoStock>(),
                    };
                    unidadesManipulacion.Add(um);
                }
                foreach (BuscaArticulo ar in res.Novedades)
                {
                    foreach (UnidadManipulacion ud in unidadesManipulacion)
                    {
                        if (ud.idArticulo == ar.IdArticulo)
                        {
                            ar.UnidadesManipulacion.Add(ud);
                        }
                    }
                }
                foreach (BuscaArticulo ar in res.UnClick)
                {
                    foreach (UnidadManipulacion ud in unidadesManipulacion)
                    {
                        if (ud.idArticulo == ar.IdArticulo)
                        {
                            ar.UnidadesManipulacion.Add(ud);
                        }
                    }
                }
            }
            return res;
        }

        public BuscaArticulo ArticulosLeerPorID(int IDArticulo, int? idcliente, int? idUsuarioWeb)
        {
            var res = new BuscaArticulo();
            res.Modelo = new Modelo();
            var Accesorios = new List<BuscaArticulo>();
            res.Carrocerias = new List<ArticuloCarroceria>();
            res.UnidadesManipulacion = new List<UnidadManipulacion>();
            var AcumuladosStockArticulo = new List<AcumuladoStock>();
            var AcumuladosStockRel = new List<AcumuladoStock>();
            var uds = new List<UnidadManipulacion>();
            var preciosTarifaUM = new List<PrecioTarifaUM>();
            res.Imagenes = new List<ImagenArticulo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                    new SqlParameter("@IDCliente", idcliente),
                    new SqlParameter("@IDUsuarioWeb", idUsuarioWeb),
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
                    var um = new AcumuladoStock
                    {
                        idArticulo = AsignaEntero("IDArticulo"),
                        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                        StockFinalUV = AsignaDecimal("StockFinalUV"),
                        NombreAlmacen = AsignaCadena("NombreAlmacen"),

                    };
                    AcumuladosStockArticulo.Add(um);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                        var um = new AcumuladoStock
                        {
                            idArticulo = AsignaEntero("IDArticulo"),
                            idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                            idAcumuladoUdMan = AsignaEntero("IdAcumuladoUdMan"),
                            StockFinalUV = AsignaDecimal("StockFinalUV"),
                            NombreAlmacen = AsignaCadena("NombreAlmacen"),

                        };
                        AcumuladosStockRel.Add(um);
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
                        AcumuladosStock = new List<AcumuladoStock>(),
                        Precio = AsignaDecimal("Precio"),
                        Descuento = AsignaDecimal("Dto1"),
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
                        AcumuladosStock = new List<AcumuladoStock>(),
                        Precio = AsignaDecimal("Precio"),
                        Descuento = AsignaDecimal("Dto1"),
                    };
                    uds.Add(um);
                }
                _reader.NextResult();
                //while (_reader.Read())
                //{
                //    var ptu = new PrecioTarifaUM
                //    {
                //        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                //        DescripcionUM = AsignaCadena("DescripcionUM"),
                //        PrecioTarifa = AsignaDecimal("PrecioTarifa"),
                //    };
                //    preciosTarifaUM.Add(ptu);
                //}
                //_reader.NextResult();
                //while (_reader.Read())
                //{
                //    var ptu = new PrecioTarifaUM
                //    {
                //        idUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                //        DescripcionUM = AsignaCadena("DescripcionUM"),
                //        PrecioTarifa = AsignaDecimal("PrecioTarifa"),
                //    };
                //    preciosTarifaUM.Add(ptu);
                //}
                //_reader.NextResult();
                while (_reader.Read())
                {
                    var i = new ImagenArticulo
                    {
                        IDImagenArticulo = AsignaEntero("IDImagenArticulo")
                    };
                    res.Imagenes.Add(i);
                }
            }
            //foreach (UnidadManipulacion ud in res.UnidadesManipulacion)
            //{
            //    foreach (PrecioTarifaUM ptu in preciosTarifaUM)
            //    {
            //        if (ptu.idUnidadManipulacion == ud.idUnidadManipulacion)
            //        {
            //            ud.PrecioTarifa = ptu.PrecioTarifa;
            //            ud.DescripcionUM = ptu.DescripcionUM;
            //        }
            //    }
            //}
            //foreach (UnidadManipulacion ud in uds)
            //{
            //    foreach (PrecioTarifaUM ptu in preciosTarifaUM)
            //    {
            //        if (ptu.idUnidadManipulacion == ud.idUnidadManipulacion)
            //        {
            //            ud.PrecioTarifa = ptu.PrecioTarifa;
            //            ud.DescripcionUM = ptu.DescripcionUM;
            //        }
            //    }
            //}

            foreach (AcumuladoStock ast in AcumuladosStockArticulo)
            {
                foreach (UnidadManipulacion ud in res.UnidadesManipulacion)
                {
                    if (ud.idUnidadManipulacion == ast.idUnidadManipulacion)
                    {
                        ud.AcumuladosStock.Add(ast);
                    }
                }
            }
            foreach (AcumuladoStock ast in AcumuladosStockRel)
            {
                foreach (UnidadManipulacion ud in uds)
                {
                    if (ud.idUnidadManipulacion == ast.idUnidadManipulacion)
                    {
                        ud.AcumuladosStock.Add(ast);
                    }
                }
            }
            foreach (BuscaArticulo ar in Accesorios)
            {
                foreach (UnidadManipulacion ud in uds)
                {
                    if (ud.idArticulo == ar.IdArticulo)
                    {
                        ar.UnidadesManipulacion.Add(ud);
                    }
                }
                foreach (Categoria cat in res.Accesorios)
                {
                    if (ar.IdCategoria == cat.IDCategoria)
                    {
                        cat.Articulos.Add(ar);
                    }
                }
                
            }
            res.Accesorios.RemoveAll(c => c.Articulos.Count < 1);
            
            return res;
        }
        public Carrito CarritosUsuariosAnadirArticulo(int IDUsuario, int IDArticulo, int? Cantidad, int? IDUnidadManipulacion, Boolean EnProcesar)
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
                    new SqlParameter("@IDUnidadManipulacion", IDUnidadManipulacion),
                    new SqlParameter("@EnProcesar", EnProcesar),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarritosUsuariosAnadirArticulo", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                res = RellenarCarrito(IDUsuario);
            }
            return res;
        }
        public Carrito CarritosUsuariosEliminarArticulo(int IDUsuario, int IDArticulo, Boolean EnProcesar)
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
                    new SqlParameter("@EnProcesar", EnProcesar),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarritosUsuariosEliminarArticulo", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                res = RellenarCarrito(IDUsuario);
            }
            return res;
        }
        public Carrito CarritosUsuariosLeerPorIDUsuario(int IDUsuario, Boolean EnProcesar)
        {
            var res = new Carrito();

            res.Articulos = new List<ArticuloCarrito>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDUsuario", IDUsuario),
                    new SqlParameter("@EnProcesar", EnProcesar),
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
            var Cliente = new PedidoWeb();
            res.TipoIva = new List<TipoIva>();
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
                    IDUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                    Dto1 = AsignaDecimal("Dto1"),
                    Dto2 = AsignaDecimal("Dto2"),
                    Bon1 = AsignaDecimal("Bon1"),
                    Bon2 = AsignaDecimal("Bon2"),
                    IdTipoIva = AsignaEntero("IdTipoIva"),
                };
                res.Articulos.Add(ar);
            }
            _reader.NextResult();
            while (_reader.Read())
            {
                var tiva = new TipoIva
                {
                    IdTipoIva = AsignaEntero("IDTipoIva"),
                    DescripcionTipoIva = AsignaCadena("DescripcionTipoIva"),
                    PorcentajeIva = AsignaDecimal("PorcentajeIva"),
                    PorcentajeRE = AsignaDecimal("PorcentajeRE"),
                    IdPorcentajeIva = AsignaEntero("IdPorcentajeIva"),
                    Articulos = new List<ArticuloCarrito>(),
                };
                res.TipoIva.Add(tiva);
            }
            _reader.NextResult();
            if (_reader.Read())
            {
                Cliente.AplicarIva = AsignaBool("AplicarIva");
                Cliente.AplicarRe = AsignaBool("AplicarRE");
            }
            _reader.NextResult();
            if (_reader.Read())
            {
                res.Mensaje = AsignaCadena("Mensaje");
            }
            foreach (ArticuloCarrito ar in res.Articulos)
            {
                ar.Precio = CalcularDescuento(ar.PrecioUd, ar.Dto1);
                ar.Precio = CalcularDescuento(ar.Precio, ar.Dto2);
                ar.Precio = CalcularDescuento(ar.Precio, ar.Bon1);
                ar.Precio = CalcularDescuento(ar.Precio, ar.Bon2);
                ar.Precio = ar.Precio * ar.Cantidad;
                foreach (TipoIva tiva in res.TipoIva)
                {
                    if (tiva.IdTipoIva == ar.IdTipoIva)
                    {
                        tiva.Articulos.Add(ar);
                    }
                }
            }

            res.TipoIva.RemoveAll(tiva => tiva.Articulos.Count < 1);
            foreach (TipoIva tiva in res.TipoIva)
            {
                tiva.TotalArticulos = tiva.Articulos.Sum(articulo => articulo.Precio);
                if (Cliente.AplicarIva)
                {

                    tiva.ValorIva = CalcularIva(tiva.TotalArticulos, tiva.PorcentajeIva);
                }
                if (Cliente.AplicarRe)
                {
                    tiva.ValorRE = CalcularIva(tiva.TotalArticulos, tiva.PorcentajeRE);
                }
                tiva.TotalIva = tiva.ValorRE + tiva.ValorIva;

                tiva.Articulos = null;
            }
            res.TotalBaseImponible = res.Articulos.Sum(articulo => articulo.Precio);
            res.TotalIva = res.TipoIva.Sum(tiva => tiva.TotalIva);
            res.TotalPedido = res.TotalBaseImponible + res.TotalIva;

            return res;
        }
        public decimal CalcularDescuento(decimal precio, decimal dto)
        {
            decimal res;
            var resta = (precio * dto) / 100;
            res = precio - resta;
            return res;
        }
        public decimal CalcularIva(decimal precio, decimal iva)
        {
            decimal res;
            var suma = (precio * iva) / 100;
            res = precio + suma;
            return suma;
        }
        public UsuarioWeb UsuariosLogin(string email, string password, string ipaddress)
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
                    new SqlParameter("@password", password),
                    new SqlParameter("@ipaddress", ipaddress)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ValidarUsuario", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IdUsuarioWeb = AsignaEntero("IdUsuarioWeb");
                    res.UltimaConexion = AsignaFechaNull("UltimaConexion");
                    res.UltimaIP = AsignaCadena("UltimaIP");
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
            if (res.Cliente.IDCliente > 0)
            {
                res.Pedidos = PedidosLeer(res.Cliente.IDCliente, 1, 5, null, null, null).Pedidos;
            }
            return res;
        }
        public Carrito CarritoVaciar(int idUsuarioWeb)
        {
            var res = new Carrito();
            res.Articulos = new List<ArticuloCarrito>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDUsuarioWeb", idUsuarioWeb)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarritoVaciar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            return res;
        }
        private decimal CalcularRestaDescuento(decimal precio, decimal dto)
        {
            return (precio * dto) / 100;
        }
        public Carrito PedidosCrear(int IDUsuarioWeb, int IdDomiEnt)
        {
            //throw new Exception("Holi");
            var pw = new PedidoWeb();
            pw.Fecha = DateTime.Now;
            pw.IdDomiEnt = IdDomiEnt;
            pw.LineasPedido = new List<LineaPedidoVentas>();
            pw.LineasIva = new List<LineaIva>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDUsuarioWeb", IDUsuarioWeb)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.PedidosCrear", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    pw.IdDelegacion = AsignaEntero("IdDelegacion");
                    pw.IdCliente = AsignaEntero("IDCliente");
                    pw.AplicarIva = AsignaBool("AplicarIva");
                    pw.AplicarRe = AsignaBool("AplicarRE");
                    pw.IdRegimenIva = AsignaEntero("IDRegimenIva");

                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var lp = new LineaPedidoVentas
                    {
                        IdUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        CantidadXUM = 0,
                        CantidadUM = AsignaEntero("Cantidad"),
                        CantidadUV = AsignaEntero("Cantidad"),
                        TipoTrans = "Insercion",
                        IdArticulo = AsignaEntero("IDArticulo"),
                        IdTipoIva = AsignaEntero("IdTipoIva"),
                        Precio = AsignaDecimal("PrecioUd"),
                        PoDto1 = AsignaDecimal("Dto1"),
                        PoDto2 = AsignaDecimal("Dto2"),
                        ImporteBonificaciones = AsignaDecimal("Bon1"),
                    };
                    pw.LineasPedido.Add(lp);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var li = new LineaIva
                    {
                        IdTipoIva = AsignaEntero("IdPorcentajeIva"),
                        idTipoIvaLeeEste = AsignaEntero("IdTipoIva"),
                        PoIva = AsignaDecimal("PorcentajeIva"),
                        PoRE = AsignaDecimal("PorcentajeRe"),
                        TipoTrans = "Insercion",
                        Articulos = new List<LineaPedidoVentas>(),
                        IdPorcentajeIva = AsignaEntero("IdPorcentajeIva"),
                    };
                    pw.LineasIva.Add(li);
                }

            }
            foreach (LineaPedidoVentas ar in pw.LineasPedido)
            {
                ar.ImporteBruto = ar.Precio * ar.CantidadUM;
                ar.ImporteDtosLinea = CalcularRestaDescuento(ar.ImporteBruto, ar.PoDto1);
                ar.ImporteDtosLinea = ar.ImporteDtosLinea + CalcularRestaDescuento(ar.ImporteBruto, ar.PoDto2);
                ar.ImporteBonificaciones = CalcularRestaDescuento(ar.ImporteBruto, ar.BonifPrecio);
                ar.ImporteNeto = ar.ImporteBruto - ar.ImporteDtosLinea - ar.ImporteBonificaciones;
                ar.ImporteNeto = decimal.Round(ar.ImporteNeto, 2);

                foreach (LineaIva tiva in pw.LineasIva)
                {
                    if (tiva.idTipoIvaLeeEste == ar.IdTipoIva)
                    {
                        if (pw.AplicarRe)
                        {
                            ar.PoRE = tiva.PoRE;
                        }
                        if (pw.AplicarIva)
                        {
                            ar.PoIva = tiva.PoIva;
                        }
                        ar.IdPoIva = tiva.IdPorcentajeIva;
                        tiva.Articulos.Add(ar);
                    }
                }
            }
            pw.LineasIva.RemoveAll(tiva => tiva.Articulos.Count < 1);
            foreach (LineaIva tiva in pw.LineasIva)
            {
                tiva.BaseImponible = tiva.Articulos.Sum(articulo => articulo.ImporteNeto);
                tiva.BaseBruta = tiva.Articulos.Sum(articulo => articulo.ImporteBruto);
                if (pw.AplicarIva)
                {
                    tiva.CuotaIva = CalcularIva(tiva.BaseImponible, tiva.PoIva);
                }
                if (pw.AplicarRe)
                {
                    tiva.CuotaRE = CalcularIva(tiva.BaseImponible, tiva.PoRE);
                }
                tiva.Articulos = null;
            }
            pw.TotalCuotaIva = pw.LineasIva.Sum(ln => ln.CuotaIva);
            pw.TotalCuotaRE = pw.LineasIva.Sum(ln => ln.CuotaRE);
            pw.ImporteBruto = pw.LineasPedido.Sum(lp => lp.ImporteNeto);
            pw.TotalBaseImponible = pw.LineasPedido.Sum(lp => lp.ImporteNeto);
            pw.ImporteDocumento = pw.TotalBaseImponible + pw.TotalCuotaIva + pw.TotalCuotaRE;
            pw.ImporteLiquido = pw.ImporteDocumento;

            var pedido = dsCore.Comun.Ayudas.SerializarACadenaXML(pw);
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@pedido", pedido)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.PedidosProcesar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    pw.IdDelegacion = AsignaEntero("IdDelegacion");
                    pw.IdCliente = AsignaEntero("IDCliente");

                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var lp = new LineaPedidoVentas
                    {
                        IdUnidadManipulacion = AsignaEntero("IDUnidadManipulacion"),
                        CantidadXUM = 0,
                        CantidadUM = AsignaEntero("Cantidad"),
                        CantidadUV = AsignaEntero("Cantidad"),
                        TipoTrans = "Insercion",
                        IdArticulo = AsignaEntero("IDArticulo")
                    };
                    pw.LineasPedido.Add(lp);
                }

            }
            var res = CarritoVaciar(IDUsuarioWeb);
            return res;
        }
        public ListadoFacturas FacturasLeer(int idCliente, int pagina, int bloque, string nFactura, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var res = new ListadoFacturas();
            res.Facturas = new List<Factura>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idCliente", idCliente),
                    new SqlParameter("@pagina", pagina),
                    new SqlParameter("@bloque", bloque),
                    new SqlParameter("@nFactura", nFactura),
                    new SqlParameter("@fechaDesde", fechaDesde),
                    new SqlParameter("@fechaHasta", fechaHasta)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.FacturasLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Registros = AsignaEntero("Registros");

                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var fct = new Factura
                    {
                        IdFactura = AsignaEntero("IdFactura"),
                        FechaDocumento = AsignaFecha("FechaDocumento"),
                        Documento = AsignaCadena("Documento"),
                        TotalBaseImponible = AsignaDecimal("TotalBaseImponible"),
                        TotalCuotaIva = AsignaDecimal("TotalCuotaIva"),
                        TotalCuotaRE = AsignaDecimal("TotalCuotaRE"),
                        ImporteLiquido = AsignaDecimal("ImporteLiquido"),
                    };
                    res.Facturas.Add(fct);
                }

            }
            return res;
        }
        public ListadoFacturas PedidosLeer(int idCliente, int pagina, int bloque, string nFactura, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var res = new ListadoFacturas();
            res.Pedidos = new List<Pedido>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idCliente", idCliente),
                    new SqlParameter("@pagina", pagina),
                    new SqlParameter("@bloque", bloque),
                    new SqlParameter("@nPedido", nFactura),
                    new SqlParameter("@fechaDesde", fechaDesde),
                    new SqlParameter("@fechaHasta", fechaHasta)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.PedidosLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Registros = AsignaEntero("Registros");

                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var fct = new Pedido
                    {
                        IdCabPedidoVentas = AsignaEntero("IDCabPedidoVentas"),
                        FechaDocumento = AsignaFecha("FechaPedido"),
                        Documento = AsignaCadena("Documento"),
                        TotalBaseImponible = AsignaDecimal("TotalBaseImponible"),
                        TotalCuotaIva = AsignaDecimal("TotalCuotaIva"),
                        TotalCuotaRE = AsignaDecimal("TotalCuotaRE"),
                        ImporteLiquido = AsignaDecimal("ImporteLiquido"),
                        Estado = AsignaCadena("Estado"),
                    };
                    switch(fct.Estado)
                    {
                        case "F":
                            fct.Estado = "Finalizado";
                            fct.ColorEstado = "label-primary";
                            break;
                        case "P":
                            fct.Estado = "Pendiente";
                            fct.ColorEstado = "label-warning";
                            break;
                    }
                    res.Pedidos.Add(fct);
                }

            }
            return res;
        }
        public SituacionCliente SituacionClienteLeer(int idCliente)
        {
            var res = new SituacionCliente();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idCliente", idCliente),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Finanzas.LeerSituacionCliente", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.idCliente = AsignaEntero("idCliente");
                    res.ImporteDebitos = AsignaDecimal("ImporteDebitos");
                    res.ImportePendiente = AsignaDecimal("ImportePendiente");
                    res.ImporteRiesgo = AsignaDecimal("ImporteRiesgo");
                    res.MaxDias = AsignaEntero("MaxDias");
                    res.Documentos = AsignaEntero("Documentos");
                    res.HayImpagos = AsignaEntero("HayImpagos");
                    res.DebitosVencidos = AsignaEntero("DebitosVencidos");
                    res.Media = AsignaEntero("Media");
                }
            }
            return res;
        }
        public List<FacturacionMensual> EsquemasMensualesLeer(int idCliente, string proc = "Facturas")
        {
            var res = new List<FacturacionMensual>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idCliente", idCliente)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web." + proc + "MensualesLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                var existe = true;
                while (existe)
                {
                    existe = false;
                    if (_reader.Read())
                    {
                        existe = true;
                        var fm = new FacturacionMensual
                        {
                            Anio = AsignaEntero("Anio"),
                            Mes = AsignaCadena("Mes"),
                            TotalMes = AsignaDecimalNull("TotalMes"),
                            arg = AsignaCadena("Mes") + " " + AsignaCadena("Anio"),
                            val = AsignaDecimal("TotalMes"),
                        };
                        res.Add(fm);
                        existe = true;
                    }
                    _reader.NextResult();
                }

                _reader.NextResult();

            }
            return res;
        }
        public void MensajeMarcarLeido(int idMensaje, int idCliente)
        {
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idCliente", idCliente),
                    new SqlParameter("@idMensaje", idMensaje)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MensajeMarcarLeido", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        public List<MensajeWeb> MensajeLeer(int idCliente, int bloque)
        {
            var res = new List<MensajeWeb>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idCliente", idCliente),
                    new SqlParameter("@bloque", bloque),
                    new SqlParameter("@idMensaje", 0)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MensajeLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var mj = new MensajeWeb
                    {
                        IdMensaje = AsignaEntero("IdMensaje"),

                        IdCliente = AsignaEntero("IdCliente"),

                        Prioridad = AsignaEntero("Prioridad"),

                        Titulo = AsignaCadena("Titulo"),

                        Mensaje = AsignaCadena("Mensaje"),

                        FechaEnvio = AsignaFechaNull("FechaEnvio"),

                        FechaLeido = AsignaFechaNull("FechaLeido"),

                        Leido = AsignaBool("Leido"),

                        TipoTransaccion = AsignaCadena("TipoTransaccion"),

                        Cliente = AsignaCadena("Cliente")
                    };
                    res.Add(mj);
                }

            }
            return res;
        }
        public List<MensajeError> CarritosUsuariosAnadirMasivamente(List<ArticuloBasico> art, Boolean Vaciar, int idUsuarioWeb)
        {
            var res = new List<MensajeError>();
            foreach(ArticuloBasico ab in art)
            {
                try
                {
                    ab.CantidadInt = Int32.Parse(ab.Cantidad);
                }
                catch (Exception ex)
                {
                    ab.CantidadInt = 1;
                }
            }
            var cc = _configuration.GetConnectionString("DefaultConnection");
            var artstr = dsCore.Comun.Ayudas.SerializarACadenaXML(art);
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@articulos", artstr),
                    new SqlParameter("@Vaciar", Vaciar),
                    new SqlParameter("@idUsuarioWeb", idUsuarioWeb),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarritosUsuariosAnadirMasivamente", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var me = new MensajeError
                    {
                        Codigo = AsignaCadena("Codigo"),
                        Estado = AsignaEntero("Estado"),
                        Cantidad = AsignaEntero("Cantidad"),
                        Descripcion = AsignaCadena("Descripcion")
                    };
                    res.Add(me);
                }
            }
            return res;
        }
        public EmpresaWeb DatosEmpresaLeer()
        {
            return DatosEmpresaLeer(null);
        }
        public EmpresaWeb DatosEmpresaLeer(int? idUsuarioWeb)
        {
            var res = new EmpresaWeb();
            var deUsuario = new EmpresaWeb();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idusuarioweb", idUsuarioWeb),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.DatosEmpresaLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IdDatosWeb = AsignaEntero("IdDatosWeb");
                    res.IdEmpresa = AsignaEntero("IdEmpresa");
                    res.GuidImg = AsignaGuid("GuidImg");
                    res.GuidIcono = AsignaGuid("GuidIcono");
                    res.Direccion = AsignaCadena("Direccion");
                    res.CodPostal = AsignaCadena("CodPostal");
                    res.Localidad = AsignaCadena("Localidad");
                    res.Telefono = AsignaCadena("Telefono");
                    res.Email = AsignaCadena("Email");
                    res.Web = AsignaCadena("Web");
                    res.PaginaFacebook = AsignaCadena("PaginaFacebook");
                    res.PaginaInstagram = AsignaCadena("PaginaInstagram");
                    res.PaginaTwitter = AsignaCadena("PaginaTwitter");
                    res.PaginaGooglePlus = AsignaCadena("PaginaGooglePlus");
                    res.PaginaPinterest = AsignaCadena("PaginaPinterest");
                    res.PaginaLinkedIn = AsignaCadena("PaginaLinkedIn");
                    res.AcercaDe = AsignaCadena("AcercaDe");
                    res.IdClienteVentaDirecta = AsignaEntero("IdClienteVentaDirecta");
                    res.VisiblePedidos = AsignaBool("VisiblePedidos");
                    res.VisibleFacturas = AsignaBool("VisibleFacturas");
                    res.VisibleFinanzas = AsignaBool("VisibleFinanzas");
                    res.VisibleCatalogo = AsignaBool("VisibleCatalogo");
                    res.VisibleCuenta = AsignaBool("VisibleCuenta");
                    res.VisibleIdiomas = AsignaBool("VisibleIdiomas");
                    res.VisibleMensajes = AsignaBool("VisibleMensajes");
                    res.VisiblePlantillas = AsignaBool("VisiblePlantillas");
                    res.VisibleInvitado = AsignaBool("VisibleInvitado");
                    res.VisibleVentaDirecta = AsignaBool("VisibleVentaDirecta");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    res.NombreCuenta = AsignaCadena("NombreCuenta");
                    res.Usuario = AsignaCadena("Usuario");
                    res.Clave = AsignaCadena("Clave");
                    res.ServCorreoSal = AsignaCadena("ServCorreoSal");
                    res.PuertoCorreoSal = AsignaEntero("PuertoCorreoSal");

                    res.NombreSitio = AsignaCadena("NombreSitio");
                    res.RutaLogo = AsignaCadena("RutaLogo");

                    res.dirEmailContacto = AsignaCadena("dirEmailContacto");

                    res.VisibleCategorias = AsignaBool("VisibleCategorias");

                    res.VisibleVehiculos = AsignaBool("VisibleVehiculos");

                    res.VisibleNovedades = AsignaBool("VisibleNovedades");

                    res.VisibleExpress = AsignaBool("VisibleExpress");
                    res.Copyright = AsignaCadena("Copyright");
                    res.VisibleUltimosPedidos = AsignaBool("VisibleUltimosPedidos");

                    res.VisibleIP = AsignaBool("VisibleIP");

                    res.VisibleUltimaConexion = AsignaBool("VisibleUltimaConexion");

                    res.VisibleEurocodeListado = AsignaBool("VisibleEurocodeListado");

                    res.VisibleEurocodeFicha = AsignaBool("VisibleEurocodeFicha");

                    res.VisibleAlmacenesListado = AsignaBool("VisibleAlmacenesListado");

                    res.VisibleAlmacenesFicha = AsignaBool("VisibleAlmacenesFicha");


                    res.VisiblePrecioListado = AsignaBool("VisiblePrecioListado");
                    res.VisiblePrecios = AsignaBool("VisiblePrecios");
                    res.VisibleDtos = AsignaBool("VisibleDtos");
                    res.VisibleTotalCompra = AsignaBool("VisibleTotalCompra");
                }
                _reader.NextResult();
                if (_reader.Read())
                {

                    deUsuario.VisiblePedidos = AsignaBool("VisiblePedidos");
                    deUsuario.VisibleFacturas = AsignaBool("VisibleFacturas");
                    deUsuario.VisibleFinanzas = AsignaBool("VisibleFinanzas");
                    deUsuario.VisibleCatalogo = AsignaBool("VisibleCatalogo");
                    deUsuario.VisibleCuenta = AsignaBool("VisibleCuenta");
                    deUsuario.VisibleIdiomas = AsignaBool("VisibleIdiomas");
                    deUsuario.VisibleMensajes = AsignaBool("VisibleMensajes");
                    deUsuario.VisiblePlantillas = AsignaBool("VisiblePlantillas");
                    deUsuario.VisibleInvitado = AsignaBool("VisibleInvitado");
                    deUsuario.VisibleVentaDirecta = AsignaBool("VisibleVentaDirecta");
                    deUsuario.VisibleCategorias = AsignaBool("VisibleCategorias");
                    deUsuario.VisibleVehiculos = AsignaBool("VisibleVehiculos");
                    deUsuario.VisibleNovedades = AsignaBool("VisibleNovedades");
                    deUsuario.VisibleExpress = AsignaBool("VisibleExpress");
                    deUsuario.Copyright = AsignaCadena("Copyright");
                    deUsuario.VisibleUltimosPedidos = AsignaBool("VisibleUltimosPedidos");
                    deUsuario.VisibleIP = AsignaBool("VisibleIP");
                    deUsuario.VisibleUltimaConexion = AsignaBool("VisibleUltimaConexion");
                    deUsuario.VisibleEurocodeListado = AsignaBool("VisibleEurocodeListado");
                    deUsuario.VisibleEurocodeFicha = AsignaBool("VisibleEurocodeFicha");
                    deUsuario.VisibleAlmacenesListado = AsignaBool("VisibleAlmacenesListado");
                    deUsuario.VisibleAlmacenesFicha = AsignaBool("VisibleAlmacenesFicha");
                    deUsuario.VisiblePrecioListado = AsignaBool("VisiblePrecioListado");
                    deUsuario.VisiblePrecios = AsignaBool("VisiblePrecios");
                    deUsuario.VisibleDtos = AsignaBool("VisibleDtos");
                    deUsuario.VisibleTotalCompra = AsignaBool("VisibleTotalCompra");
                }
            }
            if (idUsuarioWeb != null)
            {
                if (idUsuarioWeb > 0)
                {
                    if (res.VisiblePedidos && deUsuario.VisiblePedidos)
                    {
                        res.VisiblePedidos = false;
                    }
                    if (res.VisibleFacturas && deUsuario.VisibleFacturas)
                    {
                        res.VisibleFacturas = false;
                    }
                    if (res.VisibleFinanzas && deUsuario.VisibleFinanzas)
                    {
                        res.VisibleFinanzas = false;
                    }
                    if (res.VisibleMensajes && deUsuario.VisibleMensajes)
                    {
                        res.VisibleMensajes = false;
                    }
                    if (res.VisibleCategorias && deUsuario.VisibleCategorias)
                    {
                        res.VisibleCategorias = false;
                    }
                    if (res.VisibleVehiculos && deUsuario.VisibleVehiculos)
                    {
                        res.VisibleVehiculos = false;
                    }
                    if (res.VisibleNovedades && deUsuario.VisibleNovedades)
                    {
                        res.VisibleNovedades = false;
                    }
                    if (res.VisibleExpress && deUsuario.VisibleExpress)
                    {
                        res.VisibleExpress = false;
                    }
                    if (res.VisibleUltimosPedidos && deUsuario.VisibleUltimosPedidos)
                    {
                        res.VisibleUltimosPedidos = false;
                    }
                    if (res.VisibleIP && deUsuario.VisibleIP)
                    {
                        res.VisibleIP = false;
                    }
                    if (res.VisibleUltimaConexion && deUsuario.VisibleUltimaConexion)
                    {
                        res.VisibleUltimaConexion = false;
                    }
                    if (res.VisibleEurocodeListado && deUsuario.VisibleEurocodeListado)
                    {
                        res.VisibleEurocodeListado = false;
                    }
                    if (res.VisibleEurocodeFicha && deUsuario.VisibleEurocodeFicha)
                    {
                        res.VisibleEurocodeFicha = false;
                    }
                    if (res.VisiblePrecioListado && deUsuario.VisiblePrecioListado)
                    {
                        res.VisiblePrecioListado = false;
                    }
                    if (res.VisiblePrecios && deUsuario.VisiblePrecios)
                    {
                        res.VisiblePrecios = false;
                    }
                    if (res.VisibleDtos && deUsuario.VisibleDtos)
                    {
                        res.VisibleDtos = false;
                    }
                    if (res.VisibleTotalCompra && deUsuario.VisibleTotalCompra)
                    {
                        res.VisibleTotalCompra = false;
                    }
                }
            }
            return res;
        }
    }
    

}
