﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerMaintenance
{
    public partial class frmAddModifyCustomer : Form
    {
        public frmAddModifyCustomer()
        {
            InitializeComponent();
        }

        public bool addCustomer;
        public Customer customer;

        private void frmAddModifyCustomer_Load(object sender, EventArgs e)
        {
            this.LoadStateComboBox();
            if (addCustomer)
            {
                this.Text = "Add Customer";
                cboStates.SelectedIndex = -1;
            }
            else
            {
                this.Text = "Modify Customer";
                this.DisplayCustomer();
            }
        }

        private void LoadStateComboBox()
        {
            // create new List of States
            List<State> states = new List<State>();
            try
            {
                // get states from the state database
                states = StateDB.GetStates();
                cboStates.DataSource = states;
                // what you want the user to see: state name
                cboStates.DisplayMember = "StateName";
                // what field you want in the database: state code
                cboStates.ValueMember = "StateCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void DisplayCustomer()
        {
            txtName.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtCity.Text = customer.City;
            cboStates.SelectedValue = customer.State;
            txtZipCode.Text = customer.ZipCode;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addCustomer)
                {
                    customer = new Customer();
                    this.PutCustomerData(customer);
                    try
                    {
                        customer.CustomerID = CustomerDB.AddCustomer(customer);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    // this is for updating Customer
                    Customer newCustomer = new Customer();
                    // new customer object, old customer object
                    newCustomer.CustomerID = customer.CustomerID;
                    this.PutCustomerData(newCustomer);
                    try
                    {
                        if (! CustomerDB.UpdateCustomer(customer, newCustomer))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that customer.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            customer = newCustomer;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }

        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtName) &&
                Validator.IsPresent(txtAddress) &&
                Validator.IsPresent(txtCity) &&
                Validator.IsPresent(cboStates) &&
                Validator.IsPresent(txtZipCode);
        }

        private void PutCustomerData(Customer customer)
        {
            customer.Name = txtName.Text;
            customer.Address = txtAddress.Text;
            customer.City = txtCity.Text;
            customer.State = cboStates.SelectedValue.ToString();
            customer.ZipCode = txtZipCode.Text;
        }
    }
}
