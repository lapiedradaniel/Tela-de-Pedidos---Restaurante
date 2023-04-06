using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avaliação
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Conexao con= new Conexao();
        private void txtSalvar_Click(object sender, EventArgs e)
        {
            if (txtNumero.Text == "")
            {
                string sql = $"insert into tb_pedido values (null,'{cbxPrato.Text}','{txtValoraPrato.Text}','{cbxBebida.Text}','{txtValorBebida.Text}')";

                if(con.Executar(sql))
                {
                    MessageBox.Show("Cadastrado com sucesso");
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar");
                }

            }
            else
            {
                MessageBox.Show("O campo numero não pode haver registro ao Salvar!!");
            }
            Limpar();
            CarregaTabela();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = $"delete from tb_pedido where numero= '{txtNumero.Text}'";

            if(con.Executar(sql))
            {
                MessageBox.Show("Excluido com sucesso");

            }
            else
            {
                MessageBox.Show("Erro ao Excluir");
            }
            Limpar();
            CarregaTabela();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string sql = $"update tb_pedido set prato= '{cbxPrato.Text}', valor_prato= '{txtValoraPrato.Text}', bebida= '{cbxBebida.Text}', valor_bebida= '{txtValorBebida.Text}' where numero= '{txtNumero.Text}'";
            if (con.Executar(sql) )
            {
                MessageBox.Show("Atualizado com sucesso");

            }
            else
            {
                MessageBox.Show("Erro ao atualizar");
            }
            Limpar();
            CarregaTabela();


        }
        private void Limpar()
        {
            txtNumero.Text = "";
            cbxBebida.Text = null;
            cbxPrato.Text = null;
            txtValoraPrato.Text = "";
            txtValorBebida.Text= null;  
        }

        private ComboBox GetCbxPrato()
        {
            return cbxPrato;
        }

        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dgvDados["numero",e.RowIndex].Value);
            DataTable dados = con.Retorna("select * from tb_pedido where numero=" + codigo);
            txtNumero.Text = codigo.ToString();
            cbxPrato.Text = dados.Rows[0]["prato"].ToString();
            txtValoraPrato.Text = dados.Rows[0]["valor_prato"].ToString();
            cbxBebida.Text = dados.Rows[0]["bebida"].ToString();
            txtValorBebida.Text = dados.Rows[0]["valor_bebida"].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaBebida();
            CarregaPrato();
            CarregaTabela();    

        }
        private void CarregaPrato()
        {
            cbxPrato.DataSource= null;
            cbxPrato.DataSource = con.Retorna("select * from tb_pedido");
            cbxPrato.DisplayMember = "prato";
            cbxPrato.ValueMember = "numero";
        }
        private void CarregaBebida()
        {
            cbxBebida.DataSource= null;
            cbxBebida.DataSource = con.Retorna("select * from tb_pedido");
            cbxBebida.DisplayMember = "bebida";
            cbxBebida.ValueMember = "numero";
        }
        private void CarregaTabela()
        {
            dgvDados.DataSource= null;
            DataTable dados = con.Retorna("select * from tb_pedido");
            if (dados.Rows[0]["numero"].ToString() != "")
            {
                dgvDados.DataSource = dados;
            }
        }
    }
}
