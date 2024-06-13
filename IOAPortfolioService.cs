using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [ServiceContract]
    public interface IOAPortfolioService
    {
        [OperationContract]
        void GetGoals(string getReq);
    }

    public abstract class OAPortfolioServiceProviderBase : ServiceProviderBase
    {
        public IOAPortfolioService Proxy;
        public abstract void GetGoals(string getReq);
    }


    public class OAPortfolioServiceProvider : OAPortfolioServiceProviderBase
    {
        public override void GetGoals(string getReq)
        {
            Console.WriteLine("test");
            IOAPortfolioService proxy = null;
            //GetGoalsResponse platformCallResponse;
            try
            {
                proxy = base.CreateProxyInterface<IOAPortfolioService>();
                proxy.GetGoals(getReq);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                base.DisposeProxy(proxy);
            }
        }
    }
}
