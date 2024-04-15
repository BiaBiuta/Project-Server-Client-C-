﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.domain;
using WindowsFormsApp1.service;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ConcursService _service;
        private List<Sample> samples;
        private Sample selectedSample;
        public Form1(ConcursService service)
        {
            InitializeComponent();
            this._service = service;
            InitializeDataGridView();
            LoadSamples();
            // dataGridView.SelectionChanged += DataGridViewParent_SelectionChanged;
            // dataGridView2.SelectionChanged += DataGridView2_SelectionChanged;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
        private void InitializeDataGridView()
        {
            // Setarea coloanelor DataGridView pentru mostenirea
            // unui obiect SimpleObjectProperty<string> pentru a obtine valoarea dorita
            dataGridView.Columns.Add("Proba", "Proba");
            dataGridView.Columns.Add("Varsta", "Varsta");
           
            dataGridView.Columns.Add("nr_inreg", "nr_inreg");
        }

        private void LoadSamples()
        {
            samples = _service.findAllSamle().ToList();
            dataGridView.Rows.Clear();

            foreach (Sample sample in samples)
            {
                int reg = _service.numberOfRegistration(sample);
                dataGridView.Rows.Add(sample.SampleCategory.ToString(), sample.AgeCategory.ToString(), reg.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedSample = samples[dataGridView.SelectedRows[0].Index];
            LoadChildRegistrations(selectedSample);
        }
        private void LoadChildRegistrations(Sample sample)
        {
            List<Child> children = _service.listChildrenForSample(sample);

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Add("Name", "Name");
            dataGridView2.Columns.Add("Age", "Age");
            foreach (Child child in children)
            {
                
                dataGridView2.Rows.Add(child.Name, child.Age);
            }

            dataGridView2.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            return ;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nameChild = textBox1.Text;
            string ageChild = textBox2.Text;

            if (string.IsNullOrEmpty(ageChild) || string.IsNullOrEmpty(nameChild))
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            int age = int.Parse(ageChild);
            string ageCategory;

            // determinarea categoriei de varsta
            if (age < 6)
            {
                MessageBox.Show("Varsta este prea mica.");
                return;
            }
            else if (age < 9)
            {
                ageCategory = "6-8 ani";
            }
            else if (age < 12)
            {
                ageCategory = "9-11 ani";
            }
            else if (age < 16)
            {
                ageCategory = "12-15 ani";
            }
            else
            {
                ageCategory = null;
            }

            List<Sample> selectedSamples = new List<Sample>();

            // verificarea probelor selectate
            if (checkBox1.Checked)
            {
                selectedSamples.Add(_service.findSample(ageCategory, "Desen"));
            }
            if (checkBox2.Checked)
            {
                selectedSamples.Add(_service.findSample(ageCategory, "Cautare comoara"));
            }
            if (checkBox3.Checked)
            {
                selectedSamples.Add(_service.findSample(ageCategory, "Poezie"));
            }

            if (selectedSamples.Count == 0)
            {
                MessageBox.Show("Selectați cel puțin o probă.");
                return;
            }

            foreach (Sample sample in selectedSamples)
            {
                Registration reg = _service.registerChild(new Child(nameChild, age), sample);
            }
            

        
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}