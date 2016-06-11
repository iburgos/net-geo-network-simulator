using Topshelf;

namespace NetIGeo.Service
{
    public class Program
    {
        public void Main()
        {

            HostFactory.Run(x =>                                 //1
            {
                x.Service<ServiceStarter>(s =>                        //2
                {
                    s.ConstructUsing(name => new ServiceStarter());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => { });               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("Sample Topshelf Host");        //7
                x.SetDisplayName("Stuff");                       //8
                x.SetServiceName("Stuff");                       //9
            });
        }
    }
}