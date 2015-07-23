﻿using Seggu.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Seggu.Dtos;
using Seggu.Helpers;

namespace Seggu.Desktop.Forms
{

    public partial class Bancos : Form
    {
        #region Private Members
        private IBankService bankService;
        private BankDto currentBank;
        private bool isNew;
        #endregion

        #region Constructor
        public Bancos(IBankService bankService)
        {
            InitializeComponent();
            this.bankService = bankService;
            this.InitializeIndex();
        }
        #endregion

        #region Private Methods
        private void InitializeIndex()
        {
            var table = this.GetBankDataTable();
            txtNombre.Clear();
            txtNumero.Clear();
            this.bankGrid.DataSource = table;
            this.bankGrid.Columns["Id"].Visible = false;
            this.bankGrid.Columns["Numero"].HeaderText = "Código";
            this.currentBank = new BankDto();
        }

        private DataTable GetBankDataTable()
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Numero", typeof(string));
            table.Columns.Add("Nombre", typeof(string));

            var banks = this.bankService.GetAll();

            foreach (var bank in banks)
            {
                var row = table.NewRow();
                row["Id"] = bank.Id;
                row["Numero"] = bank.Number;
                row["Nombre"] = bank.Name;
                table.Rows.Add(row);
            }

            return table;
        }
        #endregion

        #region Public Methods
        public void Index()
        {
            this.InitializeIndex();
        }
        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //eliminar selected row
            if (bankGrid.DataSource != null)
            {
                try
                {
                    string id = bankGrid.SelectedCells[0].Value.ToString();
                    if (!this.bankService.HasAssociatedRecords(id))
                    {
                        if (MessageBox.Show("Esta a punto de eliminar un banco. Esta seguro?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            bankService.Delete(id);
                            this.InitializeIndex();
                            MessageBox.Show("Banco eliminado exitosamente.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El banco seleccionado posee tarjetas de credito o cheques asociados.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar eliminar el banco. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            btnAgregar.Hide();
            btnEditar.Hide();
            btnEliminar.Hide();
            btnGuardar.Show();
            btnCancelar.Show();
            txtNombre.ReadOnly = false;
            txtNumero.ReadOnly = false;
            txtNombre.Clear();
            txtNumero.Clear();
            isNew = true;
            currentBank = null;

        }

        private BankDto GetFormData()
        {
            var bank = new BankDto();
            bank.Name = txtNombre.Text.Trim();
            bank.Number = txtNumero.Text.Trim();
            return bank;
        }

        private void bankGrid_SelectionChanged(object sender, EventArgs e)
        {
            PopulateForm();
        }


        private void PopulateForm()
        {

            if (!bankGrid.Focused)
            {
                bankGrid.ClearSelection();
                bankGrid.Rows[0].Selected = false;
                CancelarAccion();
                return;
            }

            isNew = false;
            CancelarAccion();
            if (bankGrid.CurrentRow != null && bankGrid.CurrentRow.Index > -1)
            {
                currentBank = new BankDto();
                currentBank.Id = bankGrid.Rows[bankGrid.CurrentRow.Index].Cells["Id"].Value.ToString();
                currentBank.Number = bankGrid.Rows[bankGrid.CurrentRow.Index].Cells["Numero"].Value.ToString();
                currentBank.Name = bankGrid.Rows[bankGrid.CurrentRow.Index].Cells["Nombre"].Value.ToString();
            }
            else
            {
                currentBank = null;
            }
            //currentBank = (BankDto)bankGrid.CurrentRow.DataBoundItem;
            fillBank();
        }


        private void fillBank()
        {
            if (currentBank != null)
            {
                txtNombre.Text = currentBank.Name;
                txtNumero.Text = currentBank.Number;
            }
        }
        private void CancelarAccion()
        {
            isNew = false;
            btnCancelar.Hide();
            btnGuardar.Hide();
            btnEditar.Show();
            btnAgregar.Show();
            btnEliminar.Show();
            txtNombre.ReadOnly = true;
            txtNumero.ReadOnly = true;
            txtNombre.Clear();
            txtNumero.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (isNew)
            {
                BankDto bank;
                bank = this.GetFormData();

                if (bank.Name == string.Empty || bank.Number == string.Empty)
                {
                    MessageBox.Show("El nombre del banco o su número no pueden estar vacíos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (bankService.ExistName(bank.Name))
                {
                    MessageBox.Show("El nombre del banco ingresado ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (bankService.ExistNumber(bank.Number))
                {
                    MessageBox.Show("El número del banco ingresado ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!bank.Number.IsAllNumbers())
                {
                    MessageBox.Show("El codigo del banco debe ser un numero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    this.bankService.Save(bank);
                    this.InitializeIndex();
                    MessageBox.Show("Banco guardado exitosamente.");
                }
                catch
                {
                    MessageBox.Show("Error al crear el banco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                if (currentBank == null)
                {
                    MessageBox.Show("Primero debe seleccionar un banco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                currentBank.Name = txtNombre.Text.Trim();
                currentBank.Number = txtNumero.Text.Trim();

                if (currentBank.Name == string.Empty || currentBank.Number == string.Empty)
                {
                    MessageBox.Show("El nombre del banco o su número no pueden estar vacíos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (bankService.ExistName(currentBank.Name) && bankService.ExistNumber(currentBank.Number))
                {
                    MessageBox.Show("El nombre y número del banco ingresado ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!currentBank.Number.IsAllNumbers())
                {
                    MessageBox.Show("El codigo del banco debe ser un numero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    bankService.Update(currentBank);
                    MessageBox.Show("Banco modificado exitosamente.");
                    this.InitializeIndex();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Error al guardar los cambios del banco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (Seggu.Helpers.StringExtensions.isTextNumber(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (currentBank == null)
            {
                MessageBox.Show("Primero debe seleccionar un banco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            btnAgregar.Hide();
            btnEditar.Hide();
            btnEliminar.Hide();
            btnGuardar.Show();
            btnCancelar.Show();
            txtNumero.ReadOnly = false;
            txtNombre.ReadOnly = false;
            isNew = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarAccion();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bankGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateForm();
        }

        private void bankGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateForm();
        }

    }
}
