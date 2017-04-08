delete from Employees;
delete from shifts;
delete from ShiftsEmployees;

insert into Shifts values('SunMor','Sunday','Morning');
insert into Shifts values('SunEve','Sunday','Evening');
insert into Shifts values('SunNit','Sunday','Night');

insert into Shifts values('MonMor','Monday','Morning');
insert into Shifts values('MonEve','Monday','Evening');
insert into Shifts values('MonNit','Monday','Night');

insert into Shifts values('TueMor','Tuesday','Morning');
insert into Shifts values('TueEve','Tuesday','Evening');
insert into Shifts values('TueNit','Tuesday','Night');

insert into Shifts values('WedMor','Wednesday','Morning');
insert into Shifts values('WedEve','Wednesday','Evening');
insert into Shifts values('WedNit','Wednesday','Night');

insert into Shifts values('ThuMor','Thursday','Morning');
insert into Shifts values('ThuEve','Thursday','Evening');
insert into Shifts values('ThuNit','Thursday','Night');

insert into Shifts values('FriMor','Friday','Morning');
insert into Shifts values('FriEve','Friday','Evening');
insert into Shifts values('FriNit','Friday','Night');

insert into Shifts values('SatMor','Saturday','Morning');
insert into Shifts values('SatEve','Saturday','Evening');
insert into Shifts values('SatNit','Saturday','Night');

insert into Employees(employeeId,userName,password,firstname,lastname,role,address,phonenumber,department) values(1,'admin','123','Jimmy','Wang','Manager','123 Main st','1234567890','Development');
insert into Employees(employeeId,userName,password,firstname,lastname,role,address,phonenumber,department) values(2,'hayden','123','Hayden','Ng','Slave','123 Main st','1234567890','Development');
insert into Employees(employeeId,userName,password,firstname,lastname,role,address,phonenumber,department) values(3,'kevin','123','Kevin','Tam','Employee','123 Main st','1234567890','Development');
insert into Employees(employeeId,userName,password,firstname,lastname,role,address,phonenumber,department) values(4,'matt','123','Matt','Whiteguy','Employee','123 Main st','1234567890','Development');
insert into Employees(employeeId,userName,password,firstname,lastname,role,address,phonenumber,department) values(5,'alex','123','Alex','Bae','Employee','123 Main st','1234567890','Development');

insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','MonMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','MonEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','MonNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','TueMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','TueEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','TueNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('2','MonMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('3','MonMor');

select * from employees;
select * from Shifts;
select * from shiftsemployees;