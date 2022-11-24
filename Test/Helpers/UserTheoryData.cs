using DTO.Incoming;
using System.Collections;

namespace Test.Helpers
{
    public class UserTheoryData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
              new UserInsertDto
              {
                Name = "Integration test",
                Email = "integrationTests@gmail.com",
                Password = "0000",
                Roles = new List<long> { 1 }
              }
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

