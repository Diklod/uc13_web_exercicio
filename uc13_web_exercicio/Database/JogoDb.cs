using Npgsql;
using uc13_web_exercicio.Models;

namespace uc13_web_exercicio.Database
{
    public class JoggDb
    {
        public bool Add(Jogo jogo)
        {
            bool result = false;

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"INSERT INTO games " +
                                         @"(nome, genero, classificacao, idiomas, preco, multiplayer, config_minima, config_recomendada, resumo) " +
                                         @"VALUES " +
                                         @"(@nome, @genero, @classificacao, @idiomas, @preco, @multiplayer, @config_minima, @config_recomendada, @resumo);";

                    command.Parameters.AddWithValue("@nome", jogo.Nome);
                    command.Parameters.AddWithValue("@genero", jogo.Genero);
                    command.Parameters.AddWithValue("@classificacao", jogo.Classificacao);
                    command.Parameters.AddWithValue("@idiomas", jogo.Idiomas);
                    command.Parameters.AddWithValue("@preco", jogo.Preco);
                    command.Parameters.AddWithValue("@multiplayer", jogo.Multiplayer);
                    command.Parameters.AddWithValue("@config_minima", jogo.ConfigMinimina);
                    command.Parameters.AddWithValue("@config_recomendada", jogo.ConfigRecomendada);
                    command.Parameters.AddWithValue("@resumo", jogo.Resumo);


                    AccessDb db = new AccessDb();

                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public Jogo Get(int id)
        {
            Jogo result = new Jogo();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM games WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (cmd.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.Id = Convert.ToInt32(reader["id"]);
                            result.Nome = reader["nome"].ToString();
                            result.Genero = reader["genero"].ToString();
                            result.Classificacao = reader["classificacao"].ToString();
                            result.Idiomas = reader["idiomas"].ToString();
                            result.Preco = float.Parse(reader["preco"].ToString());
                            result.Multiplayer = Convert.ToBoolean(reader["multiplayer"]);
                            result.ConfigMinimina = reader["config_minima"].ToString();
                            result.ConfigRecomendada = reader["config_recomendada"].ToString();
                            result.Resumo = reader["resumo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return result;
        }

        public List<Jogo> GetAll()
        {
            List<Jogo> result = new List<Jogo>();
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT * FROM games ORDER BY id;";

                    using (command.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Jogo jogo = new Jogo();
                            jogo.Id = Convert.ToInt32(reader["id"]);
                            jogo.Nome = reader["nome"].ToString();
                            jogo.Genero = reader["genero"].ToString();
                            jogo.Classificacao = reader["classificacao"].ToString();
                            jogo.Idiomas = reader["idiomas"].ToString();
                            jogo.Preco = float.Parse(reader["preco"].ToString());
                            jogo.Multiplayer = Convert.ToBoolean(reader["multiplayer"]);
                            jogo.ConfigMinimina = reader["config_minima"].ToString();
                            jogo.ConfigRecomendada = reader["config_recomendada"].ToString();
                            jogo.Resumo = reader["resumo"].ToString();
                            result.Add(jogo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public bool Update(Jogo jogo)
        {
            bool result = false;
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE games " +
                                      @"SET nome = @nome, genero = @genero, classificacao = @classificacao, idiomas = @idiomas, preco = @preco, " +
                                      @"multiplayer = @multiplayer, config_minima = @config_minima, config_recomendada = @config_recomendada, resumo = @resumo " +
                                      @"WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", jogo.Id);
                    command.Parameters.AddWithValue("@nome", jogo.Nome);
                    command.Parameters.AddWithValue("@genero", jogo.Genero);
                    command.Parameters.AddWithValue("@classificacao", jogo.Classificacao);
                    command.Parameters.AddWithValue("@idiomas", jogo.Idiomas);
                    command.Parameters.AddWithValue("@preco", jogo.Preco);
                    command.Parameters.AddWithValue("@multiplayer", jogo.Multiplayer);
                    command.Parameters.AddWithValue("@config_minima", jogo.ConfigMinimina);
                    command.Parameters.AddWithValue("@config_recomendada", jogo.ConfigRecomendada);
                    command.Parameters.AddWithValue("@resumo", jogo.Resumo);

                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            { }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE games " +
                                      @"SET deleted = true " +
                                      @"WHERE id = @id; ";

                    command.Parameters.AddWithValue("@id", id);

                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            { }

            return result;
        }

        public int Count()
        {
            int result = 0;
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT COUNT(id) FROM games;";

                    using (command.Connection = db.OpenConnection())
                    {
                        result = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            { }

            return result;
        }
    }
}
