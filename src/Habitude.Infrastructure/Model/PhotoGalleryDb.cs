using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace Habitude.Infrastructure.Model
{
  [DynamoDBTable("Habitude-PhotoGallery")]
  public class PhotoGalleryDb
  {
    [DynamoDBHashKey]
    public int Id { get; set; }

    [DynamoDBGlobalSecondaryIndexHashKey]
    public string FileName { get; set; }

    public string Description { get; set; }

    //TODO: Add TAGS
  }
}

