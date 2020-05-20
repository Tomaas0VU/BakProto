CREATE TABLE TemperatureProduction
(
  Country      VARCHAR   NOT NULL,
  SerialNo     VARCHAR   NOT NULL,
  DeviceName   VARCHAR,
  Time         TIMESTAMP NOT NULL,
  Value        DOUBLE    NOT NULL,
  PRIMARY KEY (
    (Country, QUANTUM(Time, 1, 'd')),
    Country, Time DESC
  )
);
