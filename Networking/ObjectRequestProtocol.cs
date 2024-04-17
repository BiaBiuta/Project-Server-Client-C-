using System;

namespace Networking
{


        public interface Request 
        {
        }


        [Serializable]
        public class LoginRequest : Request
        {
            private OrganizingDTO user;

            public LoginRequest(OrganizingDTO user)
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
        public class LogoutRequest : Request
        {
            private OrganizingDTO user;

            public LogoutRequest(OrganizingDTO user)
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
        public class FindChildRequest : Request
        {
            private ChildDTO _childDto;

            public FindChildRequest(ChildDTO childDto)
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
        public class SaveChild : Request
        {
            private ChildDTO _childDto;

            public SaveChild(ChildDTO childDto)
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
        public class FindOrganizing : Request
        {
            private OrganizingDTO _orgDto;

            public FindOrganizing(OrganizingDTO orgDto)
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
        public class FindSample : Request
        {
            private SamplesDTO _samplesDto;

            public FindSample(SamplesDTO orgDto)
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
        public class RegisterChild : Request
        {
            private RegistrationDTO _registrationDto;

            public RegisterChild(RegistrationDTO orgDto)
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
        public class New : Request
        {
            private RegistrationDTO _samplesDto;

            public New(RegistrationDTO orgDto)
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
        public class ListChildrenForSample : Request
        {
            private SamplesDTO _samplesDto;

            public ListChildrenForSample(SamplesDTO samplesDto)
            {
                this._samplesDto = samplesDto;
            }

            public virtual SamplesDTO ListChildren
            {
                get
                {
                    return _samplesDto;
                }
            }
        }
        
        [Serializable]
        public class NumberOfChildren : Request
        {
            private SamplesDTO _samplesDto;

            public NumberOfChildren(SamplesDTO samplesDto)
            {
                this._samplesDto = samplesDto;
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
        public class FindAllSample : Request
        {
            

            public FindAllSample()
            {
            }
        }
    
}