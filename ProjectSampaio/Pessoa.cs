using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectSampaio
{
    class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public void GetPessoa(int id)
        {
            var sql = "SELECT * FROM pessoas WHERE id=" + id;
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    this.Id = Convert.ToInt32(dr["id"]);
                                    this.Nome = dr["nome"].ToString();
                                    this.Telefone = dr["telefone"].ToString();                                   

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SalvarPessoa()
        {
            var sql = "";
            if (this.Id == 0)
            {
                sql = "INSERT INTO pessoas (nome, telefone) VALUES (@nome, @telefone)";
            }
            else
            {
                sql = "UPDATE pessoas SET nome=@nome, telefone=@telefone WHERE id=" + this.Id;
            }
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sql, cn))
                    {
                        //Passando do Objeto para dentro do Banco de Dados
                        cmd.Parameters.AddWithValue("@nome", this.Nome);
                        cmd.Parameters.AddWithValue("@telefone", this.Telefone);

                        //Executa o Update
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Excluir()
        {
            var sql = "DELETE FROM pessoas WHERE id=" + this.Id;
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable GetPessoas(string procurar = "")
        {
            var dt = new DataTable();

            var sql = "SELECT id, nome, telefone FROM teste_cadastros.pessoas";

            if (procurar != "")
            {
                //Concatenando para buscar na coluna titulo ou autores caso seja passado algum parametro no método GetPessoas
                sql += " WHERE nome LIKE '%" + procurar + "%' OR telefone LIKE '%" + procurar + "%'";
            }

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var da = new MySqlDataAdapter(sql, cn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
    }
}
