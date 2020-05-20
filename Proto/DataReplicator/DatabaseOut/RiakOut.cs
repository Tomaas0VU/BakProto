using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReplicator.DatabaseOut.Interfaces;
using RiakClient;
using RiakClient.Commands.TS;
using DataReplicator.Models;

namespace DataReplicator.DatabaseOut
{
    public class RiakOut : ICanStore
    {
        public async Task InsertData(string collectionName, List<Message> data)
        {
            await InsertMessagesIntoDatabase(collectionName, data);
        }

        private async Task InsertMessagesIntoDatabase(string table, List<Message> messages)
        {
            //TODO: Privaloma paskirstyti po 100, pagal dokumentacija

            IRiakEndPoint cluster = RiakCluster.FromConfig("riakConfig");
            IRiakClient client = cluster.CreateClient();

            var rows = new List<Row>();

            foreach(Message message in messages)
            {
                var cells = new Cell[]
                {
                new Cell(message.SerialNo),
                new Cell(message.DeviceName),
                new Cell(message.Timestamp),
                new Cell(message.Value)
                };
                rows.Add(new Row(cells));
            }

            var columns = new Column[]
            {
                new Column("SerialNo",    ColumnType.Varchar),
                new Column("DeviceName",  ColumnType.Varchar),
                new Column("Time",        ColumnType.Timestamp),
                new Column("Value",       ColumnType.Double)
            };

            var cmd = new Store.Builder()
                    .WithTable(table)
                    .WithColumns(columns)
                    .WithRows(rows)
                    .Build();

            RiakResult rslt = client.Execute(cmd);

            if (!rslt.IsSuccess)
            {
                throw new Exception("Connection to Riak was not successful.");
            }
        }
    }
}
