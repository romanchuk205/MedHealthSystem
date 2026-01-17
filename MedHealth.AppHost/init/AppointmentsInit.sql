-- Перевіряємо, чи база існує, і створюємо, якщо ні
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MedHealthAppointmentsDB')
BEGIN
    CREATE DATABASE MedHealthAppointmentsDB;
END
GO

USE MedHealthAppointmentsDB;
GO

-- Створюємо таблицю пацієнтів
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Patients' and xtype='U')
CREATE TABLE Patients (
                          Id int IDENTITY(1,1) PRIMARY KEY,
                          FullName nvarchar(255) NOT NULL,
                          PhoneNumber nvarchar(50) NOT NULL,
                          Email nvarchar(255) NULL
);
GO

-- Додаємо тестові дані
INSERT INTO Patients (FullName, PhoneNumber, Email)
VALUES ('Марія Іваненко', '099-123-45-67', 'maria@test.com');
GO