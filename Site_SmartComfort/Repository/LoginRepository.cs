using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(IConfiguration conf)
        {
            // Obtenha a string de conexão como uma string, não como uma instância de MySqlConnection
            _connectionString = conf.GetConnectionString("ConexaoMySQL");
        }

        public IEnumerable<Usuario> Validacao(string EmailUsu, string SenhaUsu)
        {
            var usuarios = new List<Usuario>();

            // Use o construtor MySqlConnection corretamente para criar uma instância
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string query = "SELECT IdCli, EmailCli, SenhaCli FROM tbUsuario WHERE " +
                               "EmailCli = @EmailCli AND SenhaCli = @SenhaCli";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@EmailCli", EmailUsu);
                    cmd.Parameters.AddWithValue("@SenhaCli", SenhaUsu);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                IdUsu = reader.GetInt32("IdCli"),
                                EmailUsu = reader.GetString("EmailCli"),
                                SenhaUsu = reader.GetString("SenhaCli")
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
    }
}
