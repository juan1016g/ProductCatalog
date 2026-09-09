using Domain.Entities;

namespace Domain.Tests.Entities
{
    public class AuditableEntityTests
    {
        private class TestEntity : AuditableEntity { }

        [Fact]
        public void AuditableEntity_ShouldSetAuditProperties()
        {
            // Arrange
            var entity = new TestEntity();
            var now = DateTime.UtcNow;

            // Act
            entity.CreatedBy = "Juan Pablo";
            entity.CreatedAt = now;
            entity.UpdatedBy = "Juan Pablo";
            entity.UpdatedAt = now;

            // Assert
            Assert.Equal("Juan Pablo", entity.CreatedBy);
            Assert.Equal(now, entity.CreatedAt);
            Assert.Equal("Juan Pablo", entity.UpdatedBy);
            Assert.Equal(now, entity.UpdatedAt);
        }
    }
}
