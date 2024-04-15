using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.domain;
using Persistence.repository;
using Domain.domain;
using Domain.domain.validators;
using Servicies;

namespace Server
{
    public class CompetitionServerImpl:ICompetitionServices
    { 
        private IChildRepository _childRepository;
        private ISampleRepository _sampleRepository;
        private IOrganizingRepository _organizingRepository;
        private IRegistrationRepository _registrationRepository;
        private IValidator<Child> childValidator;
        private IValidator<Organizing> organizatorValidator;
        private readonly IDictionary <Int32, ICompetitionObserver> loggedClients;

    public CompetitionServerImpl(IChildRepository childRepository, ISampleRepository sampleRepository, IOrganizingRepository organizingRepository, IRegistrationRepository registrationRepository, IValidator<Child> childValidator, IValidator<Organizing> organizatorValidator)
    {
        _childRepository = childRepository;
        _sampleRepository = sampleRepository;
        _organizingRepository = organizingRepository;
        _registrationRepository = registrationRepository;
        this.childValidator = childValidator;
        this.organizatorValidator = organizatorValidator;
        this.loggedClients = new Dictionary<int, ICompetitionObserver>();
    }

    public Organizing FindOrganizing(string username, string password)
    {
        return _organizingRepository.FindByName(username, password);
    }

    public IEnumerable<Sample> findAllSamle()
    {
        return _sampleRepository.FindAll();
    }

    public Sample findSample(String age, String sample)
    {
        return _sampleRepository.FindOneByCategoryAndAge(sample,age);
    }

    public List<Child> listChildrenForSample(Sample sample)
    {
        return _registrationRepository.ListChildrenForSample(sample);
    }

    public int numberOfRegistration(Sample sample)
    {
        return _registrationRepository.NumberOfChildrenForSample(sample);
    }
    public Registration registerChild(Child child, Sample sample)
    {
        // TO DO ca nu merege actualizarea numarului 
        Child childFound = _childRepository.FindByName(child.Name);
        if (childFound == null)
        {
            try
            {
                childValidator.Validate(child);
            }
            catch (ValidationException e)
            {
                throw new ApplicationException();
            }

            childFound = _childRepository.Save(child);
        }
        else
        {
            if (childFound.NumberOfSamples == 2)
            {
                MessageBox.Show("Copilul a fost inscris la 2 probe", "ok", MessageBoxButtons.OK);
                return null!;
            }
        }
        int x=childFound.NumberOfSamples+1;
        
        childFound.NumberOfSamples=x;
        if(_registrationRepository.FindOneByChildAndSample(childFound.Id,sample.Id)!=null){
            MessageBox.Show("Copilul a fost inscris la aceasta proba","ok",MessageBoxButtons.OK);
            return null;
        }
        Registration registration=new Registration(childFound,sample);

        _childRepository.Update(childFound);

       
        Registration reg=_registrationRepository.Save(registration);
        
       notifyObservers(registration);
        return reg;
    }

    private void notifyObservers(Registration registration)
    {
        IEnumerable<Organizing> orgs=_organizingRepository.FindAll();
       // ExecutorService executor= Executors.newFixedThreadPool(defaultThreadsNo);
        foreach(Organizing org in orgs){
            if(loggedClients.ContainsKey(org.Id))
            {
                ICompetitionObserver client=loggedClients[org.Id];
                Task.Run(() =>client.participantsRegistered(registration));
            }
        }
    }

    public Child saveChild(string name, int age)
    {
        return _childRepository.Save(new Child(name, age));
    }

    public Child FindChild(string userName)
    {
        return _childRepository.FindByName(userName);
    }

    public Organizing login(Organizing org1, ICompetitionObserver observer)
    {
        Organizing org = _organizingRepository.FindByName(org1.Username,org1.Password);
        if (org != null) {
            if(loggedClients.ContainsKey(org.Id))
                throw new CompetitionException("User already logged in.");
            loggedClients[org.Id] = observer;
        } else {
            throw new CompetitionException("Authentication failed.");
        }
        return org;
    }

    public void logout(Organizing user, ICompetitionObserver observer)
    {
        ICompetitionObserver localClient=loggedClients[user.Id];
        if (localClient==null)
            throw new CompetitionException("User "+user.Username+" is not logged in.");
        loggedClients.Remove(user.Id);
    }
    }
}