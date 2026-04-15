using Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MinHeapTests
{
    [TestMethod]
    public void Insert_ShouldMaintainMinProperty()
    {
        // Arrange
        var heap = new MinHeap<string>();

        // Act
        heap.Insert("Low Priority", 10);
        heap.Insert("High Priority", 1);
        heap.Insert("Medium Priority", 5);

        // Assert
        Assert.AreEqual("High Priority", heap.Extreme());
    }
}