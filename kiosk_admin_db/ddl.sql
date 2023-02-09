-- create database Kiosk_Admin
-- go 
drop table campus
-- drop table direction
-- drop table train_station
-- use Kiosk_admin;
-- create table [User] (
--     [userID] NVARCHAR(32) PRIMARY KEY,
--     [username] NVARCHAR(25) UNIQUE not null,
--     [password] NVARCHAR(MAX) not null
-- );
create table Train_Station (
    trainstation_id int PRIMARY KEY,
    trainstation_name NVARCHAR(50),
    travelTime int not null     
);
create table Campus (
    campus_id int IDENTITY(1,1) PRIMARY KEY,
    campus_name NVARCHAR(50) not null,
    isSelected bit not null,
    trainstation_id int not null,
    CONSTRAINT FK_Campus_TrainStation
        FOREIGN KEY (trainstation_id) REFERENCES train_station
);

-- select * from Train_Station
-- insert into [User] ([userID], [username], [password])values ('722ec959acfd4f9ba1b2bd909ce8558d', 'Kiosk_Admin', '2c76caa66a02f96d86b630fae61b1c8e5d0754fef16ea27466fef75ab7126319')
-- delete from [User]
-- select * from [user]
insert into Train_Station (trainstation_id, trainstation_name, travelTime) values 
    (1162, 'Richmond', 5),
    (1028, 'Broadmeadows', 10),
    (1181, 'Southern Cross', 5),
    (1064, 'Essendon', 10);
insert into Campus (campus_name, isSelected, trainstation_id) values 
    ('Cremorne', 1, 1162),
    ('Broadmeadows', 0, 1028),
    ('Docklands', 0, 1181),
    ('Essendon', 0, 1064)
insert into [User] ([userId], [username], [password]) values
    ('b28f8a2346bb4fc6800a5572e5e20209', 'RootAuthority', 'ea4f85ee3cb498c748564fbc64ca290bfd96ae8d0a84c434fea541f50fdc0424')