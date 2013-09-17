using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OakIdeas.AppHarbor.Api.Models;
using System.Collections.Generic;

namespace OakIdeas.AppHarbor.Api.Tests
{
    [TestClass]
    public class Generic_Tests
    {
        string accessToken = "d4ae8f22-f4b8-411d-9d03-8bdfe350d894";
        [TestMethod]
        public void Get_List_Based_On_Url()
        {
            string url = "https://appharbor.com/applications/oakideasadsservice/builds/362049/tests";

            List<Test> builds = AppHarborApi.Instance.GetThing<List<Test>>(accessToken,url).Result;

            string temp = String.Empty;
        }
    }
}
