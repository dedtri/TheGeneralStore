using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGeneralStore.Frontend.ConsoleClient;

namespace TheGeneralStore.Backend.Test
{
    public class MathTest
    {
        
        [Fact]
        public void additionIsValid()
        {
            // Arrange - Assign variables / objects etc.
            int t1 = 2, t2 = 2, sum = t1 + t2;

            Maths m = new Maths();

            // Act - Invoke methods
            var result = m.add(t1, t2);

            // Assert - Verify we got the right result
            Assert.Equal(sum, result);
        }

        [Fact]
        public void additionIsNotValid()
        {
            // Arrange - Assign variables / objects etc.
            int t1 = 2, t2 = 2, sum = t1 + t2;

            Maths m = new Maths();

            // Act - Invoke methods
            var result = m.add(t1, t2);

            // Assert - Verify we got the right result
            Assert.Equal(33, result);
        }

        [Fact]
        public void multiplyIsValid()
        {
            // Arrange - Assign variables / objects etc.
            int t1 = 2, t2 = 2;

            Maths m = new Maths();

            // Act - Invoke methods
            var result = m.multiply(t1, t2);

            // Assert - Verify we got the right result
            Assert.False(t1 * t2 != result);
        }

        [Fact]
        public void subtractionIsValid()
        {
            // Arrange - Assign variables / objects etc.
            int t1 = 2, t2 = 2;

            Maths m = new Maths();

            // Act - Invoke methods
            var result = m.subtraction(t1, t2);

            // Assert - Verify we got the right result
            Assert.True(result == t1-t2);
        }

    }
}
