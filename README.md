# EQ Log Analyzer
EQ Log Analyzer was created to offer a clean and clutter-free log parser with two unique capabilities.

1.	Display a chart that graphically displays boss and buff uptimes so that these can be visually optimized for a raid.
2.	Provide real-time merging of multiple personal log files into one combined set.

As background, a personal log file is really only reliable for the player taking the log. Results for other players vary because some players are out of logging range at times during a raid, because the logger dies, or the logger does not have all logging options enabled. Not only do results vary from logger to logger, but all of their raid totals are often well below the actual damage output.

EQ Log Analyzer is designed to provide a more complete parse analysis by merging multiple parses. It does this in real-time by monitoring the log file during a raid and uploading the events to a SQL Server database where they are filtered for duplicates and logged. Reports against the merged set can be viewed through the Server Report window in EQ Log Analyzer.

Server Merging shows no impact on local system or internet performance during monitoring and the logger can simply toggle server logging on or off at any time. Reports from the server are returned in sub-second time frames.

This first version meets my needs for the time being. It logs all damage, deaths, and a limted set of buffs. It does not log heals at this time. I am open to expanding it on request or to allowing collaborators to do so.

I am not including an install because without database connectivity, all server logging options are disabled. If you wish to use the server logging options, you would have to provide a SQL Server connection string, compile, and deploy your own version using ClickOnce if you like. All necessary database object creation scripts are included.

Personal Log Report
 
 ![image](https://user-images.githubusercontent.com/120231132/206827000-59970be9-b94c-4805-b334-c646d0e7f1b9.png)

Buff Coverage Chart

![image](https://user-images.githubusercontent.com/120231132/206827008-7a492bdb-beda-4c69-a75c-c44978863f4e.png)
 
Merged Server Report
 
 ![image](https://user-images.githubusercontent.com/120231132/206827013-9091d1e0-7b03-472c-bebe-18c58355bf0f.png)

