using Autofac;
using FluentValidation;
using System;

namespace DT.Core.Application.Validation
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext _context;

        public AutofacValidatorFactory(IComponentContext context)
        {
            _context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object instance;
            if (_context.TryResolve(validatorType, out instance))
            {
                IValidator validator = instance as IValidator;
                return validator;
            }

            return null;
        }
    }
}
