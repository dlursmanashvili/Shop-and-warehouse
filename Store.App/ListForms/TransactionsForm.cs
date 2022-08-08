using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.App.DialogForms;
using Store.App.Interfaces;
using Store.Models;
using Store.Services;

namespace Store.App.ListForms
{
    public partial class TransactionsForm : Form, IListForm
    {
        private int _id;
        private readonly TransactionService _transactionService;

        public TransactionsForm()
        {
            InitializeComponent();
            _transactionService = new(Program.configuration);
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
            grdList.DataSource = LoadTransactions();
        }

        public void InsertRecord()
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord()
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord()
        {
            throw new NotImplementedException();
        }

        public void SearchRecords()
        {
            throw new NotImplementedException();
        }
        private void grdList_CellEnter(object sender, DataGridViewCellEventArgs e)
            => _id = Convert.ToInt32(grdList.Rows[e.RowIndex].Cells["ID"].Value);

        private List<Transaction> LoadTransactions() => _transactionService.Load().ToList();
    }
}
