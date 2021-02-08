using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Contacts.Api.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data.ResponseModel;
using System.Net.Http;
using System.Threading;
using System.Security.Claims;
using DevExtreme.AspNet.Mvc;

namespace Contacts.Tests
{
  [TestClass]
  public class ApiIntegrationTests
  {

    // TODO: Change this GUID to match any user in your Identity user table.
    public const string UserParm = "?userId=07f946dd-eda7-4d09-ab7d-27603591e790";

    [TestInitialize()]
    public void BeforeEachTest()
    {
    }

    [TestCleanup()]
    public void AfterEachTest()
    {
    }

    [TestMethod]
    public async Task GetContactsList()
    {
      var factory = new WebApplicationFactory<Contacts.Api.Startup>();
      var Client = factory.CreateClient();
      var result = await Client.GetAsync($"/contacts/getcontactslist{UserParm}");
      Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
      var content = result.Content;
      var data = await content.ReadAsStringAsync();
      var contacts = JsonConvert.DeserializeObject<List<Contact>>(data);
      Assert.IsTrue(contacts.Count > 0);
    }

  }
}