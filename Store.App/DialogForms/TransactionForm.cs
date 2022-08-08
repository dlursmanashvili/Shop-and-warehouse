using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.App;
using Store.Models;
using Store.Services;

namespace Store.App.DialogForms
{
    public partial class TransactionForm : Form
    {
        private readonly TransactionService _transactionService;
        private readonly object _id;

        private bool EditMode => _id != null;

        public TransactionForm()
        {
            InitializeComponent();
            _transactionService = new(Program.configuration);
        }

        public TransactionForm(object id) : this()
        {
            _id = id;
        }
    }
}