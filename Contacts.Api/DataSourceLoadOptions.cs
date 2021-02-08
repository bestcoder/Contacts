using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// https://github.com/DevExpress/DevExtreme.AspNet.Data/blob/master/net/Sample/DataSourceLoadOptions.cs
/// </summary>
namespace DevExtreme.AspNet.Api
{
  [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
  public class DataSourceLoadOptions : DataSourceLoadOptionsBase
  {
  }

  public class DataSourceLoadOptionsBinder : DevExtreme.AspNet.Mvc.IModelBinder
  {

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      var loadOptions = new DataSourceLoadOptions();
      DataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key).FirstOrDefault());
      bindingContext.Result = ModelBindingResult.Success(loadOptions);
      return Task.CompletedTask;
    }

  }
}

