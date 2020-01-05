using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Habitude.Framework.IntegrationTestss;
using Habitude.Infrastructure.AWS.S3;
using Habitude.Infrastructure.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Extensions.DependencyInjection;
using Habitude.Test.Common;

namespace Habitude.Framework.IntegrationTests
{
  [TestClass]
  [TestCategory("IntegrationTests")]
  public class S3BucketLocalStackTester
  {
    private static IServiceProvider _container;
    private static IAmazonS3 _s3Client;
    private static SetupTestS3Bucket _setupTestS3Bucket;

    [ClassInitialize]
    public static async Task ClassSetup(TestContext context)
    {
      _container = new IntegrationTestContainerBuilder().Build();
      _s3Client = _container.GetService<IAmazonS3>();
      _setupTestS3Bucket = new SetupTestS3Bucket(_s3Client);
      await _setupTestS3Bucket.Setup();
    }

    [TestInitialize]
    public async Task Setup()
    {
      //_container = new IntegrationTestContainerBuilder().Build();
      try
      {
        await _setupTestS3Bucket.ArrangeTestFile("Test.txt");
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    [TestCleanup]
    public  async Task Cleanup()
    {
      //await _setupTestS3Bucket.Cleanup();
    }

    [ClassCleanup]
    public static async Task ClassCleanup()
    {
      await _setupTestS3Bucket.Cleanup();
    }

    [TestMethod]
    public void Test1()
    {
      int i = 1;
      Assert.AreEqual(1, i);
    }

    [TestMethod]
    public void Test2()
    {
      int i = 1;
      Assert.AreEqual(1, i);
    }
  }
}
