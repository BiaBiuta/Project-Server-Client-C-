



namespace Domain.domain.validators
{
    public interface IValidator<E>
    {
        void Validate(E e);
    }
}