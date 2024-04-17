using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Domain.domain;
using Servicies;

namespace Networking
{
    public class CompetitionClientObjectWorker:ICompetitionObserver
    {
        private ICompetitionServices server;
        private TcpClient connection;
        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public CompetitionClientObjectWorker(ICompetitionServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream=connection.GetStream();
                formatter = new BinaryFormatter();
                connected=true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public virtual void run()
        {
            while(connected)
            {
                try
                {
	                Console.WriteLine("i enter in CompetitionClient");
                    object request = formatter.Deserialize(stream);
                    object response =handleRequest((Request)request);
                    if (response!=null)
                    {
                        sendResponse((Response) response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
				
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error "+e);
            }
        }
        private Response handleRequest(Request request)
		{
			Response response =null;
			if (request is LoginRequest)
			{
				Console.WriteLine("Login request ...");
				LoginRequest logReq =(LoginRequest)request;
				OrganizingDTO udto =logReq.User;
				Organizing user =DTOUtils.getFromDTO(udto);
				try
				{
					Organizing org;
                    
                       org=server.login(user, this);
                    
					return new LoginRequestResponse(DTOUtils.getDTO(org));
				}
				catch (CompetitionException e)
				{
					connected=false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is LogoutRequest)
			{
				Console.WriteLine("Logout request");
				LogoutRequest logReq =(LogoutRequest)request;
				OrganizingDTO udto =logReq.User;
				Organizing user =DTOUtils.getFromDTO(udto);
				try
				{
                    lock (server)
                    {

                        server.logout(user, this);
                    }
					connected=false;
					return new OkResponse();

				}
				catch (CompetitionException e)
				{
				   return new ErrorResponse(e.Message);
				}
			}
			if (request is NumberOfChildren ){
				Console.WriteLine("GetParticipants Request ...");
				NumberOfChildren req = (NumberOfChildren)request;
				SamplesDTO sdto = req.SamplesDto;
				Sample sample=server.findSample(sdto.AgeCategory,sdto.SampleCategory);
				int number=server.numberOfChildrenForSample(sample);
				return new NumberOfChildrenResponse(number.ToString());
			}
			if (request is FindAllSample)
			{
				Console.WriteLine("SendMessageRequest ...");
				FindAllSample senReq =(FindAllSample)request;
				IEnumerable<Sample> mdto = server.findAllSamle();
				List<SamplesDTO> message =DTOUtils.getDTOSample(mdto);
				return new FindAllSampleResponse(message);
			}
			if (request is FindOrganizing)
			{
				Console.WriteLine("Logout request");
				FindOrganizing findOrganizing =(FindOrganizing)request;
				OrganizingDTO udto =findOrganizing.OrganizingDto;
				Organizing user =DTOUtils.getFromDTO(udto);
				try
				{
					Organizing org=server.FindOrganizing(user.Username,user.Password);
					OrganizingDTO orgFind = DTOUtils.getDTO(org);
					return new FindOrganizingResponse(orgFind);

				}
				catch (CompetitionException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			if (request is FindChildRequest)
			{
				Console.WriteLine("Logout request");
				FindChildRequest findOrganizing =(FindChildRequest)request;
				ChildDTO udto =findOrganizing.ChildDto;
				Child user =DTOUtils.getFromDTO(udto);
				try
				{
					Child ch=server.FindChild(user.Name);
					// Console.WriteLine(ch.Id);
					// Console.WriteLine(ch.Name);
					ChildDTO chDto = DTOUtils.getDTO(ch);
					return new FindChildResponse(chDto);

				}
				catch (CompetitionException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			if (request is SaveChild)
			{
				Console.WriteLine("Logout request");
				SaveChild findOrganizing =(SaveChild)request;
				ChildDTO udto =findOrganizing.ChildDto;
				Child user =DTOUtils.getFromDTO(udto);
				try
				{
					Child ch=server.saveChild(user.Name,user.Age);
					ChildDTO chDto = DTOUtils.getDTO(ch);
					return new SaveChildResponse(chDto);

				}
				catch (CompetitionException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			if (request is FindSample)
			{
				Console.WriteLine("Logout request");
				FindSample findOrganizing =(FindSample)request;
				SamplesDTO udto =findOrganizing.SamplesDto;
				Sample user =DTOUtils.getFromDTO(udto);
				try
				{
					Sample spl=server.findSample(udto.AgeCategory,udto.SampleCategory);
					SamplesDTO splDto = DTOUtils.getDTO(spl);
					return new FindSampleResponse(splDto);

				}
				catch (CompetitionException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			if (request is RegisterChild)
			{
				Console.WriteLine("Logout request");
				RegisterChild findOrganizing =(RegisterChild)request;
				RegistrationDTO udto =findOrganizing.RegistrationDto;
				Registration user =DTOUtils.getFromDTO(udto);
				try
				{
					Registration spl=server.registerChild(user.Child,user.Sample);
					RegistrationDTO splDto = DTOUtils.getDTO(spl);
					return new RegisterChildResponse(splDto);

				}
				catch (CompetitionException e)
				{
					return new ErrorResponse(e.Message);
				}
			}


			if (request is ListChildrenForSample)
			{
				Console.WriteLine("GetLoggedFriends Request ...");
				ListChildrenForSample getReq =(ListChildrenForSample)request;
				SamplesDTO udto =getReq.ListChildren;
				Sample sample = server.findSample(udto.AgeCategory, udto.SampleCategory);
				List<Child> participants = server.listChildrenForSample(sample);
				List<ChildDTO> children = DTOUtils.getDTOChild(participants);
				return new ListChildrenForSampleResponse(children);
			}
			if (request is New)
			{
				Console.WriteLine("GetLoggedFriends Request ...");
				New getReq =(New)request;
				RegistrationDTO udto =getReq.RegistrationDto;
				// Sample sample = server.findSample(udto.AgeCategory, udto.SampleCategory);
				// List<Child> participants = server.listChildrenForSample(sample);
				// List<ChildDTO> children = DTOUtils.getDTOChild(participants);
				return new NewResponse(udto);
			}
			return response;
		}
		private void sendResponse(Response response)
		{
			Console.WriteLine("sending response "+response);
			lock (stream)
			{
				formatter.Serialize(stream, response);
				stream.Flush();
			}

		}

		public virtual void participantsRegistered(Registration org)
		{
			Console.WriteLine("Participants registered "+org);
			RegistrationDTO orgDTO= DTOUtils.getDTO(org);
			Response response = new NewResponse(orgDTO);
			try {
				sendResponse(response);
			} catch (IOException e) {
				throw new CompetitionException("Sending error: "+e);
			}
		}
    }

    
}
