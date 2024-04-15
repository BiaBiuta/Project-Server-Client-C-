

namespace WindowsFormsApp1.domain.validators
{
    public class OrganizingValidator:IValidator<Organizing>
    {
        public void Validate(Organizing e)
        {
            if (e.Password.Length < 4) {
                throw new ValidationException("the password is to low");
            }    }
    }
}