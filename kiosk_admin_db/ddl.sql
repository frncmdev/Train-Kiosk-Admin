-- create database Kiosk_Admin
-- go 

drop table train_station
drop table direction
-- use Kiosk_admin;
-- create table [User] (
--     [userID] NVARCHAR(32) PRIMARY KEY,
--     [username] NVARCHAR(25) UNIQUE not null,
--     [password] NVARCHAR(MAX) not null
-- );

create table Train_Station (
    trainstation_id int PRIMARY KEY,
    trainstation_name NVARCHAR(50)
    
);
create table direction
(
    direction_id int PRIMARY key,
    direction_name NVARCHAR(200),
    trainstation_id int
    CONSTRAINT FK_Direction_Train_Station 
        FOREIGN KEY (trainstation_id) REFERENCES train_station (trainstation_id)
);
-- select * from Train_Station
-- insert into [User] ([userID], [username], [password])values ('722ec959acfd4f9ba1b2bd909ce8558d', 'Kiosk_Admin', '2c76caa66a02f96d86b630fae61b1c8e5d0754fef16ea27466fef75ab7126319')
-- delete from [User]
select * from [user]
