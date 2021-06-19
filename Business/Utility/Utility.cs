using EzPay.DTO;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EzPay.Business.Utility
{
    public class Utility
    {
        private readonly AppSettings _appSettings;
        public enum PageFlowEnum
        {
            Region,
            Company,
            Charges,
            Tower,
            Parameter
        }
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public Utility(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }



        //public List<TowerDetailsDTO> GetTowerList(TowerDetailsDTO towerDetailsModel)
        //{
        //    try
        //    {
        //        logger.Info("Mapping TowerDetailsDTO to Entity");
        //        SQLDataModel.TowerDetails towerDetails = _mapper.Map<TowerDetailsDTO, SQLDataModel.TowerDetails>(towerDetailsModel);

        //        logger.Info("Mapping completed, getting Tower Details");
        //        var results = _towerDetailsRepository.GetList(towerDetails);
        //        logger.Info("Got Tower Details, Mapping Entity to DTO and exiting method");
        //        return _mapper.Map<List<SQLDataModel.TowerDetails>, List<TowerDetailsDTO>>(results.ToList<SQLDataModel.TowerDetails>());
        //    }

        //    catch (Exception ex)
        //    {
        //        logger.Error(ex);
        //        return null;
        //    }
        //}

        //public IEnumerable<T> GetList<T>(T entity)
        public Expression<Func<T,bool>> GetLambda<T>(T entity)
        {
            PropertyInfo prop = null;
            object value = null;
            foreach (var item in typeof(T).GetProperties())
            {
                prop = item;
                value = item.GetValue(entity);
                if (value != null)
                {
                    if (value.GetType() == typeof(Int64) && Convert.ToInt64(value) <= 0)
                    {
                        continue;
                    }
                    else if (value.GetType() == typeof(Int32) && Convert.ToInt32(value) <= 0)
                    {
                        continue;
                    }

                    if (value.GetType() == typeof(Decimal) && Convert.ToDecimal(value) <= 0)
                    {
                        continue;
                    }
                    else if (value.GetType() == typeof(Double) && Convert.ToDouble(value) <= 0)
                    {
                        continue;
                    }
                    else if (value.GetType() == typeof(System.DateTime) && Convert.ToDateTime(value) <= DateTime.MinValue)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var constant = Expression.Constant(value);
            var property = Expression.Property(parameterExpression, prop.Name);
            var expression = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, parameterExpression);
            //var result = _towerDetailsRepository.FindBy(lambda).ToList();
            //var result = _dbset.Where(lambda.Compile()).AsEnumerable();
            //return result;
            return lambda;
        }

        public string getNavigationUrl(string callingPage, string destinationPage, int Id)
        {
            string finalURL = string.Empty;
            try
            {
                foreach (var enumItem in Enum.GetNames(typeof(PageFlowEnum)))
                {
                    if (enumItem.ToString().ToLower() == callingPage)
                    {
                        finalURL = $"{_appSettings.appURL}{enumItem.ToLower()}?{destinationPage}ID={Id}"; 
                    } 
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                //throw;
            }
            return finalURL;
        }
    }
}
