CREATE TABLE Temperature
(
  SerialNo     VARCHAR   NOT NULL,
  DeviceName   VARCHAR,
  Time         TIMESTAMP NOT NULL,
  Value        DOUBLE    NOT NULL,
  PRIMARY KEY (
    (SerialNo, QUANTUM(Time, 1, 'd')),
    SerialNo, Time DESC
  )
);
