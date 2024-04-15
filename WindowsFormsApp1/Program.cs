using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Config;
using WindowsFormsApp1.domain;
using WindowsFormsApp1.domain.validators;
using WindowsFormsApp1.repository;
using WindowsFormsApp1.service;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("App.config"));
            Console.WriteLine("Configuration Settings for tasksDB {0}", GetConnectionStringByName("concurs"));
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("concurs"));
            var sampleRepository = new SampleRepository(props);
            var childRepository = new ChildRepository(props);
            var organizingRepository = new OrganizingRepository(props);
            var registrationRepository = new RegistrationRepository(props, sampleRepository, childRepository);
            var validatorChild = new ChildValidator();
            var organizingValidator = new OrganizingValidator();
            var service = new ConcursService(childRepository, sampleRepository, organizingRepository,
                registrationRepository, validatorChild, organizingValidator);
            // try
            // {
            //     Organizing organizing1 = organizingRepository.FindByName("organizator1", "org1");
            //     Organizing organizing2 = organizingRepository.FindByName("organizator2","org2");
            //     Console.WriteLine(organizing1.Name);
            //     Console.WriteLine(organizing2.Name);
            //     IEnumerable<Organizing> organizingList=organizingRepository.FindAll();
            //     foreach (var org in organizingList)
            //     {
            //         Console.WriteLine(org.Name);
            //     }
            //
            // } catch (Exception ex)
            // {
            //     // Tratarea excepției
            //     // De obicei, aici se afișează mesajul de eroare sau se efectuează alte operații pentru a gestiona excepția
            //     Console.WriteLine("A apărut o excepție: " + ex.Message);
            // }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form2 rm2 = new Form2(service);
            Application.Run(rm2);
            // Pornim loop-ul nostru personalizat pentru a menține aplicația deschisă
            // while (!form2.ShouldClose)
            // {
            //     Application.Run(form2);
            //
            //     Application.DoEvents(); // Permite aplicației să proceseze evenimentele
            //     // Dacă formularul a fost închis, ieșim din loop
            //     if (form2.IsDisposed)
            //         break;
            //
            // }
        }

        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}