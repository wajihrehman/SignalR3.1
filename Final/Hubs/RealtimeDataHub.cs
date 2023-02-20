using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using Final.BusinessObjects;

namespace Final.Hubs
{
    public class RealtimeDataHub : Hub
    {
        public List<Years> GetUsers()
        {
            List<Years> _lst = new List<Years>();
            using (var connection = new SqlConnection("Server=DESKTOP-NBU4QBG;Database=SignalRFinal;Trusted_Connection=True;"))
            {
                String query = "SELECT Year_ID, Year_Decode FROM dbo.T_Years";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Notification = null;
                    DataTable dt = new DataTable();
                    SqlDependency dependency = new SqlDependency(command);

                    dependency.OnChange += dependency_OnChange;

                    if (connection.State == ConnectionState.Closed) connection.Open();

                    SqlDependency.Start(connection.ConnectionString);
                    var reader = command.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _lst.Add(new Years
                            {
                                Year_ID = Int32.Parse(dt.Rows[i]["Year_ID"].ToString()),
                                Year_Decode = dt.Rows[i]["Year_Decode"].ToString(),
                               
                            });
                        }
                    }
                }
            }
            return _lst;
            //IHubContext context = GlobalHost.ConnectionManager.GetHubContext<RealtimeDataHub>();
            //context.Clients.All.displayUsers(_lst);
        }

        void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                RealtimeDataHub _dataHub = new RealtimeDataHub();
                _dataHub.GetUsers();
            }
        }
    }
}
