using System;
using FishMarket.WebApi;
using FishMarket.WebApi.Data.Interfaces;
using FishMarket.WebApi.Data.Contexts.Entities;
using FishMarket.WebApi.Controllers;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FishMarket.WebApi.Tests
{
    public class FishDefinitionUnitTest
    {
        [Fact]
        public void TestListAll()
        {
            // arrange

            bool searchwasCalled = false;
            Mock<IFishDefinitionRepository> repositoryMock = new Mock<IFishDefinitionRepository>();
            repositoryMock.Setup(r => r.ListAll())
                .Returns(() =>
                {
                    return new List<FishDefinitionListModel>
                    {
                        new FishDefinitionListModel { Id = 1, Name = "Balýk1", Price = 12 },
                        new FishDefinitionListModel { Id = 2, Name = "Balýk2", Price = 22 },
                        new FishDefinitionListModel { Id = 3, Name = "Balýk3", Price = 32 },
                        new FishDefinitionListModel { Id = 4, Name = "Balýk4", Price = 42.5m }
                    };
                })
                .Callback(() => searchwasCalled = true);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.FishDefinitionRepository).Returns(repositoryMock.Object);

            var controller = new FishDefinitionV1Controller(mockUnitOfWork.Object);

            // act
            var data = controller.ListAll();

            // assert
            Assert.True(searchwasCalled, "Search was not called.");
            Assert.True(data.Count() == 4);
        }

        [Fact]
        public void TestGet()
        {
            // arrange

            bool searchwasCalled = false;
            Mock<IFishDefinitionRepository> repositoryMock = new Mock<IFishDefinitionRepository>();
            repositoryMock.Setup(r => r.FindOne(15))
                .Returns(() =>
                {
                    return new FishDefinition { Id = 15, Name = "Balýk1", Price = 12 };
                })
                .Callback(() => searchwasCalled = true);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.FishDefinitionRepository).Returns(repositoryMock.Object);

            var controller = new FishDefinitionV1Controller(mockUnitOfWork.Object);

            // act
            var data = controller.Get(15);

            // assert
            Assert.True(searchwasCalled, "Search was not called.");
            Assert.True(data != null);
            Assert.True(data.Id == 15);
        }

        [Fact]
        public void TestAdd()
        {
            var data = new List<FishDefinitionListModel>
            {
                new FishDefinitionListModel { Id = 1, Name = "Balýk1", Price = 12 },
                new FishDefinitionListModel { Id = 2, Name = "Balýk2", Price = 22 },
                new FishDefinitionListModel { Id = 3, Name = "Balýk3", Price = 32 },
                new FishDefinitionListModel { Id = 4, Name = "Balýk4", Price = 42.5m }
            };

            // arrange
            Mock<IFishDefinitionRepository> repositoryMock = new Mock<IFishDefinitionRepository>();
            repositoryMock.Setup(r => r.Add(It.IsAny<FishDefinition>()))
                .Callback<FishDefinition>(item => data.Add(new FishDefinitionListModel { Id = item.Id, Name = item.Name, Price = item.Price }));

            repositoryMock.Setup(r => r.ListAll())
                .Returns(() =>
                {
                    return data;
                });


            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.FishDefinitionRepository).Returns(repositoryMock.Object);

            var controller = new FishDefinitionV1Controller(mockUnitOfWork.Object);

            // act
            var fish = new FishDefinition { Id = 5, Name = "Balýk5", Price = 123 };
            controller.Add(fish);
            var result = controller.ListAll();

            // assert
            Assert.True(result != null);
            Assert.True(result.Count() == 5);
            Assert.True(result.Any(x => x.Id == 5));
        }

        [Fact]
        public void TestUpdatePrice()
        {
            var data = new List<FishDefinitionListModel>
            {
                new FishDefinitionListModel { Id = 1, Name = "Balýk1", Price = 12 },
                new FishDefinitionListModel { Id = 2, Name = "Balýk2", Price = 22 },
                new FishDefinitionListModel { Id = 3, Name = "Balýk3", Price = 32 },
                new FishDefinitionListModel { Id = 4, Name = "Balýk4", Price = 42.5m }
            };

            // arrange
            Mock<IFishDefinitionRepository> repositoryMock = new Mock<IFishDefinitionRepository>();
            repositoryMock.Setup(r => r.UpdatePrice(It.IsAny<int>(), It.IsAny<decimal>()))
                .Callback<int, decimal>((id, price) =>
                {
                    var found = data.FirstOrDefault(p => p.Id == id);
                    if (found != null)
                    {
                        found.Price = price;
                    }
                });

            repositoryMock.Setup(r => r.ListAll())
                .Returns(() =>
                {
                    return data;
                });


            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.FishDefinitionRepository).Returns(repositoryMock.Object);

            var controller = new FishDefinitionV1Controller(mockUnitOfWork.Object);

            // act
            var fish = new FishDefinition { Id = 5, Name = "Balýk5", Price = 123 };
            controller.UpdatePrice(1, 44);
            var result = controller.ListAll();

            // assert
            Assert.True(result != null);
            Assert.True(result.Count() == 4);
            Assert.True(result.Any(x => x.Id == 1));
            Assert.True(result.FirstOrDefault(x => x.Id == 1).Price == 44);
        }

        [Fact]
        public void TestUpdate()
        {
            var data = new List<FishDefinitionListModel>
            {
                new FishDefinitionListModel { Id = 1, Name = "Balýk1", Price = 12 },
                new FishDefinitionListModel { Id = 2, Name = "Balýk2", Price = 22 },
                new FishDefinitionListModel { Id = 3, Name = "Balýk3", Price = 32 },
                new FishDefinitionListModel { Id = 4, Name = "Balýk4", Price = 42.5m }
            };

            // arrange
            Mock<IFishDefinitionRepository> repositoryMock = new Mock<IFishDefinitionRepository>();
            repositoryMock.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<FishDefinition>()))
                .Callback<int, FishDefinition>((id, fishDef) =>
                {
                    var found = data.FirstOrDefault(p => p.Id == id);
                    if (found != null)
                    {
                        found.Name = fishDef.Name;
                        found.Price = fishDef.Price;
                    }
                });

            repositoryMock.Setup(r => r.ListAll())
                .Returns(() =>
                {
                    return data;
                });


            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.FishDefinitionRepository).Returns(repositoryMock.Object);

            var controller = new FishDefinitionV1Controller(mockUnitOfWork.Object);

            // act
            var fish = new FishDefinition { Id = 1, Name = "Balýk Test", Price = 33 };
            controller.Update(1, fish);
            var result = controller.ListAll();

            // assert
            Assert.True(result != null);
            Assert.True(result.Count() == 4);
            Assert.True(result.Any(x => x.Id == 1));
            Assert.True(result.FirstOrDefault(x => x.Id == 1).Price == 33);
            Assert.True(result.FirstOrDefault(x => x.Id == 1).Name == "Balýk Test");
        }

        [Fact]
        public void TestDelete()
        {
            var data = new List<FishDefinitionListModel>
            {
                new FishDefinitionListModel { Id = 1, Name = "Balýk1", Price = 12 },
                new FishDefinitionListModel { Id = 2, Name = "Balýk2", Price = 22 },
                new FishDefinitionListModel { Id = 3, Name = "Balýk3", Price = 32 },
                new FishDefinitionListModel { Id = 4, Name = "Balýk4", Price = 42.5m }
            };

            // arrange
            Mock<IFishDefinitionRepository> repositoryMock = new Mock<IFishDefinitionRepository>();
            repositoryMock.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback<int>((id) =>
                {
                    var found = data.FirstOrDefault(p => p.Id == id);
                    if (found != null)
                    {
                        data.Remove(found);
                    }
                });

            repositoryMock.Setup(r => r.ListAll())
                .Returns(() =>
                {
                    return data;
                });


            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.FishDefinitionRepository).Returns(repositoryMock.Object);

            var controller = new FishDefinitionV1Controller(mockUnitOfWork.Object);

            // act
            controller.Delete(1);
            var result = controller.ListAll();

            // assert
            Assert.True(result != null);
            Assert.True(result.Count() == 3);
            Assert.True(!result.Any(x => x.Id == 1));
        }
    }
}
