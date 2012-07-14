using JetBrains.Annotations;

// ReSharper disable CheckNamespace
namespace System.Diagnostics.Contracts
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Enables factoring legacy if-then-throw into separate methods for reuse and full control over
    /// thrown exception and arguments
    /// </summary>
    [UsedImplicitly]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [Conditional("CONTRACTS_FULL")]
    internal sealed class ContractArgumentValidatorAttribute : Attribute
    {
    }

    /// <summary>
    /// Enables writing abbreviations for contracts that get copied to other methods
    /// </summary>
    [UsedImplicitly]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [Conditional("CONTRACTS_FULL")]
    internal sealed class ContractAbbreviatorAttribute : Attribute
    {
    }

    /// <summary>
    /// Allows setting contract and tool options at assembly, type, or method granularity.
    /// </summary>
    [UsedImplicitly]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    [Conditional("CONTRACTS_FULL")]
    internal sealed class ContractOptionAttribute : Attribute
    {
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toggle", Justification = "Build-time only attribute")]
        public ContractOptionAttribute(string category, string setting, bool toggle)
        {
        }

        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Build-time only attribute")]
        public ContractOptionAttribute(string category, string setting, string value)
        {
        }
    }
} ;