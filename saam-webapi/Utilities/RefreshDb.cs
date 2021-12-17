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
            for (int i = 0;i < 4; i++)
            {
                await Maestros(SAAMcontext, i);
            }

            return "Refresh All";
        }

        public static async Task<string> RefreshTerminal(SAAMDbContext SAAMcontext, int terminal)
        {
            await Maestros(SAAMcontext, terminal);

            return "Refresh Terminal";
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
            bool estado = await VerifyState(SAAMcontext,terminal);
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

        public static async Task<bool> VerifyState(SAAMDbContext SAAMcontext,string terminal)
        {
            var especialidades = await SAAMcontext.Especialidades.Where(x => x.Terminal == terminal).ToListAsync();
            var faenas = await SAAMcontext.Faenas.Where(x => x.Terminal == terminal).ToListAsync();
            var lugares = await SAAMcontext.Lugares.Where(x => x.Terminal == terminal).ToListAsync();
            var trabajadores = await SAAMcontext.Trabajadores.Where(x => x.Terminal == terminal).ToListAsync();
            var listas = await SAAMcontext.Listas.Where(x => x.Terminal == terminal).ToListAsync();
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
            int cambioscreado = 0;
            int cambiosmod = 0;
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
                            cambioscreado++;
                        }
                        else
                        {
                            if (especialidades.Nombre != reader["desc_especialidad"].ToString())
                            {
                                especialidades.Nombre = reader["desc_especialidad"].ToString();
                                SAAMcontext.Update(especialidades);
                                await SAAMcontext.SaveChangesAsync();
                                cambiosmod++;
                            }
                        }
                    }
                    if ( maestro == "Faenas")
                    {
                        var faenas = await SAAMcontext.Faenas.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["tipofaena"]) 
                                                                                    && e.Terminal == terminal);
                        if (faenas == null)
                        {
                            await Add(SAAMcontext, terminal, maestro, reader);
                            cambioscreado++;
                        }
                        else
                        {
                            if (faenas.Nombre != reader["descripcion"].ToString())
                            {
                                faenas.Nombre = reader["descripcion"].ToString();
                                SAAMcontext.Update(faenas);
                                await SAAMcontext.SaveChangesAsync();
                                cambiosmod++;
                            }
                        }
                    }
                    if (maestro == "Lugares")
                    {
                        var lugares = await SAAMcontext.Lugares.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["lugar"]) 
                                                                                     && e.Terminal == terminal);
                        if (lugares == null)
                        {
                            await Add(SAAMcontext, terminal, maestro, reader);
                            cambioscreado++;
                        }
                        else
                        {
                            if (lugares.Nombre != reader["descripcion"].ToString())
                            {
                                lugares.Nombre = reader["descripcion"].ToString();
                                SAAMcontext.Update(lugares);
                                await SAAMcontext.SaveChangesAsync();
                                cambiosmod++;
                            }
                        }
                    }
                    if ( maestro == "Trabajadores")
                    {
                        var trabajadores = await SAAMcontext.Trabajadores.FirstOrDefaultAsync(e => e.Rut == reader["rut"].ToString()
                                                                                                && e.Terminal == terminal);
                        if (trabajadores == null)
                        {
                            await Add(SAAMcontext, terminal, maestro, reader);
                            cambioscreado++;
                        }
                        else
                        {
                            var EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["especialidad"]) && e.Terminal == terminal);
                            if (EspecialidadesQueryId == null)
                            {
                                EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == 0 && e.Terminal == terminal);
                            }
                            var TiposQueryId = await SAAMcontext.Tipocontratos.FirstOrDefaultAsync(t => t.Nombre == Convert.ToChar(reader["planta"]) && t.Terminal == terminal);
                            if (trabajadores.Nombres != reader["nombres"].ToString() || trabajadores.Papellido != reader["apellido1"].ToString() 
                                || trabajadores.Sapellido != reader["apellido2"].ToString() || trabajadores.EspecialidadId != EspecialidadesQueryId.Id|| trabajadores.TipocontratoId != TiposQueryId.Id)
                            {
                                trabajadores.Nombres = reader["nombres"].ToString();
                                trabajadores.Papellido = reader["apellido1"].ToString();
                                trabajadores.Sapellido = reader["apellido2"].ToString();
                                trabajadores.TipocontratoId = TiposQueryId.Id;
                                trabajadores.EspecialidadId = EspecialidadesQueryId.Id;
                                SAAMcontext.Update(trabajadores);
                                await SAAMcontext.SaveChangesAsync();
                                cambiosmod++;
                            }
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
                            cambioscreado++;
                        }
                        else
                        {
                            var cartolas = await SAAMcontext.Cartolas.Where(e => e.ListaId == listas.Id && e.Terminal == terminal).ToListAsync();
                            foreach (var cartola in cartolas)
                            {
                                var EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == Convert.ToInt32(reader["especialidad"]) && e.Terminal == terminal);
                                if (EspecialidadesQueryId == null)
                                {
                                    EspecialidadesQueryId = await SAAMcontext.Especialidades.FirstOrDefaultAsync(e => e.Origen == 0 && e.Terminal == terminal);
                                }
                                if (cartola.EspecialidadId != EspecialidadesQueryId.Id || cartola.Rut != reader["rut"].ToString() || cartola.Posicion != Convert.ToInt32(reader["orden"]))
                                {
                                    cartola.EspecialidadId = EspecialidadesQueryId.Id;
                                    cartola.Rut = reader["rut"].ToString();
                                    cartola.Posicion = Convert.ToInt32(reader["orden"]);
                                    SAAMcontext.Update(cartola);
                                    await SAAMcontext.SaveChangesAsync();
                                    cambiosmod++;
                                }
                            }
                        }
                    }

                }
            }
            HistorialRefresco historial = new HistorialRefresco();
            historial.Fecha = DateTime.Now;
            historial.Terminal = terminal;
            historial.Creado = cambioscreado;
            historial.Editado = cambiosmod;
            historial.Eliminado = 0;
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
            historial.Creado = cambios;
            historial.Editado = 0;
            historial.Eliminado = 0;
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
                var TiposQueryId = await SAAMcontext.Tipocontratos.FirstOrDefaultAsync(t => t.Nombre == Convert.ToChar(reader["planta"]) && t.Terminal == terminal);
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
