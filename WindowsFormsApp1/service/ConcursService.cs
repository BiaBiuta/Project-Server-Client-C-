using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WindowsFormsApp1.domain;
using WindowsFormsApp1.domain.validators;
using WindowsFormsApp1.repository;

using System.Runtime.CompilerServices;

namespace WindowsFormsApp1.service;

public class ConcursService
{
    private IChildRepository _childRepository;
    private ISampleRepository _sampleRepository;
    private IOrganizingRepository _organizingRepository;
    private IRegistrationRepository _registrationRepository;
    private IValidator<Child> childValidator;
    private IValidator<Organizing> organizatorValidator;

    public ConcursService(IChildRepository childRepository, ISampleRepository sampleRepository, IOrganizingRepository organizingRepository, IRegistrationRepository registrationRepository, IValidator<Child> childValidator, IValidator<Organizing> organizatorValidator)
    {
        _childRepository = childRepository;
        _sampleRepository = sampleRepository;
        _organizingRepository = organizingRepository;
        _registrationRepository = registrationRepository;
        this.childValidator = childValidator;
        this.organizatorValidator = organizatorValidator;
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
        
       // notifyObservers(new ChangeEventRegister(ChangeEvent.REGISTER,registration));
        return reg;
    }
}