using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using ServiceStack.DataAnnotations;

namespace ExamplePubSub.Domain
{
  public   class Article
  {
      private int _id;

      private string _title;

      private string _body;

      private string _intro;

      public Article()
      {
          LastModifiedDate = DateTime.Now;
      }

      [AutoIncrement]
      public int Id
      {
          get
          {
              return this._id;
          }
          set
          {
              this._id = value;
          }
      }
      [StringLength(100), Required]
      public string Title
      {
          get
          {
              return this._title;
          }
          set
          {
              this._title = value;
          }
      }

      public string Body
      {
          get
          {
              return this._body;
          }
          set
          {
              this._body = value;
          }
      }

      public string Intro
      {
          get
          {
              return this._intro;
          }
          set
          {
              this._intro = value;
          }
      }

      public DateTime LastModifiedDate { get; set; }
  }
}
