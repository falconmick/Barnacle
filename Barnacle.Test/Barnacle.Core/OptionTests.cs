using NUnit.Framework;

namespace Barnacle.Test.Barnacle.Core
{
    public class OptionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnValueFromSomeWhenSomeResult()
        {
            const int expected = 5;

            int result = SomeInt(expected) switch
            {
                Some<int>({ } intValue) => intValue,
                None<int> => -1,
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturnValueFromNoneWhenNoneResult()
        {
            const int expected = 5;

            int result = NoneInt() switch
            {
                Some<int>({ } intValue) => intValue,
                None<int> => expected,
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturnValueFromDiscardWhenNoneResultAndNoNoneArm()
        {
            const int expected = 5;

            int result = NoneInt() switch
            {
                Some<int>({ } intValue) => intValue,
                _ => expected,
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        Option<int> SomeInt(int echo)
        {
            return new Some<int>(echo);
        }

        Option<int> NoneInt()
        {
            return new None<int>();
        }
    }

    public class Option<T>
    {
    }

    public class None<T> : Option<T>
    {
    }

    public class Some<T> : Option<T>
    {
        public Some(T val)
        {
            Value = val;
        }
        public T Value { get; }

        public void Deconstruct(out T val)
        {
            val = Value;
        }
    }
}