# CorrelationAlarmTeamsNotification

This repository contains an automation script that can be launched from correlation and creates a teams channel message that includes the element, parameter description and alarm value. There is also (commented) code to include an alarm property. A teams channel message will be displayed using adaptive cards.

## How to get started
1. Use the script in the cloud connected DataMiner Agent. Organization must Grant Admin Consent for chat integration with Teams.

2. Install DataMiner Application in MS Teams.

3. It is neccessary to use Chat Integration automation scripts to fetch the teams and channels data from the Teams. Scripts can be found here: 
<a href="https://github.com/SkylineCommunications/ChatOps-Extensions/tree/main/ChatIntegrationExamples" target="_blank">Chat Integration Examples on GitHub</a> or here: <a href="https://catalog.dataminer.services/details/package/3129" target="_blank">Chat Integration Examples on Catalog</a>.
   
      2.1. Create a memory file "Teams"
      
      2.2. Run Fetch Teams script

      2.3. Create a memory file "Channels" 
  
      2.3. Run Fetch Channels script (With Parameter TeamID value file "Teams")

4. "Correlation Alarm Teams Notification" script can be used now. Please pay attention that Script Parameters have predefined values for Team Id and Channel Id value files, as defined in the previous step. If these memory files have different names, parameter value files should be modified before the usage.

5. Create a new correlation rule with desired alarm filtering and rule conditions. In actions, specify CorrelationAlarmTeamsNotification script to run. It is neccessary to select desired Team and Channel where the notification should be sent, to define Notification title that will be displayed and Parameter description must have value "[field(parametername)]". Please note that "Evaluate script parameter values" must be checked too.

## Help

<a href="https://skyline.be/contact/tech-support" target="_blank">Skyline Communications: Support</a>

## Contact

<a href="https://skyline.be/contact" target="_blank">Skyline Communications: Contact</a>

## Version History

<a href="https://github.com/SkylineCommunications/SLC-AS-CorrelationAlarmTeamsNotification/releases" target="_blank">Skyline Communications: Contact</a>

## License

This project is licensed under the <a href="https://github.com/SkylineCommunications/SLC-AS-CorrelationAlarmTeamsNotification/blob/main/LICENSE" target="_blank">MIT License</a> – see the file for details.

