﻿using DataReplicator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataReplicator.DatabaseIn.Interfaces
{
    public interface ICanRead
    {
        Task<List<MessageFromMongo>> GetData(string collectionName, DateTime from, DateTime to);
    }
}
