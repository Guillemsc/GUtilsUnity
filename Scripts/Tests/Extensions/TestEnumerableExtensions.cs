using System.Collections.Generic;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    public class TestEnumerableExtensions
    {
        [Test]
        public void MinObject_InListOfObjectsUsingIndexAsValue_IsTheFirstInTheList()
        {
            var objects = new List<string>()
            {
                "first",
                "second",
                "third"
            };

            var minObject = objects.MinObjectOrDefault(objects.IndexOf, Comparer<int>.Default);
            Assert.That(minObject, Is.EqualTo(objects[0]));
        }

        [Test]
        public void GetDifference_WithNoChanges_ReturnsEmptyLists()
        {
            // Arrange
            var oldEnumerable = new List<int> { 1, 2, 3 };
            var newEnumerable = new List<int> { 1, 2, 3 };

            // Act
            oldEnumerable.GetDifference(newEnumerable, out var addedElements, out var removedElements);

            // Assert
            Assert.That(addedElements, Is.Empty);
            Assert.That(removedElements, Is.Empty);
        }

        [Test]
        public void GetDifference_WithAddedElements_ReturnsCorrectLists()
        {
            // Arrange
            var oldEnumerable = new List<int> { 1, 2, 3 };
            var newEnumerable = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            oldEnumerable.GetDifference(newEnumerable, out var addedElements, out var removedElements);

            // Assert
            Assert.That(addedElements, Is.EquivalentTo(new[] { 4, 5 }));
            Assert.That(removedElements, Is.Empty);
        }

        [Test]
        public void GetDifference_WithRemovedElements_ReturnsCorrectLists()
        {
            // Arrange
            var oldEnumerable = new List<int> { 1, 2, 3, 4, 5 };
            var newEnumerable = new List<int> { 1, 2, 3 };

            // Act
            oldEnumerable.GetDifference(newEnumerable, out var addedElements, out var removedElements);

            // Assert
            Assert.That(addedElements, Is.Empty);
            Assert.That(removedElements, Is.EquivalentTo(new[] { 4, 5 }));
        }

        [Test]
        public void GetDifference_WithChangedElements_ReturnsCorrectLists()
        {
            // Arrange
            var oldEnumerable = new List<int> { 1, 2, 3, 4 };
            var newEnumerable = new List<int> { 1, 2, 5, 6 };

            // Act
            oldEnumerable.GetDifference(newEnumerable, out var addedElements, out var removedElements);

            // Assert
            Assert.That(addedElements, Is.EquivalentTo(new[] { 5, 6 }));
            Assert.That(removedElements, Is.EquivalentTo(new[] { 3, 4 }));
        }

        [Test]
        public void HasMoreThanOrEqual_ShouldReturnTrue_WhenCountIsMet()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasMoreThanOrEqual(3);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasMoreThanOrEqual_ShouldReturnFalse_WhenCountIsNotMet()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasMoreThanOrEqual(6);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasMoreThanOrEqual_WithPredicate_ShouldReturnTrue_WhenCountIsMet()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasMoreThanOrEqual(3, x => x.IsOdd());

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasMoreThanOrEqual_WithPredicate_ShouldReturnFalse_WhenCountIsNotMet()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasMoreThanOrEqual(3, x => x.IsEven());

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasLessThanOrEqual_ShouldReturnTrue_WhenCountIsMet()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasLessThanOrEqual(5);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasLessThanOrEqual_ShouldReturnFalse_WhenCountIsExceeded()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasLessThanOrEqual(3);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasLessThanOrEqual_WithPredicate_ShouldReturnTrue_WhenCountIsMet()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasLessThanOrEqual(4, x => x.IsEven());

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasLessThanOrEqual_WithPredicate_ShouldReturnFalse_WhenCountIsExceeded()
        {
            // Arrange
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            bool result = numbers.HasLessThanOrEqual(2, x => x.IsOdd());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
