select * from SensorRecords order by id desc;
select * from Collectors;
select * from Dashboard;
select * from Controller;
select * from Sensors;

/*truncate table SensorRecords*/
/*
update Collectors set
	Name = "Pokojíček"
	, Description = "Teplota, vlhkost a CO2 v pokojíčku"
	, SmartHomeControllerId = 1
where id = 17
*/
