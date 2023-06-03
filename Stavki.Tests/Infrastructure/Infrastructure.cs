using NUnit.Framework;
using Stavki.Data.Data;
using Stavki.Data.Data.Enums;

namespace Stavki.Tests.Infrastructure
{
    [TestFixture]
    public class Infrastructure
    {
        private void Mock()
        {
            //_requestRepo.Table.Returns(RequestSearchTestData.GetRequests().AsQueryable());
        }

        [Test(Description = "Поиск по статусу 'Новый'. Должно найтись 2 обращения.")]
        public void RequestsInNewStatusSearch()
        {
            //Arrange
            //var searchSettings = ArrangeSearch();
            //Mock();
            //searchSettings = new SearchSettings()
            //{
            //    RequestStatuses = new List<RequestStatus> { RequestStatus.InWork }
            //};

            ////Act
            //var result = _requestService.SearchRequestsResult(searchSettings);

            ////Assert
            //result.RequestsCount.ShouldEqual(2);
        }
    }
}
