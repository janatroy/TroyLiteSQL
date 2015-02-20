using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInfo
/// </summary>
public class UserInfo
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int EmpNo { get; set; }
    public string EmpName { get; set; }

    public string ManagerUserId { get; set; }
    public string ManagerUserName { get; set; }
    public int ManagerEmpNo { get; set; }
    public string ManagerEmpName { get; set; }

	public UserInfo()
	{
		
	}
}