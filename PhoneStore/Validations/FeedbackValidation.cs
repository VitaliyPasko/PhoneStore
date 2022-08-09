using FluentValidation;
using PhoneStore.ViewModels.Feedback;

namespace PhoneStore.Validations
{
    public class FeedbackValidation : AbstractValidator<FeedbackCreateViewModel>
    {
        public FeedbackValidation()
        {
            RuleFor(f => f.Text).NotEmpty().MinimumLength(10);
            RuleFor(f => f.PhoneId).NotEmpty().NotEqual(0);
        }
    }
}