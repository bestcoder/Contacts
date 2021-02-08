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
using System;
using System.Linq;

namespace Contacts.Tests
{
  [TestClass]
  public class ApiIntegrationTests
  {

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
    public void Codility1()
    {
      var values = new int[] { 3, 4, 5, 3, 7 };
      var result = solution(values);
      var expected = 3;
      Assert.AreEqual(expected, result);

      values = new int[] { 1, 2, 3, 4 };
      result = solution(values);
      expected = -1;
      Assert.AreEqual(expected, result);

      values = new int[] { 1, 3, 1, 2 };
      result = solution(values);
      expected = 0;
      Assert.AreEqual(expected, result);

    }

    /// <summary>
    /// returns the sum of all integers which are a multiples of 4
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    public int solution(int[] A)
    {
      // 3, 4, 5, 3, 7
      // can remove any one of 3 numbers to achieve result of odd/even
      if (IsPleasing(A.ToList()) == true)
        return 0;

      var successCount = 0;
      var index = 0;
      foreach (var item in A)
      {
        var trees = A.ToList();
        trees.RemoveAt(index);
        if (IsPleasing(trees))
        {
          successCount += 1;
        }
        index += 1;
      }
      return successCount > 0 ? successCount : -1;
    }

    /// <summary>
    /// Evaluates an array of trees to determine whether it is aesthetically pleasing.
    /// </summary>
    /// <remarks>
    /// Trees are pleasing only when all are shorter, taller, shorter, taller, etc.
    /// </remarks>
    private bool IsPleasing(List<int> treeHeights)
    {
      var result = true;
      var lastTreeHeight = 0;
      var lastWasTaller = false;
      // 3, 4, 5, 3, 7
      var firstTree = true;
      foreach (var treeHeight in treeHeights)
      {
        if (firstTree == true)
          firstTree = false;
        else
        {
          if ((treeHeight == lastTreeHeight) || (lastWasTaller == true && treeHeight > lastTreeHeight))
          {
            result = false;
            break;
          }
          else // last tree was shorter than the one before it
          {
            // current tree must be taller
            if (treeHeight < lastTreeHeight)
            {
              result = false;
              break;
            }
          }
          lastWasTaller = lastTreeHeight > treeHeight;
          lastTreeHeight = treeHeight;
        }
      }
      return result;
    }

  }
}

