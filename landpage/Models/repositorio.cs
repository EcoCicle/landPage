namespace repositorio.Models
{
    public class Cadastro
    {
        private static readonly string connectionString =
            "Host=db.sbxsbnxytqvefvxzcbpt.supabase.co;Database=postgres;Username=postgres;Password=*Galiv726;SSL Mode=Require;Trust Server Certificate=true";

        public static bool Cadastro(int idFormulario, string nomeLoja, string descricao, string email, string senha)
        {
            try
            {
                using var connection = new Npgsql.NpgsqlConnection(connectionString);
                connection.Open();

                string query = @"UPDATE Forms 
                        SET email = @email, senha = @senha
                         SET nome_loja = @NomeLoja, descricao = @Descricao 
                         WHERE id = @Id";

                using var command = new Npgsql.NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@senha", senha);
                command.Parameters.AddWithValue("@NomeLoja", nomeLoja);
                command.Parameters.AddWithValue("@Descricao", descricao);
                command.Parameters.AddWithValue("@Id", idFormulario);

                int linhasAfetadas = command.ExecuteNonQuery();

                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar dados da loja: " + ex.Message);
                return false;
            }
        }
    }
}