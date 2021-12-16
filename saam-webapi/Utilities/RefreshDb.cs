using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using saam_webapi.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace saam_webapi.Utilities
{
    public static class RefreshDb
    {

        public static async Task<string> RefreshAll(SAAMDbContext SAAMcontext)
        {

            await RefreshATI(SAAMcontext);
            await RefreshITI(SAAMcontext);
            await RefreshSTI(SAAMcontext);
            await RefreshSVTI(SAAMcontext);

            return "Refresh All";
        }

        public static async Task<string> RefreshATI(SAAMDbContext SAAMcontext)
        {
            await Maestros(SAAMcontext, 0);

            return "Refresh ATI";
        }

        public static async Task<string> RefreshITI(SAAMDbContext SAAMcontext)
        {
            await Maestros(SAAMcontext, 1);

            return "Refresh ITI";
        }

        public static async Task<string> RefreshSTI(SAAMDbContext SAAMcontext)
        {
            await Maestros(SAAMcontext, 2);

            return "Refresh STI";
        }

        public static async Task<string> RefreshSVTI(SAAMDbContext SAAMcontext)
        {
            await Maestros(SAAMcontext, 3);

            return "Refresh SVTI";
        }

        public static async Task<bool> Maestros(SAAMDbContext SAAMcontext, int nterminal)
        {
            string[] arrayMaestros = new string[] { "Especialidades", "Faenas", "Lugares", "Trabajadores", "Cartolas" };
            string[] arrayTerminal = new string[] { "Nominacion_ATI", "Nominacion_ITI", "Nominacion_STI", "Nominacion_SVTI" };
            string terminal = arrayTerminal[nterminal];
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "nt-sql-optisat";
            builder.UserID = "notus";
            builder.Password = "n0tu5.0pt1s4t#2021";
            builder.InitialCatalog = terminal;
            bool estado = await VerifyState(SAAMcontext);
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                foreach (string maestro in arrayMaestros)
                {
                    using (SqlCommand command = new SqlCommand(SelectQuery(maestro), connection))
                    {
                        if (estado)
                        {
                            await FirstRefresh(SAAMcontext, terminal, maestro, command);
                        }
                        else
                        {
                            await VerifySAAMDB(SAAMcontext, terminal, maestro, command);
                        }

                    }
                }

                connection.Close();
            }

            return true;
        }

        public static async Task<bool> VerifyState(SAAMDbContext SAAMcontext)
        {
            var especialidades = await SAAMcontext.Especialidades.ToListAsync();
            var faenas = await SAAMcontext.Faenas.ToListAsync();
            var lugares = await SAAMcontext.Lugares.ToListAsync();
            var trabajadores = await SAAMcontext.Trabajadores.ToListAsync();
            var listas = await SAAMcontext.Listas.ToListAsync();
            if (especialidades.Count == 0 && faenas.Count == 0 && lugares.Count == 0 && trabajadores.Count == 0 && listas.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<Boolean> VerifySAAMDB(SAAMDbContext SAAMcontext, string terminal, string maestro, SqlCommand command)
        {
            int ListaN = 0;
            int listaid = 0;
            int contador = 0;
            int cambios = 0;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if ( maestro == "Especialidades")
                    {
                        var especialidades = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["especialidad"]) 
                                                                                                    && e.Terminal == terminal);
                        if (especialidades == null)
                        {
                            await Add(SAAMcontext, terminal, maestro, reader);
                            cambios++;
                        }
                    }
                    if ( maestro == "Faenas")
                    {
                        var faenas = await SAAMcontext.Faenas.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["tipofaena"]) 
                                                                                    && e.Terminal == terminal);
                        if (faenas == null)
                        {
                            await Add(SAAMcontext, terminal, maestro, reader);
                            cambios++;
                        }
                    }
                    if (maestro == "Lugares")
                    {
                        var lugares = await SAAMcontext.Lugares.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["lugar"]) 
                                                                                     && e.Terminal == terminal);
                        if (lugares == null)
                        {
                            await Add(SAAMcontext, terminal, maestro, reader);
                            cambios++;
                        }
                    }
                    if ( maestro == "Trabajadores")
                    {
                        var trabajadores = await SAAMcontext.Trabajadores.FirstOrDefaultAsync(e => e.Rut == reader["rut"].ToString()
                                                                                                && e.Terminal == terminal);
                        if (trabajadores == null)
                        {
                            await Add(SAAMcontext, terminal, maestro, reader);
                            cambios++;
                        }
                    }
                    if (maestro == "Cartolas")
                    {
                        var listas = await SAAMcontext.Listas.FirstOrDefaultAsync(e => e.Nlista == Convert.ToInt16(reader["cartola"])
                                                                                                && e.Terminal == terminal);
                        if (listas == null)
                        {
                            if (ListaN != Convert.ToInt16(reader["cartola"]))
                            {
                                contador++;
                                Lista lista = new Lista();
                                lista.Nombre = "Lista" + contador.ToString();
                                lista.Nlista = Convert.ToInt16(reader["cartola"]);
                                lista.Terminal = terminal;
                                SAAMcontext.Add(lista);
                                await SAAMcontext.SaveChangesAsync();
                                listaid = lista.Id;
                                ListaN = Convert.ToInt16(reader["cartola"]);
                            }

                            var EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["especialidad"]) && e.Terminal == terminal);
                            if (EspecialidadesQueryId == null)
                            {
                                EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == 0 && e.Terminal == terminal);
                            }
                            Cartola cartola = new Cartola();
                            cartola.ListaId = listaid;
                            cartola.EspecialidadId = EspecialidadesQueryId.Id;
                            cartola.Rut = reader["rut"].ToString();
                            cartola.Posicion = Convert.ToInt32(reader["orden"]);
                            cartola.Puerta = 0;
                            cartola.Terminal = terminal;
                            SAAMcontext.Add(cartola);
                            await SAAMcontext.SaveChangesAsync();
                            cambios++;
                        }
                    }

                }
            }
            HistorialRefresco historial = new HistorialRefresco();
            historial.Fecha = DateTime.Now;
            historial.Terminal = terminal;
            historial.Cantidad = cambios;
            historial.Maestro = maestro;
            SAAMcontext.Add(historial);
            await SAAMcontext.SaveChangesAsync();

            return true;
        }

        public static async Task<Boolean> FirstRefresh(SAAMDbContext SAAMcontext, string terminal, string maestro, SqlCommand command)
        {

            int ListaN = 0;
            int listaid = 0;
            int contador = 0;
            int cambios = 0;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (maestro == "Cartolas")
                    {
                        if (ListaN != Convert.ToInt16(reader["cartola"]))
                        {
                            contador++;
                            Lista lista = new Lista();
                            lista.Nombre = "Lista" + contador.ToString();
                            lista.Nlista = Convert.ToInt16(reader["cartola"]);
                            lista.Terminal = terminal;
                            SAAMcontext.Add(lista);
                            await SAAMcontext.SaveChangesAsync();
                            listaid = lista.Id;
                            ListaN = Convert.ToInt16(reader["cartola"]);
                        }

                        var EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["especialidad"]) && e.Terminal == terminal);
                        if (EspecialidadesQueryId == null)
                        {
                            EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == 0 && e.Terminal == terminal);
                        }
                        Cartola cartola = new Cartola();
                        cartola.ListaId = listaid;
                        cartola.EspecialidadId = EspecialidadesQueryId.Id;
                        cartola.Rut = reader["rut"].ToString();
                        cartola.Posicion = Convert.ToInt32(reader["orden"]);
                        cartola.Puerta = 0;
                        cartola.Terminal = terminal;
                        SAAMcontext.Add(cartola);
                        await SAAMcontext.SaveChangesAsync();
                        cambios++;
                    }
                    else
                    {
                        await Add(SAAMcontext, terminal, maestro, reader);
                        cambios++;
                    }
                }
            }

            HistorialRefresco historial = new HistorialRefresco();
            historial.Fecha = DateTime.Now;
            historial.Terminal = terminal;
            historial.Cantidad = cambios;
            historial.Maestro = maestro;
            SAAMcontext.Add(historial);
            await SAAMcontext.SaveChangesAsync();

            return true;
        }

        public static async Task<bool> Add(SAAMDbContext SAAMcontext, string terminal, string maestro, SqlDataReader reader)
        {
            if (maestro == "Especialidades")
            {
                Especialidad especialidad = new Especialidad();
                especialidad.Origen = Convert.ToInt32(reader["especialidad"]);
                especialidad.Nombre = reader["desc_especialidad"].ToString();
                especialidad.Terminal = terminal;
                SAAMcontext.Add(especialidad);
                await SAAMcontext.SaveChangesAsync();
            }
            if (maestro == "Faenas")
            {
                Faena faena = new Faena();
                faena.Origen = Convert.ToInt32(reader["tipofaena"]);
                faena.Nombre = reader["descripcion"].ToString();
                faena.Terminal = terminal;
                SAAMcontext.Add(faena);
                await SAAMcontext.SaveChangesAsync();
            }
            if (maestro == "Lugares")
            {
                Lugar lugar = new Lugar();
                lugar.Origen = Convert.ToInt32(reader["lugar"]);
                lugar.Nombre = reader["descripcion"].ToString();
                lugar.Terminal = terminal;
                SAAMcontext.Add(lugar);
                await SAAMcontext.SaveChangesAsync();
            }
            if (maestro == "Trabajadores")
            {
                var EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["especialidad"]) && e.Terminal == terminal);
                if (EspecialidadesQueryId == null)
                {
                    EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == 0 && e.Terminal == terminal);
                }
                var TiposQueryId = await SAAMcontext.Tipocontratos.FirstOrDefaultAsync(t => t.Nombre == Convert.ToChar(reader["planta"]));
                Trabajador trabajador = new Trabajador();
                trabajador.Nombres = reader["nombres"].ToString();
                trabajador.Papellido = reader["apellido1"].ToString();
                trabajador.Sapellido = reader["apellido2"].ToString();
                trabajador.TipocontratoId = TiposQueryId.Id;
                trabajador.Rut = reader["rut"].ToString();
                trabajador.EspecialidadId = EspecialidadesQueryId.Id;
                trabajador.Terminal = terminal;
                SAAMcontext.Add(trabajador);
                await SAAMcontext.SaveChangesAsync();
            }

            return true;
        }



        public static string SelectQuery(string maestro)
        {
            string QueryString = "";
            if (maestro == "Especialidades")
            {
                QueryString = "SELECT * FROM ut_san_Especial_m";
            }
            if (maestro == "Faenas")
            {
                QueryString = "SELECT * FROM ut_san_TipFaena_m";
            }
            if (maestro == "Lugares")
            {
                QueryString = "SELECT * FROM ut_san_LugaTrab_m";
            }
            if (maestro == "Trabajadores")
            {
                QueryString = "SELECT * FROM ut_san_Personal_m WHERE estado = 'A' ";
            }
            if (maestro == "Cartolas")
            {
                QueryString = "SELECT * FROM ut_san_DetCarto_t ORDER BY cartola";
            }

            return QueryString;
        }


    }
}
