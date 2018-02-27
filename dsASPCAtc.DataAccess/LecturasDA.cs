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
                case "Noticia":
                    //MetodoRellenarLista = MetodoRellenoNoticia;
                    TipoDato = typeof(Noticia);
                    break;

            }
        }



        /*private void MetodoRellenoNoticia(object entidad)
        {
            var cli = entidad as Noticia;
            if (cli == null)
                return;
            cli.IDNoticia = AsignaEntero("IDNoticia");
            cli.IDAutor = AsignaEntero("IDAutor");
            cli.Titular = AsignaCadena("Titular");
            cli.Contenido = AsignaCadena("Contenido");

        }*/

    }
}
