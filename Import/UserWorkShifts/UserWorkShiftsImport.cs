using Serilog;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class UserWorkShiftsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var userWorkShifts = _baseContextReplica.user_work_shifts.Where(t => t.id>0);

                using (var _baseContextTarget = new BaseContextTarget())
                {
                    List<UserWorkShifts> userWorkShiftsList = new();

                    int c = 1;
                    foreach (var table in userWorkShifts)
                    {
                        var existing = _baseContextTarget.user_work_shifts.Find(table.id);
                        if (existing != null)
                        {
                            _baseContextTarget.user_work_shifts.Remove(existing);
                        }
                        //Log.Logger.Information($"id is: {table.id}");
                        userWorkShiftsList.Add(table);
                        if (c >= 1000)
                        {
                            _baseContextTarget.SaveChanges();
                            _baseContextTarget.user_work_shifts.AddRange(userWorkShiftsList);
                            _baseContextTarget.SaveChanges();
                            userWorkShiftsList.Clear();
                            c = 1;
                        }
                        else
                        {
                            c++;
                        }

                    }
                    _baseContextTarget.SaveChanges();
                    _baseContextTarget.user_work_shifts.AddRange(userWorkShiftsList);
                    _baseContextTarget.SaveChanges();
                }
            }
            Log.Logger.Information("UserWorkShifts Done!");
        }
    }
}
