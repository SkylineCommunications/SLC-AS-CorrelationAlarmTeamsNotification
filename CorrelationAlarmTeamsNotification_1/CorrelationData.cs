namespace CorrelationAlarmTeamsNotification_1
{
	using System;

	using Skyline.DataMiner.Net;

	public class CorrelationData
	{
		public int AlarmID { get; set; }

		public int DMAgentID { get; set; }

		public int ElementID { get; set; }

		public int ParameterID { get; set; }

		public string ParameterIdx { get; set; }

		public int RootAlarmID { get; set; }

		public int PrevAlarmID { get; set; }

		public int Severity { get; set; }

		public int Type { get; set; }

		public int Status { get; set; }

		public string AlarmValue { get; set; }

		public DateTime AlarmTime { get; set; }

		public int ServiceRCA { get; set; }

		public int ElementRCA { get; set; }

		public int ParameterRCA { get; set; }

		public int SeverityRange { get; set; }

		public int SourceID { get; set; }

		public int UserStatus { get; set; }

		public string Owner { get; set; }

		public string ImpactedServices { get; set; }

		public string Color { get; set; }

		public static CorrelationData GetCorrelationData(string alarmInfo)
		{
			string[] parts = alarmInfo.Split('|');

			// Check for the expected number of parts
			if (parts.Length < 20)
			{
				// Handle the error or return an empty CorrelationData
				return new CorrelationData();
			}

			CorrelationData correlationData = new CorrelationData
			{
				AlarmID = Tools.ToInt32(parts[0]),
				DMAgentID = Tools.ToInt32(parts[1]),
				ElementID = Tools.ToInt32(parts[2]),
				ParameterID = Tools.ToInt32(parts[3]),
				ParameterIdx = parts[4],
				RootAlarmID = Tools.ToInt32(parts[5]),
				PrevAlarmID = Tools.ToInt32(parts[6]),
				Severity = Tools.ToInt32(parts[7]),
				Type = Tools.ToInt32(parts[8]),
				Status = Tools.ToInt32(parts[9]),
				AlarmValue = parts[10],
				AlarmTime = DateTime.Parse(parts[11]),
				ServiceRCA = Tools.ToInt32(parts[12]),
				ElementRCA = Tools.ToInt32(parts[13]),
				ParameterRCA = Tools.ToInt32(parts[14]),
				SeverityRange = Tools.ToInt32(parts[15]),
				SourceID = Tools.ToInt32(parts[16]),
				UserStatus = Tools.ToInt32(parts[17]),
				Owner = parts[18],
				ImpactedServices = parts[19],
				Color = GetColor(Tools.ToInt32(parts[7])),
			};

			return correlationData;
		}

		public static string GetColor(int severity)
		{
			switch (severity)
			{
				case 1:
					return "#f03241"; // Color type="critical" value="240,50,65"
				case 2:
					return "#f5d228"; // Color type="major" value="245,210,40"
				case 3:
					return "#61d6d6"; // Color type="minor" value="97,214,214"
				case 4:
					return "#3b78ff"; // Color type="warning" value="59,120,255"
				default:
					return "#000000"; // Default case (you can change this to handle unknown cases)
			}
		}
	}
}
