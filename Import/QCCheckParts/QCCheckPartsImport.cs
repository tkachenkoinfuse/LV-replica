using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class QCCheckPartsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("QCCheckParts Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.qc_check_parts.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<QCCheckPartsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("QCCheckParts Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<QCCheckParts> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<QCCheckPartsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<QCCheckParts> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<QCCheckParts> qcCheckPartsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.qc_check_parts.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.qc_check_parts.Remove(existing);
                    }
                    qcCheckPartsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.qc_check_parts.AddRange(qcCheckPartsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<QCCheckParts> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<QCCheckParts> qcCheckParts = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.qc_check_parts
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    qcCheckParts.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return qcCheckParts;
        }
    }
}
