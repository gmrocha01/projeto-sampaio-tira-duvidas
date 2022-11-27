using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectSampaio
{
    public partial class FrmPessoas : Form
    {
        int id;
        bool excluir = false;
        Pessoa pessoa = new Pessoa();

        public FrmPessoas(int id, bool excluir = false)
        {
            InitializeComponent();
            this.id = id;
            this.excluir = excluir;

            if (this.id > 0)
            {
                pessoa.GetPessoa(this.id);
                lblId.Text = pessoa.Id.ToString();
                txtNome.Text = pessoa.Nome;
                txtTelefone.Text = pessoa.Telefone;
                btnExcluir.Visible = true;
                                

            }
            /*if (this.excluir)
            {
                TravarControles();
                btnSalvar.Visible = false;
                btnExcluir.Visible = true;
            }*/
        }




        private void TravarControles()
        {
            txtNome.Enabled = false;
            txtTelefone.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarForm())
            {
                pessoa.Nome = txtNome.Text;
                pessoa.Telefone = txtTelefone.Text;

                //Chama o método Salvar Pessoa
                pessoa.SalvarPessoa();
                this.Close();
            }
        }

        private bool ValidarForm()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Informe o Nome da Pessoa.", Program.sistema);
                txtNome.Focus();
                return false;
            }
            else if (txtTelefone.Text == "")
            {
                MessageBox.Show("Informe o Telefone da Pessoa.", Program.sistema);
                txtTelefone.Focus();
                return false;
            }            
            else
            {
                return true;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            pessoa.Excluir();
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }    
}
