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

		public int ServiceRca { get; set; }

		public int ElementRca { get; set; }

		public int ParameterRca { get; set; }

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
				ServiceRca = Tools.ToInt32(parts[12]),
				ElementRca = Tools.ToInt32(parts[13]),
				ParameterRca = Tools.ToInt32(parts[14]),
				SeverityRange = Tools.ToInt32(parts[15]),
				SourceID = Tools.ToInt32(parts[16]),
				UserStatus = Tools.ToInt32(parts[17]),
				Owner = parts[18],
				ImpactedServices = parts[19],
				Color = GetColorCode(Tools.ToInt32(parts[7])),
			};

			return correlationData;
		}

		private static string GetColorCode(int severity)
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
				case 5:
					return "#16c60c"; // Color type="normal" value="22,198,12"
				case 24:
					return "#16c60c"; // Color type="error" value="204,204,204"
				default:
					return "#000000"; // Default case (you can change this to handle unknown cases)
			}
		}
	}
}
