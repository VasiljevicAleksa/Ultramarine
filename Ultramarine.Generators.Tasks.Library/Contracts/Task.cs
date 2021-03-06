﻿using Ultramarine.Workspaces;

namespace Ultramarine.Generators.Tasks.Library.Contracts
{
    public abstract class Task : ITask
    {
        protected Task() : this(null)
        {

        }
        protected Task(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public object Input { get; set; }
        public object Output { get; set; }
        public Task Parent { get; set; }
        public string ConnectedWith { get; set; }
        protected virtual ValidationResult Validate()
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Name))
                validationResult.Add(nameof(Name), "Name must not be empty");

            return validationResult;
        }

        protected abstract object OnExecute();

        public IProjectModel ExecutionContext { get; private set; }

        public ILogger Logger { get; private set; }

        public virtual void SetExecutionContext(IProjectModel executionContext)
        {
            ExecutionContext = executionContext;
        }

        public virtual void SetLogger(ILogger logger)
        {
            Logger = logger;
        }

        public virtual void Execute()
        {
            Logger.Info($"Starting {GetType()} execution: {Name}");

            Validate();
            Output = OnExecute();

            Logger.Info($"Ending {GetType()} execution: {Name}");
        }

    }

}
