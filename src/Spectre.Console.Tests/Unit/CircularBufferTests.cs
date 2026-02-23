namespace Spectre.Console.Tests.Unit;

public sealed class CircularBufferTests
{
    [Fact]
    public void CircularBuffer_Should_Respect_Capacity()
    {
        // Given
        var buffer = new CircularBuffer<int>(3);

        // When
        buffer.Add(1);
        buffer.Add(2);
        buffer.Add(3);
        buffer.Add(4);

        // Then
        buffer.Count.ShouldBe(3);
        buffer.ToArray().ShouldBe(new[] { 2, 3, 4 });
    }

    [Fact]
    public void CircularBuffer_Should_Handle_UniqueRemovedCheck()
    {
        // Given
        var buffer = new CircularBuffer<string>(2)
        {
            UniqueRemovedCheck = true
        };

        // When
        buffer.Add("a");
        buffer.Add("b");
        buffer.Add("c"); // "a" is pushed out

        // Then
        buffer.Count.ShouldBe(2);
        buffer.ToArray().ShouldBe(new[] { "b", "c" });
    }

    [Fact]
    public void CircularBuffer_Should_Not_Throw_On_Duplicates_When_Check_Is_False()
    {
        // Given
        var buffer = new CircularBuffer<string>(2)
        {
            UniqueRemovedCheck = false
        };

        // When
        buffer.Add("a");
        buffer.Add("a");
        buffer.Add("b");

        // Then
        buffer.Count.ShouldBe(2);
        buffer.ToArray().ShouldBe(new[] { "a", "b" });
    }
}
