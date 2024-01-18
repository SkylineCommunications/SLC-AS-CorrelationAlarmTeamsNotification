namespace CorrelationAlarmTeamsNotification_1
{
	using System;
	using System.Collections.Generic;

	using AdaptiveCards;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.DcpChatIntegrationHelper.Common;
	using Skyline.DataMiner.DcpChatIntegrationHelper.Teams;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			var chatIntegrationHelper = new ChatIntegrationHelperBuilder().Build();
			try
			{
				string teamId = GetRequiredParam(engine, "Team Id");
				string channelId = GetRequiredParam(engine, "Channel Id");
				string notification = GetRequiredParam(engine, "Notification");
				string parameterDescription = GetRequiredParam(engine, "Parameter Description");

				// string alarmPropertyName = GetRequiredParam(engine, "Alarm Property Name");
				string alarmInfo = engine.GetScriptParam(65006).Value;
				CorrelationData correlationAlarmData = CorrelationData.GetCorrelationData(alarmInfo);
				if (correlationAlarmData is null)
				{
					engine.ExitFail("Alarm information is empty.");
					return;
				}

				var element = engine.FindElement(correlationAlarmData.DMAgentID, correlationAlarmData.ElementID);
				if (element is null)
				{
					engine.ExitFail("Element information is empty.");
					return;
				}

				// var alarmProperty = engine.GetAlarmProperty(correlationAlarmData.DMAgentID, correlationAlarmData.ElementID, correlationAlarmData.AlarmID, alarmPropertyNameParam.Value);
				List<AdaptiveElement> adaptiveCard = CreateNotification(notification, parameterDescription, correlationAlarmData, element.ElementName);

				try
				{
					chatIntegrationHelper.Teams.TrySendChannelNotification(teamId, channelId, adaptiveCard);
				}
				catch (TeamsChatIntegrationException e)
				{
					engine.ExitFail($"Couldn't send the notification to the channel with id {channelId} with error {e.Message}.");
					return;
				}

				engine.ExitSuccess($"The notification was sent to the channel with id {channelId}!");
			}
			catch (ScriptAbortException)
			{
				// Also ExitSuccess is a ScriptAbortException
				throw;
			}
			catch (Exception e)
			{
				engine.ExitFail($"An exception occurred with the following message: {e.Message}");
			}
			finally
			{
				chatIntegrationHelper?.Dispose();
			}
		}

		private static string GetRequiredParam(IEngine engine, string parameterName)
		{
			try
			{
				var parameter = engine.GetScriptParam(parameterName);
				if (string.IsNullOrWhiteSpace(parameter?.Value))
				{
					engine.ExitFail($"'{parameterName}' parameter is required.");
					return null;
				}

				return parameter.Value;
			}
			catch (ScriptAbortException e)
			{
				throw new ScriptAbortException(e.Message);
			}
		}

		private List<AdaptiveElement> CreateNotification(string notification, string parameterDescription, CorrelationData correlationAlarmData, string element)
		{
			AdaptiveColumnSet notificationTitle = CreateNotificationTitle(notification, correlationAlarmData.Color);
			var adaptiveCard = new List<AdaptiveElement>
				{
					notificationTitle,
					new AdaptiveFactSet
					{
						Facts = new List<AdaptiveFact>
						{
							new AdaptiveFact("Element:", element),
							new AdaptiveFact("Parameter Description:", parameterDescription),
							new AdaptiveFact("Alarm Value:", correlationAlarmData.AlarmValue),

							// new AdaptiveFact("Alarm Property:", alarmProperty),
						},
					},
				};
			return adaptiveCard;
		}

		private AdaptiveColumnSet CreateNotificationTitle(string notification, string notificationColor)
		{
			// Create a column set to hold the color and text
			var columnSet = new AdaptiveColumnSet();

			var colorColumn = new AdaptiveColumn { Width = "auto" };
			var coloredBox = new AdaptiveImage
			{
				Size = AdaptiveImageSize.Stretch,
				Url = new Uri("https://raw.githubusercontent.com/SkylineCommunications/SLC-AS-CorrelationAlarmTeamsNotification/main/images/empty_image.png"),
				BackgroundColor = notificationColor,
				Style = AdaptiveImageStyle.Person,
				PixelWidth = 20,
				PixelHeight = 20,
			};
			colorColumn.Items.Add(coloredBox);

			var textColumn = new AdaptiveColumn { Width = "stretch" };
			var textBlock = new AdaptiveTextBlock
			{
				Text = notification,
				Color = AdaptiveTextColor.Default,
				Size = AdaptiveTextSize.Medium,
			};
			textColumn.Items.Add(textBlock);

			columnSet.Columns.Add(colorColumn);
			columnSet.Columns.Add(textColumn);
			return columnSet;
		}
	}
}