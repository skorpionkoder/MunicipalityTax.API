using System.Drawing;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MunicipalityTax.API.Controllers;
using MunicipalityTax.API.Data.Entities;
using MunicipalityTax.API.Data.Interfaces;
using MunicipalityTax.API.Models;

namespace MunicipalityTax.API.Tests
{
    public class MunicipalityTaxControllerTests
    {
        private readonly Mock<IMunicipalityRepository> municipalityService;
        private readonly Mock<ITaxTypeRepository> taxTypeService;
        private readonly Mock<IMunicipalityTaxRepository> municipalityTaxService;
        private readonly Mock<ILogger<MunicipalityTaxController>> mockLogger;

        public MunicipalityTaxControllerTests()
        {
            municipalityService = new Mock<IMunicipalityRepository>();
            taxTypeService = new Mock<ITaxTypeRepository>();
            municipalityTaxService = new Mock<IMunicipalityTaxRepository>();
            mockLogger = new Mock<ILogger<MunicipalityTaxController>>();
        }
        [Fact]
        public async Task GetMunicipalityTaxTest()
        {
            //arrange
            var municipalityList = GetMunicipalityData();
            var municipalityTaxList = GetMunicipalityTaxData();

            municipalityService.Setup(x => x.GetMunicipalityAsync("Copenhagen"))
                .ReturnsAsync(municipalityList[0]);
            municipalityTaxService.Setup(x => x.GetMunicipalityTaxesAsync(municipalityTaxList[0].MunicipalityId, new DateTime(2020, 01, 01)))
                .ReturnsAsync(municipalityTaxList);

            var municipalityTaxController = new MunicipalityTaxController(municipalityTaxService.Object, municipalityService.Object, mockLogger.Object);

            //act
            var municipalityTaxResult = await municipalityTaxController.GetMunicipalityTax("Copenhagen", new DateTime(2020, 01, 01));

            //assert
            var okResult = Assert.IsType<OkObjectResult>(municipalityTaxResult);
            Assert.NotNull(municipalityTaxResult);
            Assert.Equal(0.1M, okResult.Value);
        }

        [Theory]
        [InlineData("01/01/2020", "yearly")]
        [InlineData("05/01/2020", "monthly")]
        [InlineData("01/01/2020", "daily")]
        public async Task CreateMunicipalityTaxTest(DateTime startDtm, string taxType)
        {
            MunicipalityTaxModel model = new MunicipalityTaxModel
            {
                MunicipalityName = "Aarhus",
                StartDtm = startDtm,
                TaxType = taxType,
                TaxAmount = 0.3M
            };
            municipalityTaxService.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(true);

            var municipalityTaxController = new MunicipalityTaxController(municipalityTaxService.Object, municipalityService.Object, mockLogger.Object);
            var municipalityTaxResult = await municipalityTaxController.Post(model);

            Assert.IsType<ActionResult<MunicipalityTaxModel>>(municipalityTaxResult);
            Assert.NotNull(municipalityTaxResult);
        }

        [Fact]
        public async Task UpdateMunicipalityTaxTest()
        {
            MunicipalityTaxModel model = new MunicipalityTaxModel
            {
                MunicipalityName = "Aarhus",
                StartDtm = DateTime.Now,
                TaxType = "monthly",
                TaxAmount = 0.4M
            };
            //municipalityTaxService.Setup(x => x.SaveChangesAsync())
            //    .ReturnsAsync(true);

            var municipalityTaxController = new MunicipalityTaxController(municipalityTaxService.Object, municipalityService.Object, mockLogger.Object);
            var municipalityTaxResult = await municipalityTaxController.PutMunicipalityTax(1, model);

            Assert.IsType<ActionResult<MunicipalityTaxModel>>(municipalityTaxResult);
            Assert.NotNull(municipalityTaxResult);
        }

        private List<Data.Entities.MunicipalityTax> GetMunicipalityTaxData()
        {
            List<Data.Entities.MunicipalityTax> municipalityTaxData = new List<Data.Entities.MunicipalityTax>
            {
                new Data.Entities.MunicipalityTax
                {
                    MunicipalityTaxId = 1,
                    MunicipalityId = 1,
                    TaxTypeId = 1,
                    TaxType = new TaxType
                    {
                        TaxTypeId = 1,
                        TypeName = "yearly"
                    },
                    Municipality = new Municipality
                    {
                        MunicipalityId = 1,
                        Name = "Copenhagen"
                    },
                    TaxAmount = 0.2M,
                    StartDtm = new DateTime(2020,01,01),
                    EndDtm = new DateTime(2021,01,01)
                },
                new Data.Entities.MunicipalityTax
                {
                    MunicipalityTaxId = 2,
                    MunicipalityId = 1,
                    TaxTypeId = 2,
                    TaxType = new TaxType
                    {
                        TaxTypeId = 2,
                        TypeName = "monthly"
                    },
                    Municipality = new Municipality
                    {
                        MunicipalityId = 1,
                        Name = "Copenhagen"
                    },
                    TaxAmount = 0.4M,
                    StartDtm = new DateTime(2020,05,01),
                    EndDtm = new DateTime(2021,06,01)
                },
                new Data.Entities.MunicipalityTax
                {
                    MunicipalityTaxId = 3,
                    MunicipalityId = 1,
                    TaxTypeId = 4,
                    TaxType = new TaxType
                    {
                        TaxTypeId = 4,
                        TypeName = "daily"
                    },
                    Municipality = new Municipality
                    {
                        MunicipalityId = 1,
                        Name = "Copenhagen"
                    },
                    TaxAmount = 0.1M,
                    StartDtm = new DateTime(2020,01,01),
                    EndDtm = new DateTime(2020,01,02)
                },
                new Data.Entities.MunicipalityTax
                {
                    MunicipalityTaxId = 4,
                    MunicipalityId = 1,
                    TaxTypeId = 4,
                    TaxType = new TaxType
                    {
                        TaxTypeId = 4,
                        TypeName = "daily"
                    },
                    Municipality = new Municipality
                    {
                        MunicipalityId = 1,
                        Name = "Copenhagen"
                    },
                    TaxAmount = 0.1M,
                    StartDtm = new DateTime(2020,12,25),
                    EndDtm = new DateTime(2020,12,26)
                }
            };

            return municipalityTaxData;
        }

        private List<Municipality> GetMunicipalityData()
        {
            List<Municipality> municipalityData = new List<Municipality>
            {
                new Municipality
                {
                    MunicipalityId = 1,
                    Name = "Copenhagen"
                }
            };
            return municipalityData;
        }
    }
}