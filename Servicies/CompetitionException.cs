using System;

namespace Servicies
{
    public class CompetitionException:Exception
    {
        
        public CompetitionException():base() { }

        public CompetitionException(String msg) : base(msg) { }

        public CompetitionException(String msg, Exception ex) : base(msg, ex) { }

    
    }
}