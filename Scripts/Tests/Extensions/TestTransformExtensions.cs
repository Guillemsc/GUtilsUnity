using NUnit.Framework;
using UnityEngine;

namespace GUtilsUnity.Extensions.Tests
{
    public class TestTransformExtensions
    {
        [Test]
        public void SetAsNextSibling_OnRootTransform_MovesNext()
        {
            var gameObject = new GameObject();
            var gameObject2 = new GameObject();

            var nextIndex = gameObject2.transform.GetSiblingIndex();
            gameObject.transform.SetAsNextSibling();

            Assert.That(gameObject.transform.GetSiblingIndex(), Is.EqualTo(nextIndex));
        }

        [Test]
        public void SetAsNextSibling_OnTransformAsLastSibling_DoesNothing()
        {
            var gameObject = new GameObject();

            var initialIndex = gameObject.transform.GetSiblingIndex();
            gameObject.transform.SetAsNextSibling();

            Assert.That(gameObject.transform.GetSiblingIndex(), Is.EqualTo(initialIndex));
        }

        [Test]
        public void SetAsPreviousSibling_OnRootTransform_MovesPrevious()
        {
            var gameObject = new GameObject();
            var gameObject2 = new GameObject();

            var nextIndex = gameObject.transform.GetSiblingIndex();
            gameObject2.transform.SetAsPreviousSibling();

            Assert.That(gameObject2.transform.GetSiblingIndex(), Is.EqualTo(nextIndex));
        }

        [Test]
        public void GetActiveChildIndex_OnTransformWithNoChildren_IsZero()
        {
            var parent = new GameObject();
            var transform = parent.transform;

            new GameObject().SetParent(parent);

            Assert.That(transform.GetActiveChildIndex(0), Is.EqualTo(0));
            Assert.That(transform.GetActiveChildIndex(1), Is.EqualTo(1));
        }

        [Test]
        public void GetActiveChildIndex_OnTransformWithAllSiblingsActive_IsSame()
        {
            var parent = new GameObject();
            var transform = parent.transform;

            new GameObject().SetParent(parent);
            new GameObject().SetParent(parent);

            Assert.That(transform.GetActiveChildIndex(0), Is.EqualTo(0));
            Assert.That(transform.GetActiveChildIndex(1), Is.EqualTo(1));
            Assert.That(transform.GetActiveChildIndex(2), Is.EqualTo(2));
            Assert.That(transform.GetActiveChildIndex(3), Is.EqualTo(2));
        }

        [Test]
        public void GetActiveChildIndex_OnTransformWithFirstDisabled_ConsidersProper()
        {
            var parent = new GameObject();
            var transform = parent.transform;

            var child1 = new GameObject();
            child1.SetParent(parent);
            child1.SetActive(false);

            new GameObject().SetParent(parent);
            new GameObject().SetParent(parent);

            Assert.That(transform.GetActiveChildIndex(0), Is.EqualTo(0));
            Assert.That(transform.GetActiveChildIndex(1), Is.EqualTo(2));
            Assert.That(transform.GetActiveChildIndex(2), Is.EqualTo(3));
            Assert.That(transform.GetActiveChildIndex(3), Is.EqualTo(3));
        }

        [Test]
        public void GetActiveChildIndex_OnTransformWithLastDisabled_ConsidersProper()
        {
            var parent = new GameObject();
            var transform = parent.transform;

            new GameObject().SetParent(parent);
            new GameObject().SetParent(parent);

            var child2 = new GameObject();
            child2.SetParent(parent);
            child2.SetActive(false);

            Assert.That(transform.GetActiveChildIndex(0), Is.EqualTo(0));
            Assert.That(transform.GetActiveChildIndex(1), Is.EqualTo(1));
            Assert.That(transform.GetActiveChildIndex(2), Is.EqualTo(2));
            Assert.That(transform.GetActiveChildIndex(3), Is.EqualTo(3));
        }
    }
}
