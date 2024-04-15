using System;
using System.Windows.Forms;
using Networking;
using Servicies;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
namespace WindowsFormsApp1;

static class StartClient
{ private static int DEFAULT_PORT=55556;
    private static String DEFAULT_IP="127.0.0.1";
    [STAThread]
    public static void Main(string[] args)
    {
        
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ICompetitionServices server = new CompetitionServerObjectProxy("127.0.0.1", 55556);
        Console.WriteLine("is set server ");
        ClientCtrl ctrl=new ClientCtrl(server);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Form2 win=new Form2(ctrl);
        Console.WriteLine("is will run ");
        Application.Run(win);
    }

}