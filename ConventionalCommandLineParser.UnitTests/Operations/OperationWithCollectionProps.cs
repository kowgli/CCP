using CCP;
using CCP.UnitTests.Types;
using System.Collections.Generic;

namespace CCP.UnitTests.Operations
{
    public class OperationWithCollectionProps : IOperation
    {
        public List<string> StringList { get; set; }

        public List<int> IntList { get; set; }

        public IList<string> StringIList { get; set; }

        public IEnumerable<int> IntIEnumerable { get; set; }

        public ICollection<string> StringICollection { get; set; }

        public IReadOnlyList<int> IntIReadOnlyList { get; set; }

        public List<SampleComplexType> ComplexList { get; set; }

        public Dictionary<string, int> StringIntDictionary { get; set; }

        public void Run()
        {

        }
    }
}
