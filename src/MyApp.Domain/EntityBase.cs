using System;

namespace MyApp.Domain
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
