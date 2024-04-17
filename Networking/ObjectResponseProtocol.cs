using System;
using System.Collections.Generic;

namespace Networking
{
    public interface Response 
    {
    }

    [Serializable]
    public class OkResponse : Response
    {
		
    }
   
    public interface UpdateResponse : Response
    {
    }

    [Serializable]
    public class ErrorResponse : Response
    {
        private string message;

        public ErrorResponse(string message)
        {
            this.message = message;
        }

        public virtual string Message
        {
            get
            {
                return message;
            }
        }
    }

    [Serializable]
        public class LoginRequestResponse : Response
        {
            private OrganizingDTO user;

            public LoginRequestResponse(OrganizingDTO user)
            {
                this.user = user;
            }

            public virtual OrganizingDTO User
            {
                get
                {
                    return user;
                }
            }
        }

        [Serializable]
        public class LogoutRequestResponse : Response
        {
            private OrganizingDTO user;

            public LogoutRequestResponse(OrganizingDTO user)
            {
                this.user = user;
            }

            public virtual OrganizingDTO User
            {
                get
                {
                    return user;
                }
            }
        }

        [Serializable]
        public class FindChildResponse : Response
        {
            private ChildDTO _childDto;

            public FindChildResponse(ChildDTO childDto)
            {
                this._childDto = childDto;
            }

            public virtual ChildDTO ChildDto
            {
                get
                {
                    return _childDto;
                }
            }
        }
        [Serializable]
        public class SaveChildResponse : Response
        {
            private ChildDTO _childDto;

            public SaveChildResponse(ChildDTO childDto)
            {
                this._childDto = childDto;
            }

            public virtual ChildDTO ChildDto
            {
                get
                {
                    return _childDto;
                }
            }
        }
        [Serializable]
        public class FindOrganizingResponse : Response
        {
            private OrganizingDTO _orgDto;

            public FindOrganizingResponse(OrganizingDTO orgDto)
            {
                this._orgDto = orgDto;
            }

            public virtual OrganizingDTO OrganizingDto
            {
                get
                {
                    return _orgDto;
                }
            }
        }
        [Serializable]
        public class FindSampleResponse : Response
        {
            private SamplesDTO _samplesDto;

            public FindSampleResponse(SamplesDTO orgDto)
            {
                this._samplesDto = orgDto;
            }

            public virtual SamplesDTO SamplesDto
            {
                get
                {
                    return _samplesDto;
                }
            }
        }
        [Serializable]
        public class RegisterChildResponse : Response
        {
            private RegistrationDTO _registrationDto;

            public RegisterChildResponse(RegistrationDTO orgDto)
            {
                this._registrationDto = orgDto;
            }

            public virtual RegistrationDTO RegistrationDto
            {
                get
                {
                    return _registrationDto;
                }
            }
        }
        [Serializable]
        public class NewResponse : UpdateResponse
        {
            private RegistrationDTO _samplesDto;
        
            public NewResponse(RegistrationDTO orgDto)
            {
                this._samplesDto = orgDto;
            }
        
            public virtual RegistrationDTO RegistrationDto
            {
                get
                {
                    return _samplesDto;
                }
            }
        }
        [Serializable]
        public class ListChildrenForSampleResponse : Response
        {
            private List<ChildDTO> _samplesDto;

            public ListChildrenForSampleResponse(List<ChildDTO> samplesDto)
            {
                this._samplesDto = samplesDto;
            }

            public virtual List<ChildDTO> ListChildren
            {
                get
                {
                    return _samplesDto;
                }
            }
        }
        
        

        [Serializable]
        public class FindAllSampleResponse : Response
        {
            private List<SamplesDTO> samples;

            public FindAllSampleResponse(List<SamplesDTO> samples)
            {
                this.samples = samples;
            }
            public virtual List<SamplesDTO> ListSamples
            {
                get
                {
                    return samples;
                }
            }
            
        }
        [Serializable]
        public class NumberOfChildrenResponse : Response
        {
            private String number;
            public NumberOfChildrenResponse(string toString)
            {
                number = toString;
            }
            public virtual String Number
            {
                get
                {
                    return number;
                }
            }
        }
}