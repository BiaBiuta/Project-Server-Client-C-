using System;
using System.IO;
using System.Windows.Forms;
using Domain.domain;


namespace WindowsFormsApp1;

public partial class Form2 : Form
{
    private ClientCtrl _service;
    public bool ShouldClose { get; set; } = false;
    public Form2(ClientCtrl service)
    {
        InitializeComponent();
       
        this._service = service;
        button1.Click += button1_Click;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
        {
            MessageBox.Show("Nu ați introdus datele","ok",MessageBoxButtons.OK);
            return;
        }

        string username = textBox1.Text;
        string password = textBox2.Text;

        Organizing org = _service.login(username, password);
        if (org == null)
        {
            MessageBox.Show( "Nu există organizatorul","ok",MessageBoxButtons.OK);
            return;
        }

        try
        {
            // Încărcați formularul de start
            Form1 startPage = new Form1(_service);
            startPage.Show();

            // Ștergeți textul din câmpurile de text
            textBox1.Clear();
            textBox2.Clear();
        }
        catch (IOException ex)
        {
            Console.WriteLine("Eroare la încărcarea formularului de start: " + ex.Message);
        }
    }
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        // Verificăm dacă utilizatorul dorește să închidă aplicația
        if (e.CloseReason == CloseReason.UserClosing)
        {
            // Setăm proprietatea ShouldClose pe true pentru a permite închiderea aplicației
            ShouldClose = true;
        }
    }
    
}