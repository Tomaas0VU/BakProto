CREATE TABLE Electricity
(
  SerialNo     VARCHAR   NOT NULL,
  DeviceName   VARCHAR,
  Timestamp    TIMESTAMP NOT NULL,
  Value        DOUBLE    NOT NULL,
  PRIMARY KEY (
    (SerialNo, QUANTUM(Timestamp, 1, 'd')),
    SerialNo, Timestamp DESC
  )
);