using MqttSubscriber.NET.Models;
using MqttSubscriber.NET.Database.Interfaces;
using RiakClient;
using RiakClient.Commands.TS;

namespace MqttSubscriber.NET.Database
{
    public class RiakTSClient : IDatabase
    {
        public void InsertTemperatureReadingToDatabase(Message message)
        {
            string table = "Temperature";
            InsertMessageIntoDatabase(table, message);
        }

        public void InsertElectricityReadingToDatabase(Message message)
        {
            string table = "Temperature";
            InsertMessageIntoDatabase(table, message);
        }

        private void InsertMessageIntoDatabase(string table, Message message)
        {
            IRiakEndPoint cluster = RiakCluster.FromConfig("riakConfig");
            IRiakClient client = cluster.CreateClient();

            var cells = new Cell[]
            {
                new Cell(message.SerialNo),
                new Cell(message.DeviceName),
                new Cell(message.Timestamp),
                new Cell(message.Value)
            };

            var rows = new Row[]
            {
                new Row(cells)
            };

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
        }
    }
}
