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
    public partial class FrmPrincipal : Form
    {
        DataTable dt = new DataTable();
        public FrmPrincipal()
        {
            InitializeComponent();
            Inicializar();

        }
        //Criando o método Inicializar para quando startar o formulário, preencha o datagrid.
        private void Inicializar()
        {
            dt = Pessoa.GetPessoas();
            //A linha a seguir vai fazer receber o dt no grid
            dgvPessoas.DataSource = dt;
            ConfigurarGradePessoas();
        }
        //Criando esse método para quando inicializar o programa configurar como foi setado a grade.
        private void ConfigurarGradePessoas()
        {
            //Definindo a Fonte e o Tamanho da Fonte
            dgvPessoas.DefaultCellStyle.Font = new Font("Arial", 10);
            //Tamanho do Seletor na esquerda
            dgvPessoas.RowHeadersWidth = 4;            
            //Define para negrito (BOLD) todo o Cabeçalho.
            dgvPessoas.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            dgvPessoas.Columns["id"].HeaderText = "ID";
            //Define a visibilidade, de padrão é TRUE, como está FALSE, fica invisível no DataGrid
            dgvPessoas.Columns["id"].Visible = false;

            dgvPessoas.Columns["nome"].HeaderText = "Nome";
            //Define a largura
            dgvPessoas.Columns["nome"].Width = 351;
            //Alinha o cabeçalho
            dgvPessoas.Columns["nome"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Alinha os dados para centralizado (pq está MiddleCenter)
            dgvPessoas.Columns["nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPessoas.Columns["telefone"].HeaderText = "Telefone";
            dgvPessoas.Columns["telefone"].Width = 355;
            //Padding define o espaçamento (ESQUERDA, TOPO, DIREITA, BAIXO);
            dgvPessoas.Columns["telefone"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);


            //Ordenando pela coluna título ascendente (do menor para o maior)
            dgvPessoas.Sort(dgvPessoas.Columns["nome"], ListSortDirection.Ascending);
        }
         
              
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dgvPessoas.Rows[dgvPessoas.CurrentCell.RowIndex].Cells["Id"].Value);

            using (var frm = new FrmPessoas(id))
            {
                frm.ShowDialog();
                //Realiza a atualização visual do Grid
                dgvPessoas.DataSource = Pessoa.GetPessoas();
                ConfigurarGradePessoas();
            }
        }
        


        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPessoas(0))
            {
                frm.ShowDialog();
                //Realiza a atualização visual do Grid
                dgvPessoas.DataSource = Pessoa.GetPessoas();
                ConfigurarGradePessoas();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            dt = Pessoa.GetPessoas(txtProcurar.Text);
            dgvPessoas.DataSource = dt;
            ConfigurarGradePessoas();
        }


        
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dgvPessoas.Rows[dgvPessoas.CurrentCell.RowIndex].Cells["Id"].Value);

            using (var frm = new FrmPessoas(id, true))
            {
                frm.ShowDialog();
                //Realizará a atualização visual do Grid.
                dgvPessoas.DataSource = Pessoa.GetPessoas();

            }
        }

        //Bloco para executar ao dar double click numa celula na linha do DataGrid
        private void dgvPessoas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(dgvPessoas.Rows[dgvPessoas.CurrentCell.RowIndex].Cells["Id"].Value);

            using (var frm = new FrmPessoas(id))
            {
                frm.ShowDialog();
                //Realiza a atualização visual do Grid
                dgvPessoas.DataSource = Pessoa.GetPessoas();
                ConfigurarGradePessoas();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            var dt = GerarDadosRelatorio();
            using (var frm = new FrmPessoasRelatorio(dt))
            {
                frm.ShowDialog();
            }
        }

        private DataTable GerarDadosRelatorio()
        {
            var dt = new DataTable();
            dt.Columns.Add("nome");
            dt.Columns.Add("telefone");

            foreach (DataGridViewRow item in dgvPessoas.Rows)
            {
                dt.Rows.Add(item.Cells["nome"].Value.ToString(), item.Cells["telefone"].Value.ToString());
            }
            return dt;
        }
    }
}
