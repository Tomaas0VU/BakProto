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

            string pasteDBLocation = "mongo";
            DateTime startDate = new DateTime(2010, 1, 1);
            TimeSpan startTime = new TimeSpan(0,0,0);
            TimeSpan duration = new TimeSpan(168, 0, 0);

            int howManyDaysBack = -7;

            // CODE

            DateTime from = startDate.AddTicks(startTime.Ticks);
            DateTime to = from.AddTicks(duration.Ticks);

            ICanRead mongoIn = new MongoIn(connectionStringMongo);
            var data = mongoIn.GetData("Temperature", from, to).Result;

            var list = TransformData(data, howManyDaysBack);
            
            ICanStore dataOut = null;
            if (pasteDBLocation.Equals("mongo"))
            {
                dataOut = new MongoOut(connectionStringMongo);
            }
            else if (pasteDBLocation.Equals("riak"))
            {
                dataOut = new RiakOut();
            }

            dataOut.InsertData("Temperature", list).Wait();
        }

        private static List<Message> TransformData(List<MessageFromMongo> data, int howManyDaysBack)
        {
            return data.Select(x => new Message {
                SerialNo = x.SerialNo,
                DeviceName = x.DeviceName,
                Timestamp = x.Timestamp.AddDays(-1 * howManyDaysBack),
                Value = x.Value
            }).ToList();
        }

    }
}
