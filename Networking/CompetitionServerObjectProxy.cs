using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Domain.domain;
using Servicies;

namespace Networking
{
    public class CompetitionServerObjectProxy:ICompetitionServices
    {
        private string host;
        private int port;

        private ICompetitionObserver client;

        private NetworkStream stream;
		
        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        public CompetitionServerObjectProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses=new Queue<Response>();
        }
        private void closeConnection()
        {
            finished=true;
            try
            {
                stream.Close();
			
                connection.Close();
                _waitHandle.Close();
                client=null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }
        private void sendRequest(Request request)
		{
			try
			{
                formatter.Serialize(stream, request);
                stream.Flush();
			}
			catch (Exception e)
			{
				throw new CompetitionException(("Error sending object "+e));
			}

		}

		private Response readResponse()
		{
			Response response =null;
			try
			{
                _waitHandle.WaitOne();
                Console.WriteLine("am scos din coada");
				lock (responses)
				{
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                
				}
				

			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}
		private void initializeConnection()
		{
			 try
			 {
				connection=new TcpClient(host,port);
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				finished=false;
                _waitHandle = new AutoResetEvent(false);
				startReader();
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}
		private void startReader()
		{
			Thread tw =new Thread(run);
			Console.WriteLine("am ajuns aici");
			tw.Start();
		}


		private void handleUpdate(UpdateResponse update)
		{
			if (update is NewResponse)
			{

				NewResponse registration = (NewResponse)update;
				RegistrationDTO registrationDto = registration.RegistrationDto;
				Registration reg = DTOUtils.getFromDTO(registrationDto);
				Console.WriteLine("new registration"+reg);
				try
				{
					client.participantsRegistered(reg);
				}
				catch (CompetitionException e)
				{
                    Console.WriteLine(e.StackTrace); 
				}
			}
		}
		public virtual void run()
			{
				Console.WriteLine("am intrat in run ");
				while(!finished)
				{
					try
					{
                        object response = formatter.Deserialize(stream);
						Console.WriteLine("response received "+response);
						if (response is UpdateResponse)
						{
							 handleUpdate((UpdateResponse)response);
						}
						else
						{
							Console.WriteLine("am ajuns la set");
                                responses.Enqueue((Response)response);
                            _waitHandle.Set();
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("Reading error "+e);
					}
					
				}
			}

			public Organizing FindOrganizing(string username, string password)
			{
				OrganizingDTO org = new OrganizingDTO(username, password);
				Request req = new FindOrganizing(org);
				sendRequest(req);
				Response response=readResponse();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}
				FindOrganizingResponse resp = (FindOrganizingResponse)response;
				OrganizingDTO orgDto = resp.OrganizingDto;
				Organizing org1= DTOUtils.getFromDTO(orgDto);
				return org1;
			}

			public IEnumerable<Sample> findAllSamle()
			{
				FindAllSample req = new FindAllSample();
				sendRequest(req);
				Console.WriteLine("am trimis cerere ");
				Response response=readResponse();
				Console.WriteLine("am primit raspuns ");
				if (response is ErrorResponse)
				{
					ErrorResponse errorResponse = (ErrorResponse)response;
					throw new CompetitionException(errorResponse.Message);
				}

				FindAllSampleResponse resp = (FindAllSampleResponse)response;
				List<SamplesDTO> samplesDTO = resp.ListSamples;
				List<Sample> samples= DTOUtils.getFromDTOSamples(samplesDTO);
				return samples;
			}

			public Sample findSample(string age, string sample)
			{
				SamplesDTO sampleDTO=new SamplesDTO(sample,age);
				Request req = new FindSample(sampleDTO);
				sendRequest(req);
				Response response=readResponse();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}

				FindSampleResponse resp = (FindSampleResponse)response;
				SamplesDTO sampleDTO1 = resp.SamplesDto;
				Sample sample1= DTOUtils.getFromDTO(sampleDTO1);
				Console.WriteLine(sample1.Id);
				return sample1;
			}

			public List<Child> listChildrenForSample(Sample sample)
			{
				SamplesDTO samplesDto = DTOUtils.getDTO(sample);
				Request req = new ListChildrenForSample(samplesDto);
				sendRequest(req);
				Response response = readResponse();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}

				ListChildrenForSampleResponse resp = (ListChildrenForSampleResponse)response;
				List<ChildDTO> childDTO = resp.ListChildren;
				List<Child> children = DTOUtils.getFromDTOChild(childDTO);
				return children;
			}

			

			public void x()
			{
				
			}

			public Registration registerChild(Child child, Sample sample)
			{
				ChildDTO childDTO= DTOUtils.getDTO(child);
				Child child1= FindChild(childDTO.Name);
				if(child1==null){
					child1=saveChild(child.Name,child.Age);
				}
				SamplesDTO sampleDTO= DTOUtils.getDTO(sample);
				Sample sample1= findSample(sampleDTO.AgeCategory,sampleDTO.SampleCategory);
				RegistrationDTO regDTO=new RegistrationDTO(child1.Id.ToString(),sample1.Id.ToString(),sample1.SampleCategory.GetCategoryName(),sample1.AgeCategory.GetCategoryName());
				RegisterChild req=new RegisterChild(regDTO);
				sendRequest(req);
				Response response=readResponse();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}

				RegisterChildResponse resp = (RegisterChildResponse)response;
				RegistrationDTO regDto1 = resp.RegistrationDto;
				Registration reg= DTOUtils.getFromDTO(regDto1);
				return reg;
			}

			public Child saveChild(String name, int age)
			{
				Child child=new Child(name,age);
				ChildDTO childDTO=new ChildDTO(name,age.ToString());
				SaveChild req = new SaveChild(childDTO);
				sendRequest(req);
				Response response=readResponse();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}

				SaveChildResponse resp = (SaveChildResponse)response;
				ChildDTO childDTO1 = resp.ChildDto;
				Child child1 = DTOUtils.getFromDTO(childDTO1);
				return child1;
			}

			public Child FindChild(string userName)
			{
				ChildDTO org=new ChildDTO(userName);
				FindChildRequest req = new FindChildRequest(org);
				
				sendRequest(req);
				Response response=readResponse();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}
FindChildResponse resp = (FindChildResponse)response;
				ChildDTO orgDTO = resp.ChildDto;
				if (orgDTO==null)
					return null;
				Child org1= DTOUtils.getFromDTO(orgDTO);
				return null;
			}

			
			public Organizing login(Organizing org,ICompetitionObserver observer){
				initializeConnection();
				OrganizingDTO udto= DTOUtils.getDTO(org);
				LoginRequest req = new LoginRequest(udto);
				sendRequest(req);
				Response response=readResponse();
				if (response is ErrorResponse)
				{

					ErrorResponse err = (ErrorResponse)response;
					//closeConnection();
					throw new CompetitionException(err.Message);
				}else{

				this.client=observer;
				LoginRequestResponse log = (LoginRequestResponse)response;
				return DTOUtils.getFromDTO(log.User);
			}
		
		
			}

		

			public void logout(Organizing user, ICompetitionObserver observer)
			{
				OrganizingDTO udto= DTOUtils.getDTO(user);
				Request req = new LogoutRequest(udto);
				sendRequest(req);
				Response response=readResponse();
				closeConnection();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}
			}

			public int numberOfChildrenForSample(Sample sample)
			{
				SamplesDTO udto = DTOUtils.getDTO(sample);
				NumberOfChildren req = new NumberOfChildren(udto);
				sendRequest(req);
				Response response=readResponse();
				if (response is ErrorResponse)
				{
					ErrorResponse err = (ErrorResponse)response;
					throw new CompetitionException(err.Message);
				}
				NumberOfChildrenResponse numberString=(NumberOfChildrenResponse)response;
				return int.Parse(numberString.Number);
			}
    }
}