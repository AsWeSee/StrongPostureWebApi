using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace PostureWebApi.JsonModels
{
    public class LearnPoint
    {
        public string UserName { get; set; }

        [LoadColumn(0, 13), VectorType(14), ColumnName("Points")]
        public float[] Points { get; set; }

        [LoadColumn(14)]
        public bool isGood { get; set; }
    }
}
