using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReplicator.DatabaseIn;
using DataReplicator.DatabaseIn.Interfaces;
using DataReplicator.DatabaseOut;
using DataReplicator.DatabaseOut.Interfaces;
using DataReplicator.Models;

namespace DataReplicator
{
    class Program
    {
        static void Main(string[] args)
        {
            // PARAMS
            string connectionStringMongo = "mongodb://192.168.1.105:27017";
            string connectionStringRiak = "";

            string pasteDBLocation = "mongo";
            DateTime startDate = new DateTime(2020, 5, 17);
            TimeSpan startTime = new TimeSpan(0,0,0);
            TimeSpan duration = new TimeSpan(168, 0, 0);

            // CODE

            DateTime from = startDate.AddTicks(startTime.Ticks);
            DateTime to = from.AddTicks(duration.Ticks);

            ICanRead mongoIn = new MongoIn(connectionStringMongo);
            var data = mongoIn.GetData("Temperature", from, to).Result;

            // var list = TransformData(data);

            ICanStore dataOut = new MongoOut(connectionStringMongo);

            dataOut.InsertData("Temperature", data).Wait();
        }
    }
}
