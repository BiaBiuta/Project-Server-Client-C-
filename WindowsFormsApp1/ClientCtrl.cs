using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Domain.domain;
using Servicies;

namespace WindowsFormsApp1;

public class ClientCtrl:ICompetitionObserver
{
     public event EventHandler<ChatUserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly ICompetitionServices server;
        private Organizing currentUser;
        public ClientCtrl(ICompetitionServices server)
        {
            this.server = server;
            currentUser = null;
        }

        public Organizing login(String userId, String pass)
        {
            Organizing user = new Organizing(userId, pass);
            Organizing organizing = server.login(user, this);
            Console.WriteLine("Login succeeded ....");
            currentUser = user;
            Console.WriteLine("Current user {0}", user);
            return organizing;
        }

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.logout(currentUser, this);
            currentUser = null;
        }

        protected virtual void OnUserEvent(ChatUserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }

        public void participantsRegistered(Registration org)
        {
            ChatUserEventArgs userArgs=new ChatUserEventArgs(CompetitionUserEventArgs.ParticipantRegister,org);
            Console.WriteLine("Message received");
            OnUserEvent(userArgs);
        }

        public List<Child> listChildrenForSample(Sample sample)
        {
            return server.listChildrenForSample(sample);
        }

        public Sample findSample(string ageCategory, string desen)
        {
            return server.findSample( ageCategory,  desen);
        }

        public Registration registerChild(Child child, Sample sample)
        {
            return server.registerChild(child, sample);
        }

        public IEnumerable<Sample> findAllSamle()
        {
            return server.findAllSamle();
        }

        public int numberOfRegistration(Sample sample)
        {
            return server.numberOfChildrenForSample(sample);
        }

        public Organizing FindOrganizing(string username, string password)
        {
            return server.FindOrganizing(username, password);
        }
}